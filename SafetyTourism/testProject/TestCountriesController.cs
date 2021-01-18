using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Data;
using SafetyTourismApi.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestCountriesController
    {
     
        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnAllCountries()
        {
            Thread.Sleep(2000);
            var TestContext = TodoContextMocker.GetWHOContext("GetAllCountries");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountries();

            var items = Assert.IsType<List<Country>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetCountryByID_ShouldReturnCountryByID()
        {
            Thread.Sleep(2000);
            var TestContext = TodoContextMocker.GetWHOContext("GetCountryByID");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountry(1);

            var items = Assert.IsType<Country>(result.Value);
            Assert.Equal("Australia", items.CountryName);
        }

        [Fact]
        public async Task PutCountry_ShouldReturnEditedCountry()
        {
            Thread.Sleep(2000);
            var TestContext = TodoContextMocker.GetWHOContext("PutCountry");
            var theController = new CountriesController(TestContext);

            var oldCountryResult = await theController.GetCountry(1);
            var oldCountry = oldCountryResult.Value;
            oldCountry.CountryID = 1;   
            oldCountry.CountryName = "China";
            oldCountry.GeoZoneID = 1;

            var result = await theController.PutCountry(oldCountry.CountryID, oldCountry);
            var getResult = await theController.GetCountry(1);

            var items = Assert.IsType<Country>(getResult.Value);
            Assert.Equal("China", items.CountryName);
            Assert.Equal(1, items.CountryID);
            Assert.Equal(1, items.GeoZoneID);
            Assert.IsType<NoContentResult>(result); 
        }

        [Fact]
        public async Task PostCountry_ShouldCreateNewCountry()
        {

            var TestContext = TodoContextMocker.GetWHOContext("PostCountry");
            var theController = new CountriesController(TestContext);

            var newCountry = new Country();
            newCountry.CountryID = 3;
            newCountry.CountryName = "Islândia";
            newCountry.GeoZoneID = 1;

            var result = await theController.PostCountry(newCountry);
            var getResult = await theController.GetCountry(3);

            var items = Assert.IsType<Country>(getResult.Value);
            Assert.Equal("Islândia", items.CountryName);
            Assert.Equal(3, items.CountryID);
            Assert.Equal(1, items.GeoZoneID);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task DeleteCountry_ShouldDeleteCountry()
        {
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetWHOContext("DeleteCountry");
            var theController = new CountriesController(TestContext);

            var result = await theController.DeleteCountry(1);
            var getResult = await theController.GetCountry(1);

            Assert.IsType<NotFoundResult>(result);
        }



    }
}
