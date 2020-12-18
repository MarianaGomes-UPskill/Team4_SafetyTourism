using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using SafetyTourism.Models;
using Microsoft.AspNetCore.Identity;

namespace SafetyTourism.Models
{
    public class Employee : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }

    }
}
