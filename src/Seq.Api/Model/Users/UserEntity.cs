using System.Collections.Generic;
using Seq.Api.Model.Signals;

namespace Seq.Api.Model.Users
{
    public class UserEntity : Entity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
        public string NewPassword { get; set; }
        public SignalFilterPart ViewFilter { get; set; }
        public bool MustChangePassword { get; set; }
        public HashSet<string> RoleIds { get; set; } = new HashSet<string>();
    }
}
