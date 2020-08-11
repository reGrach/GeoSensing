using System;

namespace GeoService.API.Auth.JwtExtension
{
    public sealed class JwtTokenResult
    {
        public string AccessToken { get; internal set; }

        public TimeSpan Expires { get; set; }
    }
}
