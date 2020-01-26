using GeoService.BLL.Enums;

namespace GeoService.BLL.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public RoleType Role { get; set; }
    }
}
