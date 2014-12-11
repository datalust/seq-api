using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Seq.Api.Serialization;

namespace Seq.Api.Model
{
    [JsonConverter(typeof(LinkCollectionConverter))]
    public class LinkCollection : Dictionary<string, Link>
    {
        public LinkCollection() : base(StringComparer.OrdinalIgnoreCase) { }

        public string GetUri(string name)
        {
            return this[name].GetUri();
        }
    }
}
