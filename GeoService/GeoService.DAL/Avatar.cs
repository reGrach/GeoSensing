using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class Avatar
    {
        public int Id { get; set; }

        [Required]
        public byte[] FileContent { get; set; }

        [Required]
        [MaxLength(50)]
        public string MimeType { get; set; }

        public virtual User User { get; set; }
    }
}
