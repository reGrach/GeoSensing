using Microsoft.IdentityModel.Tokens;
namespace GeoService.API.Auth
{
    public interface IJwtSigningEncodingKey
    {
        string SigningAlgorithm { get; }
        SecurityKey GetKey();
    }
}
