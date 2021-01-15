using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Models;
using System.Threading.Tasks;
using Xunit;

namespace testProject {
    //Outbreak Controller Test
    public class TestOutbreaksController {

        [Fact]
        public async Task GetAllCountriesAsync_ShouldReturnAllCountries() {

            var TestContext = TodoContextMocker.GetWHOContext("batat");
            var theController = new CountriesController(TestContext);

            var result = await theController.GetCountries();

            var items = Assert.IsType<List<Country>>(result.Value);
            Assert.Equal(2, items.Count);

        }

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync() {
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("Alheilha");
            var theController = new OutBreaksController(TestContext);
            //Act
            var result = await theController.GetOutBreaks();

            //Assert
            var items = Assert.IsType<List<OutBreak>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        //Virus Controller Test
        [Fact]
        public async Task GetAllVirusAsync_ShouldReturnAllVirus() {
            var TestContext = TodoContextMocker.GetWHOContext("GetAllViruses");
            var theController = new VirusesController(TestContext);

            var result = await theController.GetViruses();

            var items = Assert.IsType<List<Virus>>(result.Value);
            Assert.Equal(2, items.Count);
        }


        [Fact]
        public async Task GetVirusByID_ShouldReturnVirusByID() {
            var TestContext = TodoContextMocker.GetWHOContext("GetVirusByID");
            var theController = new VirusesController(TestContext);

            var result = await theController.GetVirus(1);

            var items = Assert.IsType<Virus>(result.Value);
            Assert.Equal("SARS-Cov2", items.VirusName);
        }

        [Fact]
        public async Task PutVirusAsync_ShouldReturnAlteredVirus() {
            var TestContext = TodoContextMocker.GetWHOContext("GetChangedVirus");
            var theController = new VirusesController(TestContext);
            var id = 2;
            var viruses = new Virus { VirusID = 2, VirusName = "Ebola" };

            var result = await theController.PutVirus(id, viruses);

            Assert.IsType<CreatedAtActionResult>(result);
        }

    }
}
