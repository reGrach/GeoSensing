using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace GeoService.API.Auth.JwtExtension
{
    public sealed class JwtOptions
    {
        internal JwtOptions(string issuer, string audience, int tokenExpiryInMinutes, SecurityKey signingKey)
        {
            if (string.IsNullOrWhiteSpace(audience))
                throw new ArgumentNullException($"{nameof(Audience)} is required in order to generate a JWT!");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new ArgumentNullException($"{nameof(Issuer)} is required in order to generate a JWT!");

            Audience = audience;
            Issuer = issuer;
            SigningKey = signingKey ??
                         throw new ArgumentNullException($"{nameof(SigningKey)} is required in order to generate a JWT!");
            TokenExpiryInMinutes = tokenExpiryInMinutes;
        }

        public JwtOptions(string issuer, string audience, int tokenExpiryInMinutes, string rawSigningKey)
            : this(issuer, audience, tokenExpiryInMinutes, new SymmetricSecurityKey(Encoding.ASCII.GetBytes(rawSigningKey)))
        { }

        public SecurityKey SigningKey { get; }

        public string Issuer { get; }

        public string Audience { get; }

        public int TokenExpiryInMinutes { get; }
    }
}
