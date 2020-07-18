using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class Team
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        public string Color { get; set; }

        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        public NpgsqlPolygon? Polygon { get; set; }

        public virtual ICollection<User> Users { get; set; }

        public virtual ICollection<Experiment> Experiments { get; set; }
    }
}
