using GeoService.DAL.Enums;
using System;

namespace GeoService.BLL.DTO
{
    public class GeoParameterDTO
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public TimeSpan CreateTime { get; set; }
        public ModeEnum Mode { get; set; }
        public float? Accuracy { get; set; }
        public float? AltitudeAccuracy { get; set; }
        public float? Heading { get; set; }
        public float? Speed { get; set; }
        public int UserId { get; set; }
        public int ExperimentId { get; set; }
    }
}
