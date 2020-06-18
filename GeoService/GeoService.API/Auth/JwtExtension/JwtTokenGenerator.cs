using GeoService.API.Auth.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;

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

        public JwtTokenResult Generate(UserIdentity user, string role, bool rememberMe = false) =>
            Generate(user, role, rememberMe ? TimeSpan.FromDays(14) : TimeSpan.FromMinutes(_tokenOptions.TokenExpiryInMinutes));

        public JwtTokenResult Generate(UserIdentity user, string role, DateTime expired) =>
            Generate(user, role, (expired > DateTime.UtcNow ? expired.Subtract(DateTime.UtcNow) : TimeSpan.FromMinutes(_tokenOptions.TokenExpiryInMinutes)));

        public JwtTokenResult Generate(UserIdentity user, string role, TimeSpan expiration)
        {
            var jwt = new JwtSecurityToken(
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: user.BuildClaims(role, expiration).Claims,
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
