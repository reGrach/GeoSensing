using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GeoService.API.Auth.Identity
{
    public static class UserAccountExtensions
    {
        public static ClaimsIdentity BuildClaims<T>(this T user, string role)
            where T : UserIdentity
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.TimeOfDay.Ticks.ToString(), ClaimValueTypes.Integer64),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role)
            };

            return new ClaimsIdentity(claims, "token");
        }
    }
}
