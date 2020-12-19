using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafetyTourism.Models
{
    public class Employee : IdentityUser
    {
        public string Name { get; set; }
       
        public string Address { get; set; }
       
    }
}