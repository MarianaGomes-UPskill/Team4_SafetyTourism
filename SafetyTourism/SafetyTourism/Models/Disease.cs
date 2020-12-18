using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Disease
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Desc { get; set; }
        public string? Recommendation { get; set; }
    }
}
