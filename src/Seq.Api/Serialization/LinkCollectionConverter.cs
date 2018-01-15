using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Seq.Api.Model;

namespace Seq.Api.Serialization
{
    public class LinkCollectionConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var lc = (LinkCollection)value;
            var dictionary = lc.ToDictionary(kv => kv.Key, kv => kv.Value.GetUri());
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