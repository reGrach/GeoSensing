namespace GeoService.API.Auth
{
    public class AuthOptions
    {
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public int LIFETIME { get; set; }
    }
}
