using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourismApi.Models
{
    public class Country
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        public string CountryName { get; set; }

        [ForeignKey("GeoZone")]
        public int GeoZoneID { get; set; }

        public GeoZone GeoZone { get; set; }


    }
}
