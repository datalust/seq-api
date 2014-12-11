using System.Collections.Generic;

namespace Seq.Api.Model.Users
{
    public class UserEntity : Seq.Api.Model.Entity
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string EmailAddress { get; set; }
        public string DefaultViewId { get; set; }
        public Dictionary<string, object> Preferences { get; set; }
        public bool IsAdministrator { get; set; }
        public string NewPassword { get; set; }
    }
}
