﻿// Copyright © Datalust and contributors. 
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
using System.Net;

#nullable enable

namespace Seq.Api.Client;

/// <summary>
/// Thrown when an action cannot be performed.
/// </summary>
public class SeqApiException : Exception
{
    /// <summary>
    /// Construct a <see cref="SeqApiException"/> with the given message and status code.
    /// </summary>
    /// <param name="message">A message describing the error.</param>
    public SeqApiException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Construct a <see cref="SeqApiException"/> with the given message and status code.
    /// </summary>
    /// <param name="message">A message describing the error.</param>
    /// <param name="statusCode">The corresponding status code returned from Seq, if available.</param>
    public SeqApiException(string message, HttpStatusCode? statusCode)
        : this(message)
    {
        StatusCode = statusCode;
    }

    /// <summary>
    /// Construct a <see cref="SeqApiException"/> with the given message and status code.
    /// </summary>
    /// <param name="message">A message describing the error.</param>
    /// <param name="innerException">The cause of the API failure.</param>
    public SeqApiException(string message, Exception innerException)
        : base(message, innerException)
    {
    }
        
    /// <summary>
    /// The status code returned from Seq, if available.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }
}