namespace GeoService.API.Models
{
    public class CreateTeamModel
    {
        public string Title { get; set; }
        public string Color { get; set; }
    }

    public class PolygonTeamModel
    {
        public int Id { get; set; }
        public double[] PointsX { get; set; }
        public double[] PointsY { get; set; }
    }

}
