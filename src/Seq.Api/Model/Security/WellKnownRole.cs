using System;

namespace Seq.Api.Model.Security
{
    [Obsolete("It's recommended that new code look up roles by name in `SeqConnection.Roles`.")]
    public static class WellKnownRole
    {
        public const string AdministratorRoleId = "role-administrator";
        public const string UserRoleId = "role-user";
    }
}
