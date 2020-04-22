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
        /// The type of value accepted for the setting; valid values are <c>Text</c>,
        /// <c>LongText</c>, <c>Checkbox</c>, <c>Integer</c>, <c>Decimal</c>, and <c>Password</c>.
        /// </summary>
        /// <remarks>An enum was historically not used here in order to improve
        /// forwards/backwards compatibility.</remarks>
        public string Type { get; set; }
    }
}
