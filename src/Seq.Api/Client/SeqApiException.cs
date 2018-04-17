using System;
using System.Net;

namespace Seq.Api.Client
{
    /// <summary>
    /// Thrown when an action cannot be performed.
    /// </summary>
    public class SeqApiException : Exception
    {
        /// <summary>
        /// Construct a <see cref="SeqApiException"/> with the given message and status code.
        /// </summary>
        /// <param name="message">A message describing the error.</param>
        /// <param name="statusCode">The corresponding status code returned from Seq, if available.</param>
        public SeqApiException(string message, HttpStatusCode? statusCode)
            : base(message)
        {
            StatusCode = statusCode;
        }
        
        /// <summary>
        /// The status code returned from Seq, if available.
        /// </summary>
        public HttpStatusCode? StatusCode { get; }
    }
}