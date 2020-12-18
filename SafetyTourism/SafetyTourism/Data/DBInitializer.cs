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

            if (context.Destinations.Any())
            {
                return;   // DB has been seeded
            }

            var countries = new Country[]
            {
                    new Country{Name="Australia"},
                    new Country{Name="Spain"},
                    new Country{Name="Portugal"},
                    new Country{Name="India"},
                    new Country{Name="Italy",},
                    new Country{Name="Japan"},
                    new Country{Name="Angola"}
            };
            foreach (Country c in countries)
            {
                context.Countries.Add(c);
            }
            context.SaveChanges();

            // Look for any destinations.
            var destinations = new Destination[]
            {
                    new Destination{CountryID=1,Name="Sydney"},
                    new Destination{CountryID=2,Name="Madrid"},
                    new Destination{CountryID=3,Name="Lisbon"},
                    new Destination{CountryID=4,Name="New Dehli"},
                    new Destination{CountryID=5,Name="Rome",},
                    new Destination{CountryID=6,Name="Tokyo"},
                    new Destination{CountryID=7,Name="Luanda"}
            };
            foreach (Destination d in destinations)
            {
                context.Destinations.Add(d);
            }
            context.SaveChanges();

            var diseases = new Disease[]
            {
                    new Disease{Name="SARS-Cov2", Desc="Coronavirus disease 2019 (COVID-2019) is caused by a novel coronavirus known as Severe Acute Respiratory Syndrome Coronavirus 2 (SARS-CoV-2) and was identified as a pandemic by the World Health Organization (WHO) on March 11, 2020"},
                    new Disease{Name="Malaria", Desc="Malaria is caused by Plasmodium parasites, transmitted to humans through the bites of infected Anopheles mosquitoes.", Recommendation="Use insectiside"}
            };
            foreach (Disease d in diseases)
            {
                context.Diseases.Add(d);
            }
            context.SaveChanges();
        }
    }
}
