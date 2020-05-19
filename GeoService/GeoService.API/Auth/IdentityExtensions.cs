using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace GeoService.API.Auth
{
    public static class IdentityExtensions
    {
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            string role = "";
            if (identity is ClaimsIdentity ci)
            {
                var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (id != null)
                    role = id.Value;
            }
            return role;
        }
    }
}
