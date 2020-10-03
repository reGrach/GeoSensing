namespace GeoService.BLL.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string FullName => $"{SurName} {Name}";
        public string Name { get; set; }
        public string SurName { get; set; }
        public bool IsLeader { get; set; }
    }

    public class UserWithImgDTO : UserDTO
    {
        public string AvatarSrc { get; set; }
    }

    public class UserExtensionDTO : UserWithImgDTO
    {
        public int Id { get; set; }
        public TeamDTO Team { get; set; }
    }
}
