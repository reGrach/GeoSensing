using GeoService.DAL.Enums;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;

namespace GeoService.API.Models
{
    public class FixPointModel
    {
        public int ExperimentId { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public TimeSpan CreateTime { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public ModeEnum Mode { get; set; }
        public float? Accuracy { get; set; }
        public float? AltitudeAccuracy { get; set; }
        public float? Heading { get; set; }
        public float? Speed { get; set; }
    }
}
