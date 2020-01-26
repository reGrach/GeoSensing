using System;

namespace GeoService.DAL
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public short Role { get; set; }
    }
}
