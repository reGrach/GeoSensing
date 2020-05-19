using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace GeoService.API.Auth.Identity
{
    public static class UserAccountExtensions
    {
        public static ClaimsIdentity BuildClaims<T>(this T user, string role) where T : UserIdentity =>
            new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(), ClaimValueTypes.Integer64)
            }, "token");


        public static int GetUserId(this IIdentity identity)
        {
            if (identity == null)
                throw new ArgumentNullException("Identity");

            if (identity is ClaimsIdentity ci)
            {
                var idClaim = ci.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (int.TryParse(idClaim, out int id))              
                    return id;
            }
            return default;
        }
    }
}
