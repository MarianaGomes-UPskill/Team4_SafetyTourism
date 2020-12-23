using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class OutBreak
    {
        public int OutBreakID { get; set; }

        [ForeignKey("Virus")]
        public int VirusID { get; set; }

        [ForeignKey("GeoZone")]
        public int GeoZoneID { get; set; }

        public DateTime StartDate { get; set; }

#nullable enable
        public DateTime? EndDate { get; set; }
#nullable disable
        public Virus Virus { get; set; }
        public GeoZone GeoZone { get; set; }
    }
}
