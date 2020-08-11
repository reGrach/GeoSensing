using System;

namespace GeoService.BLL.DTO
{
    public class ExperimentDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }
        public bool IsMyInit { get; set; }
    }
}
