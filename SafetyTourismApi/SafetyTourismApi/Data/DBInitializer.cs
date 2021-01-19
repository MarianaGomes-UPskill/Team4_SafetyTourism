using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SafetyTourismApi.Models;

namespace SafetyTourismApi.Data {
    public class DBInitializer
    {
        public static void Initialize(WHOContext context)
        {
            context.Database.EnsureCreated();

            if (context.GeoZones.Any())
            {
                return; //DB is seeded
            }

            // Look for any destinations.
            var geozones = new GeoZone[]
            {
                    new GeoZone{GeoZoneName="Oceania"},
                    new GeoZone{GeoZoneName="Europe"},
                    new GeoZone{GeoZoneName="Asia"},
                    new GeoZone{GeoZoneName="Africa"},
                    new GeoZone{GeoZoneName="NorthAmerica"},
                    new GeoZone{GeoZoneName="SouthAmerica"},
                    new GeoZone{GeoZoneName="Caribean"},
                    new GeoZone{GeoZoneName="Pacific Islands"},
                    new GeoZone{GeoZoneName="Antarctica"}
            };
            foreach (GeoZone g in geozones)
            {
                context.GeoZones.Add(g);
            }
            context.SaveChanges();




            if (context.Countries.Any())
            {
                return;   // DB has been seeded
            }

            var countries = new Country[]
            {
                    new Country{CountryName="Australia", GeoZoneID=1},
                    new Country{CountryName="Spain", GeoZoneID=2},
                    new Country{CountryName="Portugal", GeoZoneID=2},
                    new Country{CountryName="India", GeoZoneID=3},
                    new Country{CountryName="Italy", GeoZoneID=2},
                    new Country{CountryName="Japan", GeoZoneID=3},
                    new Country{CountryName="Angola", GeoZoneID=4}
            };
            foreach (Country c in countries)
            {
                context.Countries.Add(c);
            }
            context.SaveChanges();



            var viruses = new Virus[]
            {
                    new Virus{VirusName="SARS-Cov2"},
                    new Virus{VirusName="Malaria"},
                    new Virus{VirusName="Gripe-H1N1"},
                    new Virus{VirusName="Lepra"},
                    new Virus{VirusName="Escorbuto"},
                    new Virus{VirusName="Varicela"},
                    new Virus{VirusName="Tuberculose"},
                    new Virus{VirusName="Febre Amarela"},
                    new Virus{VirusName="Febre Tifóide"},
                    new Virus{VirusName="Cólera"},
                    new Virus{VirusName="Escarlatina"},
                    new Virus{VirusName="Obesidade"}
            };
            foreach (Virus v in viruses)
            {
                context.Viruses.Add(v);
            }
            context.SaveChanges();

            var outbreaks = new OutBreak[]
            {
                    new OutBreak{GeoZoneID=1, VirusID=3, StartDate= DateTime.Parse("2020-02-22")},
                    new OutBreak{GeoZoneID=2, VirusID=2, StartDate= DateTime.Parse("2021-10-02")},
                    new OutBreak{GeoZoneID=2, VirusID=1, StartDate= DateTime.Parse("2022-11-11")},
                    new OutBreak{GeoZoneID=2, VirusID=8, StartDate= DateTime.Parse("2012-03-02")},
                    new OutBreak{GeoZoneID=3, VirusID=7, StartDate= DateTime.Parse("2000-05-06"), EndDate= DateTime.Parse("2009-08-20")},
                    new OutBreak{GeoZoneID=4, VirusID=5, StartDate= DateTime.Parse("2050-06-01")},
                    new OutBreak{GeoZoneID=7, VirusID=9, StartDate= DateTime.Parse("2003-08-02")},
                    new OutBreak{GeoZoneID=8, VirusID=6, StartDate= DateTime.Parse("2004-02-28")},
                    new OutBreak{GeoZoneID=4, VirusID=11,StartDate= DateTime.Parse("1995-01-21")},
                    new OutBreak{GeoZoneID=7, VirusID=9, StartDate= DateTime.Parse("1950-09-30")},
                    new OutBreak{GeoZoneID=8, VirusID=10,StartDate= DateTime.Parse("1989-10-25")},
                    new OutBreak{GeoZoneID=9, VirusID=12,StartDate= DateTime.Parse("1993-01-29"), EndDate= DateTime.Parse("1999-01-02")}
            };
            foreach (OutBreak o in outbreaks)
            {
                context.OutBreaks.Add(o);
            }
            context.SaveChanges();

            var recomendation = new Recomendation[]
{
                    new Recomendation{Note="Be careful", GeoZoneID=3, CreationDate= DateTime.Parse("2001-02-22"), ExpirationDate=20},
                    new Recomendation{Note="Don't drive", GeoZoneID=5, CreationDate= DateTime.Parse("1996-12-22"), ExpirationDate=1500},
                    new Recomendation{Note="Kill it with fire", GeoZoneID=6, CreationDate= DateTime.Parse("2008-02-25"), ExpirationDate=260}

    };
            foreach (Recomendation r in recomendation)
            {
                context.Recomendations.Add(r);
            }
            context.SaveChanges();
        }

    }
}
