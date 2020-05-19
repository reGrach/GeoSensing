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
        public List<UserDTO> Participants { get; set; }
        public double[][] Polygon { get; set; }
    }    
}
