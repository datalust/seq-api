using System;

namespace Seq.Api.Client
{
    public class SeqApiException : Exception
    {
        public SeqApiException(string message)
            : base(message)
        {
        }
    }
}