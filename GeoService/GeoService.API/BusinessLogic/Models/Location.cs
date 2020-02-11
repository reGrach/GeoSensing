namespace GeoService.API.BusinessLogic.Models
{
    public struct Location
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }

        public Location(float lat, float lon)
        {
            Latitude = lat;
            Longitude = lon;
        }
    }
}
