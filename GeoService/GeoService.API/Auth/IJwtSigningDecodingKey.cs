using Microsoft.IdentityModel.Tokens;

namespace GeoService.API.Auth
{
    public interface IJwtSigningDecodingKey
    {
        SecurityKey GetKey();
    }
}
