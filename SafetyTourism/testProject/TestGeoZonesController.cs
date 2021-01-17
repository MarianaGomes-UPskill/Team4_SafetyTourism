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
    }
}
