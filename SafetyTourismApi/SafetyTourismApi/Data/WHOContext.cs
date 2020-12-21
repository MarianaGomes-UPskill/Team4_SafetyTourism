using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Models;

namespace SafetyTourismApi.Data
{
    public class WHOContext : DbContext
    {
        public WHOContext(DbContextOptions<WHOContext> options)
            : base(options)
        {
        }

        public DbSet<Country> Countries { get; set; }

        public DbSet<GeoZone> GeoZones { get; set; }

        public DbSet<OutBreak> OutBreaks { get; set; }

        public DbSet<Recomendation> Recomendations { get; set; }

        public DbSet<Virus> Viruses { get; set; }
    }
}
