using GeoService.DAL.Enums;
using System;
using System.Collections.Generic;

namespace GeoService.BLL.DTO
{
    public class PointDTO
    {
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public float Altitude { get; set; }
        public TimeSpan CreateTime { get; set; }       
        public float? Accuracy { get; set; }
        public float? AltitudeAccuracy { get; set; }
        public float? Heading { get; set; }
        public float? Speed { get; set; }
        public ModeEnum Mode { get; set; }
    }

    public class UserPointDTO : PointDTO
    {
        public UserDTO User {get; set;}
    }

    public class SavePointDTO : PointDTO
    {
        public int ExperimentId { get; set; }
    }

    public class ListPointsDTO
    {
        public int ExperimentId { get; set; }
        public string ExperimentTitle { get; set; }
        public List<UserPointDTO> Points { get; set; }
    }

    public class ResponsePointsDTO
    {
        public Dictionary<string, string> UserImages { get; set; }
        public List<ListPointsDTO> ListPoint { get; set; }
    }
}
