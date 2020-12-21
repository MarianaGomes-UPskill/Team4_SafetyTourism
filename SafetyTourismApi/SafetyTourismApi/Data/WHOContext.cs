using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SafetyTourismApi.Data
{
    public class WHOContext : DbContext
    {
            public WHOContext(DbContextOptions<WHOContext> options)
                : base(options)
            {
            }

            public DbSet<WHOContext> TodoItems { get; set; }
        }
    }
