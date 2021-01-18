using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Data;
using SafetyTourismApi.Models;
using System;

namespace testProject
{
    public static class TodoContextMocker
    {
        private static WHOContext dbContext;

        public static WHOContext GetWHOContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<WHOContext>()
                .UseInMemoryDatabase(databaseName: dbName)
                .Options;

            dbContext = new WHOContext(options);
            Seed();
            return dbContext;
        }

        private static void Seed()
        {
            dbContext.Countries.Add(new Country { CountryName = "Australia", GeoZoneID = 1 });
            dbContext.Countries.Add(new Country { CountryName = "Spain", GeoZoneID = 2 });
            
            dbContext.GeoZones.Add(new GeoZone { GeoZoneID = 1, GeoZoneName = "Oceania" });
            dbContext.GeoZones.Add(new GeoZone { GeoZoneID = 2, GeoZoneName = "Europe" });

            dbContext.Viruses.Add(new Virus { VirusName = "SARS-Cov2" });
            dbContext.Viruses.Add(new Virus { VirusName = "Malaria" });

            dbContext.OutBreaks.Add(new OutBreak { GeoZoneID = 1, VirusID = 1, StartDate = DateTime.Parse("2001 - 02 - 22") });
            dbContext.OutBreaks.Add(new OutBreak { GeoZoneID = 2, VirusID = 2, StartDate = DateTime.Parse("2001 - 02 - 25") });

            dbContext.Recomendations.Add(new Recomendation { Note = "Be careful", GeoZoneID = 1, CreationDate = DateTime.Parse("2001 - 02 - 22"), ExpirationDate = 20 });
            dbContext.Recomendations.Add(new Recomendation { Note = "Don't drive", GeoZoneID = 2, CreationDate = DateTime.Parse("1996 - 12 - 22"), ExpirationDate = 1500 });
            dbContext.SaveChanges();

        }

    }
}
