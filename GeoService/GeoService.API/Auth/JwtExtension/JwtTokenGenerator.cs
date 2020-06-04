using GeoService.API.Auth.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Auth.JwtExtension
{
    public sealed class JwtTokenGenerator
    {
        private readonly JwtOptions _tokenOptions;
        public JwtTokenGenerator(JwtOptions tokenOptions)
        {
            _tokenOptions = tokenOptions 
                ?? throw new ArgumentNullException($"An instance of valid {nameof(JwtOptions)} must be passed in order to generate a JWT!");
        }

        public JwtTokenResult Generate(UserIdentity user, string role, bool rememberMe = false)
        {
            var expiration = rememberMe
                ? TimeSpan.FromDays(7)
                : TimeSpan.FromMinutes(_tokenOptions.TokenExpiryInMinutes);

            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: user.BuildClaims(role).Claims,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.Add(expiration),
                signingCredentials: new SigningCredentials(_tokenOptions.SigningKey, SecurityAlgorithms.HmacSha256));

            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new JwtTokenResult
            {
                AccessToken = accessToken,
                Expires = expiration
            };
        }
    }
}
