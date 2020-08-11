using GeoService.DAL.Enums;
using System;

namespace GeoService.DAL
{
    public class GeoParameter
    {
        public int Id { get; set; }

        public float Latitude { get; set; }

        public float Longitude { get; set; }

        public float Altitude { get; set; }

        public TimeSpan CreateTime { get; set; }
        
        public ModeEnum Mode { get; set; }

        /// <summary> Точность измеренного положения [м]</summary>
        public float? Accuracy { get; set; }

        /// <summary> Точность измеренной высоты [м]</summary>
        public float? AltitudeAccuracy { get; set; }

        /// <summary> 
        /// Направление, в котором движется устройство. 
        /// Выражается в градусах (0 = север, восток = 90, юг = 180, запад = 270)
        /// </summary>
        public float? Heading { get; set; }

        public float? Speed { get; set; }

        /// <summary> Связь с создателем </summary>
        public int CreatorUserId { get; set; }

        /// <summary> Связь с экспериментом </summary>
        public int ExperimentId { get; set; }
        public virtual Experiment Experiment { get; set; }
    }
}
