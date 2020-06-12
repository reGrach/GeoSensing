using GeoService.DAL.Enums;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;

namespace GeoService.API.Auth.Identity
{
    public static class UserAccountExtensions
    {
        public static ClaimsIdentity BuildClaims<T>(this T user, string role, TimeSpan expiration) where T : UserIdentity =>
            new ClaimsIdentity(new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString(), ClaimValueTypes.Integer32),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                new Claim(ClaimTypes.Expiration, expiration.ToString(), ClaimValueTypes.DaytimeDuration),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(), ClaimValueTypes.Integer64)
            }, "token");


        public static int GetUserId(this IIdentity identity)
        {
            if (identity is ClaimsIdentity ci)
            {
                var idClaim = ci.FindFirst(ClaimTypes.NameIdentifier).Value;
                if (int.TryParse(idClaim, out int id))
                    return id;
                else
                    throw new InvalidCastException("Id");
            }
            else
                throw new ArgumentNullException("Identity");
        }

        public static string GetUserLogin(this IIdentity identity)
        {
            if (identity is ClaimsIdentity ci)
                return ci.FindFirst(ClaimTypes.Name).Value;
            else
                throw new ArgumentNullException("Identity");
        }

        public static RoleEnum GetUserRole(this IIdentity identity)
        {
            RoleEnum role;
            if (identity is ClaimsIdentity ci)
            {
                var strRole = ci.FindFirst(ClaimsIdentity.DefaultRoleClaimType).Value;
                return Enum.TryParse(strRole, out role) ? role : throw new ArgumentException("role");
            }
            else
                throw new ArgumentNullException("Identity");
        }

        public static DateTime GetExpirationToken(this IIdentity identity)
        {
            if (identity is ClaimsIdentity ci)
            {
                var timeSpan = TimeSpan.Parse(ci.FindFirst(ClaimTypes.Expiration).Value);
                return DateTime.Now.AddHours(timeSpan.TotalHours);
            }
            else
                throw new ArgumentNullException("Identity");
        }
    }
}
