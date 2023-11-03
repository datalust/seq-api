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
using System.Text.Json;
using System.Text.Json.Serialization;
using Seq.Api.Model;

namespace Seq.Api.Serialization
{
    class LinkCollectionConverter : JsonConverter<LinkCollection>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkCollection);
        }

        public override LinkCollection Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var hrefs = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader, options);            
            var result = new LinkCollection();
            if (hrefs != null)
            {
                foreach (var href in hrefs)
                {
                    result.Add(href.Key, new Link(href.Value));
                }
            }
            return result;
        }

        public override void Write(Utf8JsonWriter writer, LinkCollection value, JsonSerializerOptions options)
        {
            var dictionary = value.ToDictionary(kv => kv.Key, kv => kv.Value.Template);
            JsonSerializer.Serialize(writer, dictionary, options);
        }
    }
}