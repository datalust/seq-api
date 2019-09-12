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

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Seq.Api.Model;

namespace Seq.Api.Serialization
{
    class LinkCollectionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var lc = (LinkCollection)value;
            var dictionary = lc.ToDictionary(kv => kv.Key, kv => kv.Value.Template);
            serializer.Serialize(writer, dictionary);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var hrefs = serializer.Deserialize<Dictionary<string, string>>(reader);
            if (hrefs == null) return existingValue;
            var result = new LinkCollection();
            foreach (var href in hrefs)
            {
                result.Add(href.Key, new Link(href.Value));
            }
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(LinkCollection);
        }
    }
}