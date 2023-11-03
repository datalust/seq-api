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

using System.Text.Json.Serialization;

namespace Seq.Api.Model.Events
{
    /// <summary>
    /// A token parsed from a <a href="https://messagetemplates.org">message template</a>.
    /// </summary>
    public class MessageTemplateTokenPart
    {
        /// <summary>
        /// Plain text, if the token is a text token.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Text { get; set; }

        /// <summary>
        /// The name of the property to be rendered in place of the token, if a property token.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string PropertyName { get; set; }

        /// <summary>
        /// The raw source text from the message template (provided for both text and property tokens).
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RawText { get; set; }

        /// <summary>
        /// A pre-formatted value, if the token carries language-specific formatting directives.
        /// </summary>
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string FormattedValue { get; set; }
    }
}
