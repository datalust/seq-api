using System;
using System.Net.WebSockets;
using System.Threading.Tasks;
using Seq.Api.Client;

namespace Seq.Api.Streams;

// Async enumerators and try/catch blocks don't play nicely together. These extensions help maintain the
// previous exception contract around API calls.
static class WebSocketTaskExtensions
{
    public static async Task<T> WithApiExceptions<T>(this Task<T> task)
    {
        await ((Task)task).WithApiExceptions();
        return task.Result;
    }

    public static async Task WithApiExceptions(this Task task)
    {
        try
        {
            await task;
        }
        catch (WebSocketException ex)
        {
            throw new SeqApiException($"The WebSocket API call failed ({ex.ErrorCode}/{ex.WebSocketErrorCode}).", ex);
        }
        catch (Exception ex)
        {
            throw new SeqApiException("The API call failed.", ex);
        }
    }
}