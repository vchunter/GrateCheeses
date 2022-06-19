using AutoFixture;
using AutoFixture.AutoMoq;
using GrateCheeses.Api.Models;
using GrateCheeses.Api.Repository;
using Moq;
using System.Collections.Generic;
using Xunit;

namespace GrateCheeses.Test
{
    public class CheeseDataTests
    {
        private IFixture _fixture;

        public CheeseDataTests()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
        }

        [Fact]
        public void GetAllCheeses_HasCheese_ShouldReturnListOfCheese()
        {
            var lotsOfCheese = _fixture.CreateMany<Cheese>(10);

            _fixture.Freeze<Mock<ICheeseData>>()
                .Setup(gc => gc.GetAllCheeses())
                .Returns(lotsOfCheese);

            var sut = _fixture.Create<ICheeseData>();

            var result = sut.GetAllCheeses();

            Assert.NotEmpty(result);
        }

        [Fact]
        public void GetAllCheeses_HasNoCheese_ShouldReturnEmptyList()
        {
            var noCheese = new List<Cheese>();

            _fixture.Freeze<Mock<ICheeseData>>()
                .Setup(gc => gc.GetAllCheeses())
                .Returns(noCheese);

            var sut = _fixture.Create<ICheeseData>();

            var result = sut.GetAllCheeses();

            Assert.Empty(result);
        }

        //[Fact]
        //public void GetCheeseById_Has
    }
}
