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

        /// <summary> Связь с создателем </summary>
        public int CreatorUserId { get; set; }

        /// <summary> Связь с создателем </summary>
        public int TeamId { get; set; }
        public Team Team { get; set; }

        public List<GeoParameter> Coordinates { get; set; }
    }
}
