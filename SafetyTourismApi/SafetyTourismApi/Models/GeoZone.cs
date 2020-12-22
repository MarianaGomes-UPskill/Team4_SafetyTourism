using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourismApi.Models
{
    public class GeoZone
    {
        public int GeoZoneID { get; set; }

        public string GeoZoneName { get; set; }

        public List<Country> Countries { get; set; }
    }
}
