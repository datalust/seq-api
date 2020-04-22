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

namespace Seq.Api.Model.Settings
{
    /// <summary>
    /// A Seq system-level setting. Note that only runtime-modifiable
    /// settings are exposed this way. Other configuration options are
    /// set using the Seq server command-line.
    /// </summary>
    public class SettingEntity : Entity
    {
        /// <summary>
        /// The name of the setting. See <see cref="SettingName"/> for a selection
        /// of current values.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The value of the setting.
        /// </summary>
        public object Value { get; set; }
    }
}
