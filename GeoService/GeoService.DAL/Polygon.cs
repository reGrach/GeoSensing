namespace GeoService.DAL
{
    public class Polygon
    {
        public int Id { get; set; }
        public float[] Points { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
