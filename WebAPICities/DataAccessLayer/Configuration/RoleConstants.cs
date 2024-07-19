using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Configuration
{
    public static class RoleConstants
    {
        public const string DriverRole = "Driver";
        public const string LogisticManagerRole = "LogisticManager";
        public const string AdminRole = "LogisticManager";

        public static IReadOnlyList<string> ApplicationRoles = new List<string>
        {
            DriverRole,
            LogisticManagerRole,
            AdminRole
        };
    }
}
