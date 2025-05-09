// Copyright © Datalust and contributors. 
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
using System.Threading.Tasks;
using Seq.Api.Model.Data;
using Seq.Api.Model.Shared;
using Seq.Api.Model.Signals;

namespace Seq.Api.ResourceGroups
{
    /// <summary>
    /// Execute SQL-style queries against the API.
    /// </summary>
    public class DataResourceGroup : ApiResourceGroup
    {
        internal DataResourceGroup(ILoadResourceGroup connection)
            : base("Data", connection)
        {
        }

        /// <summary>
        /// Execute an SQL query and retrieve the result set as a structured <see cref="QueryResultPart"/>.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
        /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
        /// <param name="signal">A signal expression over which the query will be executed.</param>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="query"/>.</param>
        /// <param name="trace">Enable detailed (server-side) query tracing.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <returns>A structured result set.</returns>
        public async Task<QueryResultPart> QueryAsync(
            string query,
            DateTime? rangeStartUtc = null,
            DateTime? rangeEndUtc = null,
            SignalExpressionPart signal = null,
            SignalEntity unsavedSignal = null,
            TimeSpan? timeout = null,
            Dictionary<string, object> variables = null,
            bool trace = false,
            CancellationToken cancellationToken = default)
        {
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, unsavedSignal, timeout, variables, trace, out var body, out var parameters);
            return await GroupPostAsync<EvaluationContextPart, QueryResultPart>("Query", body, parameters, cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Execute an SQL query and retrieve the result set as a structured <see cref="QueryResultPart"/>.
        /// </summary>
        /// <param name="query">The query to execute.</param>
        /// <param name="rangeStartUtc">The earliest timestamp from which to include events in the query result.</param>
        /// <param name="rangeEndUtc">The exclusive latest timestamp to which events are included in the query result. The default is the current time.</param>
        /// <param name="signal">A signal expression over which the query will be executed.</param>
        /// <param name="unsavedSignal">A constructed signal that may not appear on the server, for example, a <see cref="SignalEntity"/> that has been
        /// created but not saved, a signal from another server, or the modified representation of an entity already persisted.</param>
        /// <param name="timeout">The query timeout; if not specified, the query will run until completion.</param>
        /// <param name="variables">Values for any free variables that appear in <paramref name="query"/>.</param>
        /// <param name="cancellationToken">Token through which the operation can be cancelled.</param>
        /// <param name="trace">Enable detailed (server-side) query tracing.</param>
        /// <returns>A CSV result set.</returns>
        public async Task<string> QueryCsvAsync(
            string query,
            DateTime? rangeStartUtc = null,
            DateTime? rangeEndUtc = null,
            SignalExpressionPart signal = null,
            SignalEntity unsavedSignal = null,
            TimeSpan? timeout = null,
            Dictionary<string, object> variables = null,
            bool trace = false,
            CancellationToken cancellationToken = default)
        {
            MakeParameters(query, rangeStartUtc, rangeEndUtc, signal, unsavedSignal, timeout, variables, trace, out var body, out var parameters);
            parameters.Add("format", "text/csv");
            return await GroupPostReadStringAsync("Query", body, parameters, cancellationToken).ConfigureAwait(false);
        }

        static void MakeParameters(
            string query,
            DateTime? rangeStartUtc,
            DateTime? rangeEndUtc,
            SignalExpressionPart signal,
            SignalEntity unsavedSignal,
            TimeSpan? timeout,
            Dictionary<string, object> variables,
            bool trace,
            out EvaluationContextPart body,
            out Dictionary<string, object> parameters)
        {
            parameters = new Dictionary<string, object>
            {
                {"q", query}
            };

            if (rangeStartUtc != null)
                parameters.Add(nameof(rangeStartUtc), rangeStartUtc);

            if (rangeEndUtc != null)
                parameters.Add(nameof(rangeEndUtc), rangeEndUtc.Value);

            if (signal != null)
                parameters.Add(nameof(signal), signal.ToString());

            if (timeout != null)
                parameters.Add("timeoutMS", timeout.Value.TotalMilliseconds.ToString("0"));
            
            if (trace)
                parameters.Add("trace", true);

            body = new EvaluationContextPart { Signal = unsavedSignal, Variables = variables };
        }
    }
}
