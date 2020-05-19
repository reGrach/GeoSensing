namespace GeoService.BLL.DTO
{
    public class IdentityDTO
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public DAL.Enums.RoleEnum Role { get; set; }
    }
}
