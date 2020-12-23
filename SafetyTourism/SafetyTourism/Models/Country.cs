using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Country
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }

        [ForeignKey("GeoZone")]
        [Display(Name = "GeoZone")]
        public int GeoZoneID { get; set; }
        
        public GeoZone GeoZone { get; set; }
    }
}
