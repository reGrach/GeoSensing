using GeoService.DAL.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        [Required]
        public string Salt { get; set; }
        [Required]
        public string PasswordHash { get; set; }
        [Required]
        public RoleEnum Role { get; set; }

        /// <summary> Связь с командой </summary>
        public int? TeamId { get; set; }
        public Team Team { get; set; }

        public List<Coordinate> Coordinates { get; set; }
    }
}
