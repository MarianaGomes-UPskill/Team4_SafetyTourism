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
    public class TestRecomendationController
    {
        [Fact]
        public async Task GetAllRecomendationAsync_ShouldReturnAllRecomendations()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("GetAllRecomendations");
            var theController = new RecomendationsController(TestContext);

            var result = await theController.GetRecomendations();

            var items = Assert.IsType<List<Recomendation>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetRecomendationByID_ShouldReturnRecomendationByID()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("GetRecomendationByID");
            var theController = new RecomendationsController(TestContext);

            var result = await theController.GetRecomendation(1);

            var items = Assert.IsType<Recomendation>(result.Value);
            Assert.Equal("Be careful", items.Note);
        }

        [Fact]
        public async Task GetRecomendationByID_ShouldReturnNotFound()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("NotFoundRecomendationByID");
            var theController = new RecomendationsController(TestContext);

            var result = await theController.GetRecomendation(0);

            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutRecomendation_ShouldReturnEditedRecomendation()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("PutRecomendation");
            var theController = new RecomendationsController(TestContext);

            var newRecomendation = new Recomendation
            {
                RecomendationID = 1,
                Note = "Be careful, Very Careful",
                GeoZoneID = 1,
                CreationDate = DateTime.Parse("2001 - 02 - 22"),
                ExpirationDate = 35
            };

            var oldRecomendationResult = await theController.GetRecomendation(1);
            var oldRecomendation = oldRecomendationResult.Value;

            TestContext.Entry(oldRecomendation).State = EntityState.Detached;

            var result = await theController.PutRecomendation(newRecomendation.RecomendationID, newRecomendation);
            var getResult = await theController.GetRecomendation(newRecomendation.RecomendationID);

            var items = Assert.IsType<Recomendation>(getResult.Value);
            Assert.Equal("Be careful, Very Careful", items.Note);
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task PutNoRecomendation_ShouldReturnNotFound()
        {
            Thread.Sleep(4000);
            var TestContext = TodoContextMocker.GetWHOContext("NotPutRecomendation");
            var theController = new RecomendationsController(TestContext);

            var newRecomendation = new Recomendation
            {
                RecomendationID = 0,
                Note = "Be careful, Very Careful",
                GeoZoneID = 1,
                CreationDate = DateTime.Parse("2001 - 02 - 22"),
                ExpirationDate = 35
            };

            var oldRecomendationResult = await theController.GetRecomendation(1);
            var oldRecomendation = oldRecomendationResult.Value;

            TestContext.Entry(oldRecomendation).State = EntityState.Detached;

            var result = await theController.PutRecomendation(newRecomendation.RecomendationID, newRecomendation);
            var getResult = await theController.GetRecomendation(newRecomendation.RecomendationID);

           
            Assert.IsType<NotFoundResult>(getResult.Result);
        }

        [Fact]
        public async Task PostRecomendation_ShouldCreateNewRecomendation()
        {
            Thread.Sleep(4000);

            var TestContext = TodoContextMocker.GetWHOContext("PostRecomendation");
            var theController = new RecomendationsController(TestContext);

            var newRecomendation = new Recomendation
            {
                RecomendationID = 3,
                Note = "Use mask",
                GeoZoneID = 1,
                CreationDate = DateTime.Parse("2019 - 12 - 22"),
                ExpirationDate = 20
            };
            var result = await theController.PostRecomendation(newRecomendation);
            var getResult = await theController.GetRecomendation(3);


            var items = Assert.IsType<Recomendation>(getResult.Value);
            Assert.Equal("Use mask", items.Note);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        [Fact]
        public async Task DeleteRecomendation_ShouldDeleteRecomendation()
        {
            Thread.Sleep(4500);
            var TestContext = TodoContextMocker.GetWHOContext("DeleteRecomendation");
            var theController = new RecomendationsController(TestContext);

            var result = await theController.DeleteRecomendation(1);
            
            Assert.IsType<NoContentResult>(result);
        }
        [Fact]
        public async Task DeleteNotExistRecomendation_ShouldReturnNotFound()
        {
            Thread.Sleep(4600);
            var TestContext = TodoContextMocker.GetWHOContext("NotFoundDeleteRecomendation");
            var theController = new RecomendationsController(TestContext);

            var result = await theController.DeleteRecomendation(10);

            Assert.IsType<NotFoundResult>(result);
        }
    }
}
