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

using System.Collections.Generic;

namespace Seq.Api.Model.Apps
{
    /// <summary>
    /// Describes a setting exposed by instances of an <see cref="AppEntity"/>.
    /// </summary>
    public class AppSettingPart
    {
        /// <summary>
        /// The unique name identifying the setting.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// A friendly, descriptive name of the setting.
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Whether the setting is required in order for the app to function.
        /// </summary>
        public bool IsOptional { get; set; }

        /// <summary>
        /// Long-form description of the setting.
        /// </summary>
        public string HelpText { get; set; }

        /// <summary>
        /// The type of value accepted for the setting; valid values are <c>Text</c>, <c>Select</c>,
        /// <c>LongText</c>, <c>Checkbox</c>, <c>Integer</c>, <c>Decimal</c>, and <c>Password</c>.
        /// </summary>
        /// <remarks>An enum was historically not used here in order to improve
        /// forwards/backwards compatibility.</remarks>
        public string Type { get; set; }
        
        /// <summary>
        /// For settings of <see cref="Type"/> <c>Select</c>, a description of the values that can be chosen
        /// for the setting.
        /// </summary>
        public List<AppSettingValuePart> AllowedValues { get; set; } = new List<AppSettingValuePart>();

        /// <summary>
        /// If the setting value contains code in a programming or markup language, the
        /// language name.
        /// </summary>
        /// <remarks>Valid names are a subset of the names and aliases recognized by
        /// <a href="https://github.com/github/linguist/blob/master/lib/linguist/languages.yml">GitHub
        /// Linguist</a>. The generic value <c>code</c> will be specified if the language is non-specific but
        /// the value should be displayed in fixed-width font. Seq also recognizes the special Seq-specific
        /// <c>template</c> and <c>expression</c> syntaxes.</remarks>
        public string Syntax { get; set; }
    }
}
