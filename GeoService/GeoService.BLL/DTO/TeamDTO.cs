using System.Collections.Generic;

namespace GeoService.BLL.DTO
{
    public class TeamDTO
    {
        public string Title { get; set; }
        public List<UserDTO> Users { get; set; }
    }
}
