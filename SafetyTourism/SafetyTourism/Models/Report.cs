using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Report
    {
        public int ID { get; set; }
        [Display(Name = "Number of Infected People")]
        public long NumInfected { get; set; }

        [Display(Name = "Report's Publishing Date")]
        public DateTime CreationDate = DateTime.Now;

        [ForeignKey("Destination")]
        [Display(Name = "Destination")]
        public int DestinationID { get; set; }

        [ForeignKey("Disease")]
        [Display(Name = "Disease")]
        public int DiseaseID { get; set; }

        public Disease Disease { get; set; }
        public Destination Destination { get; set; }
    }
}
