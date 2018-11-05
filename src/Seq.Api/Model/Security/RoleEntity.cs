using System.Collections.Generic;

namespace Seq.Api.Model.Security
{
    public class RoleEntity : Entity
    {
        public string Title { get; set; }
        public HashSet<Permission> Permissions { get; set; } = new HashSet<Permission>();
    }
}
