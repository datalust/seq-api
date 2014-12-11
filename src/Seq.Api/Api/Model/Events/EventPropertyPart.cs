using System;

namespace Seq.Api.Model.Events
{
    public class EventPropertyPart
    {
        public EventPropertyPart(string name, object value)
        {
            if (name == null) throw new ArgumentNullException("name");
            Name = name;
            Value = value;
        }

        public string Name { get; private set; }
        public object Value { get; private set; }
    }
}
