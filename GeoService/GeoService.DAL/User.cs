using GeoService.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Salt { get; set; }

        [Required]
        [MaxLength(100)]
        public string PasswordHash { get; set; }

        [Required]
        public RoleEnum Role { get; set; }

        public int? AvatarId { get; set; }
        public Avatar Avatar { get; set; }

        /// <summary> Связь с командой </summary>
        public int? TeamId { get; set; }
        public Team Team { get; set; }
    }
}
