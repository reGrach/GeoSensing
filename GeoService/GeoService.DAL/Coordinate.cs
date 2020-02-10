using GeoService.DAL.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace GeoService.DAL
{
    public class Coordinate
    {
        public int Id { get; set; }
        [Required]
        public float Latitude { get; set; }
        [Required]
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        [Required]
        public DateTime CreateTime { get; set; }
        [Required]
        public ModeEnum Mode { get; set; }

        /// <summary> Точность измеренного положения [м]</summary>
        public float Accuracy { get; set; }
        /// <summary> Точность измеренной высоты [м]</summary>
        public float AltitudeAccuracy { get; set; }
        /// <summary> 
        /// Направление, в котором движется устройство. 
        /// Выражается в градусах (0 = север, восток = 90, юг = 180, запад = 270)
        /// </summary>
        public float Heading { get; set; }

        public float Speed { get; set; }

        /// <summary> Связь с пользователем </summary>
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
