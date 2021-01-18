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
            Thread.Sleep(3000);
            var TestContext = TodoContextMocker.GetWHOContext("GetAllGeoZones");
            var theController = new GeoZonesController(TestContext);

            var result = await theController.GetGeoZones();

            var items = Assert.IsType<List<GeoZone>>(result.Value);
            Assert.Equal(2, items.Count);
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

            Assert.IsType<NoContentResult>(result);
        }
    }
}
