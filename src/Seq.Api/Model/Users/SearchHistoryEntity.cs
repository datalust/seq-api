using System.Collections.Generic;

namespace Seq.Api.Model.Users
{
    public class SearchHistoryEntity : Entity
    {
        public uint RetainedRecentSearches { get; set; }
        public List<string> Recent { get; set; }
        public List<string> Pinned { get; set; } 
    }
}