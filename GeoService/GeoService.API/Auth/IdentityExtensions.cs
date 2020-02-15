using System;
using System.Globalization;
using System.Security.Claims;
using System.Security.Principal;

namespace GeoService.API.Auth
{
    public static class IdentityExtensions
    {
        public static int GetUserId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("identity");           

            if (identity is ClaimsIdentity ci)
            {
                var id = ci.FindFirst(ClaimTypes.NameIdentifier);
                if (id != null)
                {
                    return int.Parse(id.Value);
                }
            }
            return default;
        }
        public static string GetUserRole(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            var ci = identity as ClaimsIdentity;
            string role = "";
            if (ci != null)
            {
                var id = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType);
                if (id != null)
                    role = id.Value;
            }
            return role;
        }
    }
}
