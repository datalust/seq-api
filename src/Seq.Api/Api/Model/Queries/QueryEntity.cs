using System.Collections.Generic;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Queries
{
    public class QueryEntity : Entity
    {
        public QueryEntity()
        {
            Title = "New Query";
            Requirements = new List<EventFilterPart>();
            PropertiesOfInterest = new List<PropertyOfInterestPart>();
            TaggedProperties = new List<TaggedPropertyPart>();
        }

        public string Title { get; set; }
        public List<EventFilterPart> Requirements { get; set; }
        public List<PropertyOfInterestPart> PropertiesOfInterest { get; set; }
        public List<TaggedPropertyPart> TaggedProperties { get; set; }
    }
}
