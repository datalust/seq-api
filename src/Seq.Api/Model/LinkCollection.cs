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
using Newtonsoft.Json;
using Seq.Api.Serialization;

namespace Seq.Api.Model
{
    /// <summary>
    /// A collection of <see cref="Link"/>s indexed by case-insensitive name.
    /// </summary>
    [JsonConverter(typeof(LinkCollectionConverter))]
    public class LinkCollection : Dictionary<string, Link>
    {
        /// <summary>
        /// Construct a <see cref="LinkCollection"/>.
        /// </summary>
        public LinkCollection() : base(StringComparer.OrdinalIgnoreCase) { }
    }
}
