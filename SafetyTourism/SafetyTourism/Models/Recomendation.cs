﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Recomendation
    {
        public int RecomendationID { get; set; }
        public string Note { get; set; }
        [ForeignKey("GeoZone")]
        public int GeoZoneID { get; set; }
        public DateTime CreationDate { get; set; }

        public int ExpirationDate { get; set; }
        public GeoZone GeoZone { get; set; }
    }   
}
