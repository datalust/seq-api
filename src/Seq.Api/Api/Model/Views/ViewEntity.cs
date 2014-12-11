using System.Collections.Generic;
using Seq.Api.Model.Shared;

namespace Seq.Api.Model.Views
{
    public class ViewEntity : Seq.Api.Model.Entity
    {
        public ViewEntity()
        {
            Title = "New View";
            Requirements = new List<EventFilterPart>();
        }

        public string Title { get; set; }
        public List<EventFilterPart> Requirements { get; set; }
    }
}
