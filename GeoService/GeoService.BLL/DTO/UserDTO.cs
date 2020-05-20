namespace GeoService.BLL.DTO
{
    public class UserDTO
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public TeamDTO Team { get; set; }
        public bool IsParticipant => Team != null;
        public bool IsLeader { get; set; }
    }
}
