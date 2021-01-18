using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestGeoZonesController
    {
        [Fact]
        public async Task GetAllGeoZonesAsync_ShouldReturnAllGeoZones()
        {
            Thread.Sleep(2000);
            var TestContext = TodoContextMocker.GetWHOContext("GetAllGeoZones");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.GetGeoZones();

            var items = Assert.IsType<List<GeoZone>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetGeoZoneAsync_ShouldReturnNotFound()
        {
            var TestContext = TodoContextMocker.GetWHOContext("GeoZonesReturnNotFound");
            var theController = new GeoZonesController(TestContext);

            var response = await theController.GetGeoZoneByID(6);

            Assert.IsType<NotFoundResult>(response.Result);
        }

        [Fact]
        public async Task GetGeoZoneByID_ShouldReturnGeoZoneByID()
        {
            Thread.Sleep(3000);
            var TestContext = TodoContextMocker.GetWHOContext("GetGeoZoneByID");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.GetGeoZoneByID(1);

            var items = Assert.IsType<GeoZone>(result.Value);
            Assert.Equal("Oceania", items.GeoZoneName);
        }

        [Fact]
        public async Task GetGeoZoneByID_ShouldReturnTheRightItemAsync()
        {
            var TestContext = TodoContextMocker.GetWHOContext("GetGeoZoneReturnRightItem");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.GetGeoZoneByID(1);
            var items = result.Value;

            //Assert     
            Assert.IsType<GeoZone>(items);
            Assert.Equal(1, items.GeoZoneID);
        }


        [Fact]
        public async Task PutGeoZone_ShouldReturnEditedGeoZone()
        {
            Thread.Sleep(3000);
            var TestContext = TodoContextMocker.GetWHOContext("PutGeoZone");
            var theController = new GeoZonesController(TestContext);

            var newGeoZone = new GeoZone
            {
                GeoZoneID = 1,
                GeoZoneName = "Asia",
            };

            var oldGeoZoneResult = await theController.GetGeoZoneByID(1);
            var oldGeoZone = oldGeoZoneResult.Value;

            TestContext.Entry(oldGeoZone).State = EntityState.Detached;

            var result = await theController.PutGeoZone(newGeoZone.GeoZoneID, newGeoZone);
            var getResult = await theController.GetGeoZoneByID(newGeoZone.GeoZoneID);

            var items = Assert.IsType<GeoZone>(getResult.Value);
            Assert.Equal("Asia", items.GeoZoneName);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutNoExistingGeoZoneAsync_ShouldReturnNotFound()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("EditGeoZone_ShouldReturnNotFound");
            var theController = new GeoZonesController(TestContext);
            var theNonExistingGeoZone = new GeoZone
            {
                GeoZoneID = 5,
                GeoZoneName = "Updated GeoZone"
            };

            var response = await theController.PutGeoZone(5, theNonExistingGeoZone);

            Assert.IsType<NotFoundResult>(response);
        }

        //[Fact]
        //public async Task PutBadNameGeoZoneAsync_ShouldReturnBadRequest()
        //{
        //    Thread.Sleep(4000);
        //    var TestContext = TodoContextMocker.GetWHOContext("EditGeoZone_ShouldReturnBadRequest");
        //    var theController = new GeoZonesController(TestContext);
        //    var theNonExistingGeoZone = new GeoZone
        //    {
        //        GeoZoneID = 5,
        //    };

        //    var g = await TestContext.FindAsync<GeoZone>(5);

        //    var response = await theController.PutGeoZone(5, theNonExistingGeoZone);

        //    Assert.IsType<BadRequestResult>(response);
        //}

        [Fact]
        public async Task PostGeoZone_ShouldCreateNewGeoZone()
        {
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetWHOContext("PostGeoZone");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.PostGeoZone(new GeoZone { GeoZoneID = 3, GeoZoneName = "America" });
            
            var addedGeoZoneResult = await theController.GetGeoZoneByID(3);

            var items = Assert.IsType<GeoZone>(addedGeoZoneResult.Value);
            Assert.Equal("America", items.GeoZoneName);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task DeleteGeoZoneByID_CantReturnGeoZoneByID()
        {
            Thread.Sleep(4000);

            var TestContext = TodoContextMocker.GetWHOContext("DeleteGeoZone");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.DeleteGeoZone(1);
            var getresult = await theController.GetGeoZoneByID(1);

            Assert.IsType<NotFoundResult>(getresult.Result);
        }

        [Fact]
        public async Task DeleteExistingGeoZoneAsync_ShouldReturnOkResult()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("DeleteGeoZone_OkResult");
            var theController = new GeoZonesController(TestContext);
            
            var response = await theController.DeleteGeoZone(2);

            Assert.IsType<NoContentResult>(response);
        }


    }
}
