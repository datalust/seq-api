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

using System.Collections.Generic;

namespace Seq.Api.Model.Security
{
    /// <summary>
    /// A role is a set of permissions designed to support a particular type of user.
    /// </summary>
    public class RoleEntity : Entity
    {
        /// <summary>
        /// The name of the role.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Permissions granted to users in the role.
        /// </summary>
        public HashSet<Permission> Permissions { get; set; } = new();
        
        /// <summary>
        /// Optionally, an extended description of the role.
        /// </summary>
        public string Description { get; set; }
    }
}
