using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Models;
using System.Threading.Tasks;
using Xunit;
using System.Threading;
using Microsoft.EntityFrameworkCore.InMemory;


namespace testProject
{
    public class TestController
    {
        
    

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync()
        {
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("Alheilha");
            var theController = new OutBreaksController(TestContext);
            //Act
            var result = await theController.GetOutBreaks();
               
            //Assert
            var items = Assert.IsType<List<OutBreak>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetOutbreakByID_ShouldReturnOutbreakByID()
        {
            Thread.Sleep(1000);
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("roca");
            var theController = new OutBreaksController(TestContext);
            //Act
            var result = await theController.GetOutBreak(1);

            //Assert
            var items = Assert.IsType<OutBreak>(result.Value);

            Assert.Equal(DateTime.Parse("2001 - 02 - 22"), items.StartDate);
          
        }

        [Fact]
        public async Task PosOutbreaAsync_ShouldCreatNewOutbreakAsync()
        {
            Thread.Sleep(500);
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("Alheilha3");
            var theController = new OutBreaksController(TestContext);

            //Act
            var result = await theController.PostOutBreak(new OutBreak { GeoZoneID = 2, VirusID = 2, StartDate = DateTime.Parse("2016-01-02") });
            var get = await theController.GetOutBreak(3);

            //Assert
            var items = Assert.IsType<OutBreak>(get.Value);
            Assert.Equal(2, items.VirusID);
            Assert.Equal(2, items.GeoZoneID);
            Assert.Equal(DateTime.Parse("2016-01-02"), items.StartDate);
            Assert.IsType<CreatedAtActionResult>(result.Result);


        }
        [Fact]
        public async Task PutOutbreakByIDAsync_ShouldUpdateOutbreakByIDAsync()
        {
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("Alheilha2");
            var theController = new OutBreaksController(TestContext);


            //Act
            var getOutbreak = await theController.GetOutBreak(2);
            var outBreak = getOutbreak.Value;
            outBreak.OutBreakID = 2;
            outBreak.GeoZoneID = 1;
            outBreak.VirusID = 1;
            outBreak.StartDate = DateTime.Parse("2021-12-1");
            outBreak.EndDate = DateTime.Parse("2021-12-31");
            var result = await theController.PutOutBreak(outBreak.OutBreakID, outBreak);
            var getresult = await theController.GetOutBreak(2);

            //Assert
            var items = Assert.IsType<OutBreak>(getresult.Value);
            Assert.Equal(1, items.VirusID);
            Assert.Equal(1, items.GeoZoneID);
            Assert.Equal(DateTime.Parse("2021-12-1"), items.StartDate);
            Assert.Equal(DateTime.Parse("2021-12-31"), items.EndDate);
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteOutbreakByID_CantReturnOutbreakByID()
        {
            Thread.Sleep(1000);
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("roca4");
            var theController = new OutBreaksController(TestContext);

            //Act
            var result = await theController.DeleteOutBreak(1);
            var getresult = await theController.GetOutBreak(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
            

        }

    }
}

