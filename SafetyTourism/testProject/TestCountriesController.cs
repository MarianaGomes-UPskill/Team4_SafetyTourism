using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Data;
using SafetyTourismApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestCountriesController
    {
     
        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnAllCountries()
        {
            var TestContext = TodoContextMocker.GetWHOContext("GetAllCountries");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountries();

            var items = Assert.IsType<List<Country>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetCountryByID_ShouldReturnCountryByID()
        {
            var TestContext = TodoContextMocker.GetWHOContext("GetCountryByID");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountry(1);

            var items = Assert.IsType<Country>(result.Value);
            Assert.Equal("Australia", items.CountryName);
        }

        [Fact]
        public async Task PutCountry_ShouldReturnEditedCountry()
        {
            var TestContext = TodoContextMocker.GetWHOContext("PutCountry");
            var theController = new CountriesController(TestContext);

            var newCountry = new Country
            {
                CountryID = 1,
                CountryName = "China",
                GeoZoneID = 1
            };

            var oldCountryResult = await theController.GetCountry(1);
            var oldCountry = oldCountryResult.Value;

            TestContext.Entry(oldCountry).State = EntityState.Detached;

            var result = await theController.PutCountry(newCountry.CountryID, newCountry);
            var getResult = await theController.GetCountry(newCountry.CountryID);

            var items = Assert.IsType<Country>(getResult.Value);
            Assert.Equal("China", items.CountryName);
            Assert.IsType<NoContentResult>(result); 
        }

        [Fact]
        public async Task PostCountry_ShouldCreateNewCountry()
        {
            var TestContext = TodoContextMocker.GetWHOContext("PostCountry");
            var theController = new CountriesController(TestContext);

            var newCountry = new Country
            {
                CountryID = 3,
                CountryName = "Islândia",
                GeoZoneID = 1
            };
            var addedCountryResult = await theController.GetCountry(3);
            var addedCountry = addedCountryResult.Value;

            TestContext.Entry(addedCountry).State = EntityState.Detached;

            var result = await theController.PostCountry(newCountry);
            var items = Assert.IsType<Country>(result.Value);
            Assert.Equal("Islândia", items.CountryName);
            Assert.IsType<CreatedAtActionResult>(result);
        }


    }
}
