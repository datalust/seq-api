// Copyright 2014-2019 Datalust and contributors. 
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

namespace Seq.Api.Model.License
{
    public class LicenseEntity : Entity
    {
        public string LicenseText { get; set; }
        public bool IsValid { get; set; }
        public bool IsSingleUser { get; set; }
        public string StatusDescription { get; set; }
        public bool IsWarning { get; set; }
        public bool CanRenewOnlineNow { get; set; }
        public int? LicensedUsers { get; set; }
    }
}
