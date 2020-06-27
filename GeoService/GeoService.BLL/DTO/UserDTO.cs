namespace GeoService.BLL.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string AvatarSrc { get; set; }
        public bool IsLeader { get; set; }
    }

    public class UserExtensionDTO : UserDTO
    {
        public int Id { get; set; }
        public TeamDTO Team { get; set; }
    }
}
