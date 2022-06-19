using AutoFixture;
using AutoFixture.AutoMoq;
using GrateCheeses.Api.Models;
using GrateCheeses.Api.Controllers;
using GrateCheeses.Api.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace GrateCheeses.Test
{
    public class GrateCheesesApiControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<ILogger<GrateCheesesController>> _logger;
        private readonly Mock<ICheeseData> _cheeseData;
        private readonly GrateCheesesController _controller;

        public GrateCheesesApiControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _logger = new Mock<ILogger<GrateCheesesController>>();
            _cheeseData = new Mock<ICheeseData>();

            _controller = new GrateCheesesController(_logger.Object, _cheeseData.Object);

        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            //Arrange
            // mock the GetAllCheeses data call to return a list of 10 cheeses 
            var lotsOfCheese = _fixture.CreateMany<Cheese>(10);

            _cheeseData.Setup(x => x.GetAllCheeses()).Returns(lotsOfCheese);

            //Act
            // call the Get api endpoint on the controller
            var cheesyResult = _controller.Get();

            //Assert
            // verify that the api endpoint returns a successful response
            var statusResult = cheesyResult.Result as OkObjectResult;

            // verify that the results contained 10 cheeses in the list
            var items = (IEnumerable<Cheese>)statusResult.Value;
            Assert.Equal(10, items.Count());
        }

        [Fact]
        //
        public void Get_WhenCalled_ReturnsBadResult()
        {
            //Arrange
            // mock the GetAllCheeses data throw a Null Reference Exception
            _cheeseData.Setup(x => x.GetAllCheeses()).Throws<NullReferenceException>();

            //Act
            // call the Get api endpoint on the controller
            var cheesyResult = _controller.Get();

            //Assert
            // verify that the api endpoint returns a bad response
            var statusResult = cheesyResult.Result as BadRequestResult;
            Assert.Equal(400, statusResult.StatusCode);
        }
    }
}
