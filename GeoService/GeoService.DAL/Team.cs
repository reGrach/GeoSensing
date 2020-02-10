using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class Team
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Color { get; set; }
        public List<User> Users { get; set; }

        /// <summary> Связь с полигоном (один к одному) </summary>
        public Polygon Polygon { get; set; }
    }
}
