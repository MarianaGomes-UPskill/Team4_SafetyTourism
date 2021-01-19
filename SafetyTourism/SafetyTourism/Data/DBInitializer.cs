using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourism.Models;

namespace SafetyTourism.Data
{
    public class DBInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.EnsureCreated();

            
            context.SaveChanges();
        }

    }
}
