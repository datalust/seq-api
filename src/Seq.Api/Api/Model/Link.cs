using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Tavis.UriTemplates;

namespace Seq.Api.Model
{
    public class Link
    {
        readonly bool _isLiteral;
        readonly string _href;
        readonly IDictionary<string, object> _parameters;

        public Link(string href, bool isLiteral = true)
            : this(href, new Dictionary<string, object>())
        {
            _isLiteral = isLiteral;
        }

        public Link(string href, IDictionary<string, object> parameters)
        {
            if (href == null) throw new ArgumentNullException("href");
            if (parameters == null) throw new ArgumentNullException("parameters");
            _href = href;
            _parameters = parameters;
        }

        public Link(string href, object parameters)
            : this(href, parameters
                .GetType()
                .GetTypeInfo()
                .DeclaredProperties
                .ToDictionary(p => p.Name, p => p.GetValue(parameters)))
        {
        }

        public string GetUri()
        {
            if (_isLiteral) return _href;

            var template = new UriTemplate(_href);
            foreach (var parameter in _parameters)
            {
                template.SetParameter(parameter.Key, parameter.Value);
            }
            return template.Resolve();
        }
    }
}