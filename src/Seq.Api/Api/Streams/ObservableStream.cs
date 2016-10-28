// Copyright 2016 Datalust; based on code from 
// Serilog.Sinks.Observable, Copyright 2013-2016 Serilog Contributors
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//     http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using System.Linq;

namespace Seq.Api.Streams
{
    // Some questionable synchronization in this one; probably better to take the Rx dependency
    // and use the Rx support classes here.
    public sealed partial class ObservableStream<T> : IObservable<T>, IDisposable
    {
        readonly object _syncRoot = new object();
        IList<IObserver<T>> _observers = new List<IObserver<T>>();
        bool _ended, _disposed;
        Task _run;

        readonly ClientWebSocket _socket;
        readonly Func<TextReader, T> _deserialize;

        internal ObservableStream(ClientWebSocket socket, Func<TextReader, T> deserialize)
        {
            if (socket == null) throw new ArgumentNullException(nameof(socket));
            if (deserialize == null) throw new ArgumentNullException(nameof(deserialize));

            _deserialize = deserialize;
            _socket = socket;
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            lock (_syncRoot)
            {
                if (_disposed)
                    throw new ObjectDisposedException(message: "The observable stream is disposed.", innerException: null);

                if (_ended)
                {
                    observer.OnCompleted();
                    return new Unsubscriber(this, observer);
                }

                _observers = _observers.Concat(new[] { observer }).ToList();

                if (_run == null)
                    _run = Task.Run(Receive);
            }

            return new Unsubscriber(this, observer);
        }

        void Unsubscribe(IObserver<T> observer)
        {
            if (observer == null) throw new ArgumentNullException(nameof(observer));

            lock (_syncRoot)
            {
                if (_disposed)
                    throw new ObjectDisposedException(message: "The observable stream is disposed.", innerException: null);

                _observers = _observers.Except(new[] { observer }).ToList();
            }
        }

        async Task Receive()
        {
            var buffer = new byte[16 * 1024];
            var current = new MemoryStream();
            var reader = new StreamReader(current, new UTF8Encoding(false));

            while (_socket.State == WebSocketState.Open)
            {
                var received = await _socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (received.MessageType == WebSocketMessageType.Close)
                {
                    await _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                    End();
                }
                else
                {
                    current.Write(buffer, 0, received.Count);

                    if (received.EndOfMessage)
                    {
                        current.Position = 0;
                        var value = _deserialize(reader);

                        current.SetLength(0);
                        reader.DiscardBufferedData();

                        Emit(value);
                    }
                }
            }
        }

        void Emit(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));

            IList<IObserver<T>> observers;
            lock (_syncRoot)
            {
                if (_ended || _disposed)
                    return;

                observers = _observers;
            }

            IList<Exception> exceptions = null;
            foreach (var observer in observers)
            {
                try
                {
                    observer.OnNext(value);
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();
                    exceptions.Add(ex);
                }
            }

            if (exceptions != null)
                OnError(exceptions);
        }

        void End()
        {
            IList<IObserver<T>> observers;
            lock (_syncRoot)
            {
                if (_ended)
                    return;

                observers = _observers;
                _observers = new List<IObserver<T>>();
                _ended = true;
            }

            IList<Exception> exceptions = null;
            foreach (var observer in observers)
            {
                try
                {
                    observer.OnCompleted();
                }
                catch (Exception ex)
                {
                    if (exceptions == null)
                        exceptions = new List<Exception>();
                    exceptions.Add(ex);
                }
            }

            if (exceptions != null)
                OnError(exceptions);
        }

        void OnError(IList<Exception> exceptions)
        {
            // This will hit TaskScheduler.UnobservedTaskException
            throw new AggregateException("At least one observer failed to accept the event", exceptions);            
        }

        public void Dispose()
        {
            lock (_syncRoot)
            {
                if (_disposed) return;
                _disposed = true;
            }

            try
            {
                if (_socket.State == WebSocketState.Open)
                {
                    using (var timeout = new CancellationTokenSource())
                    {
                        timeout.CancelAfter(30000);
                        _socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Close requested", timeout.Token)
                            .ConfigureAwait(false).GetAwaiter().GetResult();
                    }
                }
            }
            catch { }

            End();

            if (_run != null)
                _run.ConfigureAwait(false).GetAwaiter().GetResult();

            _socket.Dispose();
        }
    }
}
