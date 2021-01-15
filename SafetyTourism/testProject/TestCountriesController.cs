using Microsoft.AspNetCore.Mvc;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestCountriesController
    {
        //[Fact]
        //public async Task GetAllCountriesAsync_ShouldReturnAllCountries()
        //{
        //    var TestContext = TodoContextMocker.GetWHOContext("WHOContext");
        //    var theController = new CountriesController(TestContext);

        //    var result = await theController.GetCountries();

        //    var items = Assert.IsType<List<Country>>(result.Value);
        //    Assert.Equal(2, items.Count);
        //}

        [Fact]
        public async Task GetCountryByID_ShouldReturnCountryByID()
        {
            var TestContext = TodoContextMocker.GetWHOContext("WHOContext");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountry(1);

            var country = Assert.IsType<ActionResult<Country>>(result);
            Assert.Equal(1, country.CountryID);
        }
    }
}
