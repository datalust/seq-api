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
using System.Linq;
using Tavis.UriTemplates;

// ReSharper disable PossibleMultipleEnumeration

namespace Seq.Api.Model
{
    /// <summary>
    /// A hypermedia link. A link is an RFC 6570 URI template that can be
    /// parameterized in order to produce a complete URI (if the template contains no
    /// parameters then it may also be a literal URI).
    /// </summary>
    public struct Link
    {
        /// <summary>
        /// An empty link.
        /// </summary>
        public static Link Empty { get; } = default;

        /// <summary>
        /// Construct a link.
        /// </summary>
        /// <param name="template">The URI template.</param>
        public Link(string template)
        {
            Template = template ?? throw new ArgumentNullException(nameof(template));
        }

        /// <summary>
        /// Get the unprocessed URI template.
        /// </summary>
        public string Template { get; }

        /// <summary>
        /// Parameterize the link to construct a URI.
        /// </summary>
        /// <param name="parameters">Parameters to substitute into the template, if any.</param>
        /// <returns>A constructed URI.</returns>
        /// <remarks>This method ensures that templates containing parameters cannot be accidentally
        /// used as URIs.</remarks>
        public string GetUri(IDictionary<string, object> parameters = null)
        {
            if (Template == null) throw new InvalidOperationException("Attempted to process an empty URI template.");

            var template = new UriTemplate(Template);
            if (parameters != null)
            {
                var missing = parameters.Select(p => p.Key).Except(template.GetParameterNames());
                if (missing.Any())
                    throw new ArgumentException($"The URI template `{Template}` does not contain parameter: `{string.Join("`, `", missing)}`.");

                foreach (var parameter in parameters)
                {
                    var value = parameter.Value is DateTime time
                        ? time.ToString("O")
                        : parameter.Value;

                    template.SetParameter(parameter.Key, value);
                }
            }

            return template.Resolve();
        }
    }
}
