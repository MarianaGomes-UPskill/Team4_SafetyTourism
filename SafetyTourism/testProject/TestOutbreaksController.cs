using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using SafetyTourismApi.Controllers;
using SafetyTourismApi.Models;
using System.Threading.Tasks;
using Xunit;

namespace testProject
{
    public class TestOutbreaksController
    {
  

        [Fact]
        public async Task GetAllItemsAsync_ShouldReturnAllItemsAsync()
        {
            //Arrange
            var TestContext = TodoContextMocker.GetWHOContext("WHOContext");
            var theController = new OutBreaksController(TestContext);
            //Act
            var result = await theController.GetOutBreaks();
               
            //Assert
            var items = Assert.IsType<List<OutBreak>>(result.Value);
            Assert.Equal(2, items.Count);
        }

    }
}
