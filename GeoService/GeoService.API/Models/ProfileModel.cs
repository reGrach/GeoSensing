using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoService.API.Models
{
    public class ProfileModel
    {
        public string Login { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }


        public string FullName => $"{SurName} {Name}";
    }
}
