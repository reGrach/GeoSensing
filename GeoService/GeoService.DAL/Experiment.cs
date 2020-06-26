using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class Experiment
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required]
        public DateTime CreateDate { get; set; }

        public bool IsActive { get; set; }

        /// <summary> Связь с создателем </summary>
        public int CreatorUserId { get; set; }

        /// <summary> Связь с командой </summary>
        public int TeamId { get; set; }
        public virtual Team Team { get; set; }

        public virtual ICollection<GeoParameter> Coordinates { get; set; }
    }
}
