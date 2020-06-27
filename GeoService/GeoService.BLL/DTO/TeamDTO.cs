using GeoService.DAL;
using System.Collections.Generic;

namespace GeoService.BLL.DTO
{
    public class TeamDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Color { get; set; }       
    }

    public class TeamExtensionDTO : TeamDTO
    {
        public bool IsActive { get; set; }
        public List<UserDTO> Participants { get; set; }
        public PolygonTeam Polygon { get; set; }
    }    

    public class PolygonTeam
    {
        public double[] X { get; set; }

        public double[] Y { get; set; }

        public bool Isvalid => X.Length == Y.Length;
        public int Count => X.Length; 
    }
}
