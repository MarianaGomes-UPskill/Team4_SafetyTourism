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
    public class TestVirusController
    {
     
        [Fact]
        public async Task GetAllVirusesAsync_ShouldReturnAllViruses()
        {
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetWHOContext("GetAllViruses");
            var theController = new VirusesController(TestContext);

            var result = await theController.GetViruses();

            var items = Assert.IsType<List<Virus>>(result.Value);
            Assert.Equal(2, items.Count);
        }

        [Fact]
        public async Task GetVirusByID_ShouldReturnVirusByID()
        {
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetWHOContext("GetVirusByID");
            var theController = new VirusesController(TestContext);

            var result = await theController.GetVirus(1);

            var items = Assert.IsType<Virus>(result.Value);
            Assert.Equal("SARS-Cov2", items.VirusName);
        }


        [Fact]
        public async Task PutVirus_ShouldReturnEditedVirus()
        {
            Thread.Sleep(3500);
            var TestContext = TodoContextMocker.GetWHOContext("PutVirus");
            var theController = new VirusesController(TestContext);

            var newVirus = new Virus
            {
                VirusID = 1,
                VirusName = "Ebola"
            };

            var oldVirusResult = await theController.GetVirus(1);
            var oldVirus = oldVirusResult.Value;

            TestContext.Entry(oldVirus).State = EntityState.Detached;

            var result = await theController.PutVirus(newVirus.VirusID, newVirus);
            var getResult = await theController.GetVirus(newVirus.VirusID);

            var items = Assert.IsType<Virus>(getResult.Value);
            Assert.Equal("Ebola", items.VirusName);
            Assert.IsType<NoContentResult>(result); 
        }

        [Fact]
        public async Task PutNoExistingVirusAsync_ShouldReturnNotFound() {
            // Arrange
            Thread.Sleep(3500);
            var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
            var testContext = TodoContextMocker.GetWHOContext(dbName);
            var theController = new VirusesController(testContext);
            var testCod = 20;
            var theNonExistingVirus = new Virus {
                VirusID = testCod,
                VirusName = "Updated Virus " + testCod
            };

            // Act
            var response = await theController.PutVirus(testCod, theNonExistingVirus);

            // Assert
            Assert.IsType<NotFoundResult>(response);
        }

        [Fact]
        public async Task PostVirusAsync_ShouldCreatNewVirusAsync() {
            Thread.Sleep(3500);
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("PostVirus");
            var theController = new VirusesController(TestContext);

            //Act
            var result = await theController.PostVirus(new Virus { VirusID = 3, VirusName = "Tuberculosis" });
            var addedVirusResult = await theController.GetVirus(3);

            //Assert
            var items = Assert.IsType<Virus>(addedVirusResult.Value);
            Assert.Equal("Tuberculosis", items.VirusName);
            Assert.IsType<CreatedAtActionResult>(result.Result);
        }

        //[Fact]
        //public async Task PostBadNoNameVirusAsync_ShouldReturnBadRequest() {
        //    // Arrange
        //    var dbName = System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.Name;
        //    var TestContext = TodoContextMocker.GetWHOContext(dbName);
        //    var theController = new VirusesController(TestContext);

        //    var notValidCode = null;
        //    var noNameVirus = new Virus {
        //        VirusID = notValidCode, VirusName = "XYZ"
        //    };
        //    theController.ModelState.AddModelError("Name", "Required");

        //    // Act
        //    var response = await theController.PostVirus(noNameVirus);
        
        //// Assert
        //    Assert.IsType<BadRequestObjectResult>(response.Result);
        //}


        [Fact]
        public async Task DeleteVirusByID_CantReturnVirusByID() {
            Thread.Sleep(3500);
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("DeleteVirus");
            var theController = new VirusesController(TestContext);

            //Act
            var result = await theController.DeleteVirus(1);
            var getresult = await theController.GetVirus(1);

            //Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}