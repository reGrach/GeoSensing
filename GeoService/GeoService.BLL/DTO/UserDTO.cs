using GeoService.DAL.Enums;

namespace GeoService.BLL.DTO
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public RoleEnum Role { get; set; }
        public string FullName { get; set; }
        public string TeamTitle { get; set; }
        public string TeamColor { get; set; }
    }
}
