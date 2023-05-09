//using Microsoft.Extensions.Logging;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using WeatherApp.Api.HttpClients.Interfaces;
//using WeatherApp.Api.Models;
//using WeatherApp.Api.Services;
//using WeatherApp.Api.Services.Interfaces;
//using WeatherApp.Infrastructure.Interfaces;
//using WeatherApp.Infrastructure.Models;

//namespace WeatherApp.Tests.ServiceTests
//{
//    public class FavouriteWeatherLocationsServiceTests
//    {
//        public FavouriteWeatherLocationsServiceTests()
//        {
//        }

//        [Fact]
//        public async Task GetFavouriteLocationsAsync_ReturnsFavouriteLocations()
//        {
//            //arrange
//            var logger = new Mock<ILogger<FavouriteWeatherLocationsService>>();
//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetAllFavouritesAsync()).ReturnsAsync(
//                new List<FavouriteLocationDto>()
//                {
//                    new FavouriteLocationDto()
//                    {
//                        Id = Guid.NewGuid(),
//                        Latitude = 51.51f,
//                        Longitude = -0.13f
//                    }
//                });

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger.Object);

//            //act
//            var result = await sut.GetFavouriteLocationsAsync();

//            //assert
//            Assert.NotNull(result);
//            Assert.NotEmpty(result);
//            Assert.IsType<List<FavouriteLocation>>(result);
//        }

//        [Fact]
//        public async Task GetFavouriteLocationsAsync_ThrowsException()
//        {
//            //arrange
//            var logger = new Mock<ILogger<FavouriteWeatherLocationsService>>();
//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetAllFavouritesAsync()).ThrowsAsync(new Exception("Test Exception"));

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger.Object);

//            //act
//            Func<Task> act = () => sut.GetFavouriteLocationsAsync();

//            //assert
//            Exception ex = await Assert.ThrowsAsync<Exception>(act);
//            Assert.Contains("Test Exception", ex.Message);
//        }

//        [Fact]
//        public async Task GetFavouriteLocationsAsync_EmptyCollection()
//        {
//            //arrange
//            var logger = new Mock<ILogger<FavouriteWeatherLocationsService>>();
//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetAllFavouritesAsync()).ReturnsAsync(new List<FavouriteLocationDto>());

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger.Object);

//            //act
//            var result = await sut.GetFavouriteLocationsAsync();

//            //assert
//            Assert.Empty(result);
//        }

//        [Fact]
//        public async Task GetFavouriteLocationByIdAsync_ReturnsFavouriteLocation()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
//                new FavouriteLocationDto()
//                {
//                    Id = Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),
//                    Latitude = 51.51f,
//                    Longitude = -0.13f,
//                    LocationName = "London"

//                });

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.GetFavouriteLocationByIdAsync(Guid.NewGuid());

//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<FavouriteLocation>(result);
//            Assert.Equal("London", result.LocationName);
//            Assert.Equal(51.51f, result.Latitude);
//            Assert.Equal(-0.13f, result.Longitude);
//            Assert.Equal(Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"), result.Id);
//        }

//        [Fact]
//        public async Task GetFavouriteLocationByIdAsync_ThrowsException()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception("Test Exception"));

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            Func<Task> act = () => sut.GetFavouriteLocationByIdAsync(Guid.NewGuid());


//            //assert
//            Exception ex = await Assert.ThrowsAsync<Exception>(act);
//            Assert.Contains("Test Exception", ex.Message);
//        }

//        [Fact]
//        public async Task GetFavouriteLocationByIdAsync_ReturnsNull()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
//                (FavouriteLocationDto)null);

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.GetFavouriteLocationByIdAsync(Guid.NewGuid());

//            //assert
//            Assert.Null(result);
//        }

//        [Fact]
//        public async Task AddFavouriteLocationAsync_ReturnsFavouriteLocation()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.AddFavouriteAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(
//                new FavouriteLocationDto()
//                {
//                    Id = Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),
//                    Latitude = 51.51f,
//                    Longitude = -0.13f,
//                    LocationName = "London"
//                });

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.AddToFavouritesAsync(51.51f, 0.13f, "London");


//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<FavouriteLocation>(result);
//            Assert.Equal("London", result.LocationName);
//            Assert.Equal(51.51f, result.Latitude);
//            Assert.Equal(-0.13f, result.Longitude);
//            Assert.Equal(Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"), result.Id);
//        }

//        [Fact]
//        public async Task AddFavouriteLocationAsync_ThrowsException()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.AddFavouriteAsync(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).ThrowsAsync(new Exception("Test Exception"));

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            Func<Task> act = () => sut.AddToFavouritesAsync(51.51f, 0.13f, "London");

//            //assert
//            Exception ex = await Assert.ThrowsAsync<Exception>(act);
//            Assert.Contains("Test Exception", ex.Message);
//        }


//        [Fact]
//        public async Task DeleteFavouriteLocationAsync_ReturnsTrue()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
//               new FavouriteLocationDto()
//               {
//                   Id = Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),
//                   Latitude = 51.51f,
//                   Longitude = -0.13f,
//                   LocationName = "London"
//               });

//            mockFavouriteLocationsRepository.Setup(s => s.DeleteFavouriteAsync(It.IsAny<FavouriteLocationDto>())).ReturnsAsync(true);

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.RemoveFromFavouritesAync(Guid.NewGuid());

//            //assert
//            mockFavouriteLocationsRepository.Verify(x => x.GetFavouriteByIdAsync(It.IsAny<Guid>()), Times.Once);
//            mockFavouriteLocationsRepository.Verify(x => x.DeleteFavouriteAsync(It.IsAny<FavouriteLocationDto>()), Times.Once);
//            Assert.True(result);
//        }

//        [Fact]
//        public async Task DeleteFavouriteLocationAsync_ThrowsException()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ThrowsAsync(new Exception("Test Exception"));

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            Func<Task> act = () => sut.RemoveFromFavouritesAync(Guid.NewGuid());

//            //assert
//            Exception ex = await Assert.ThrowsAsync<Exception>(act);
//            Assert.Contains("Test Exception", ex.Message);
//        }

//        [Fact]
//        public async Task DeleteFavouriteLocationAsync_ReturnsFalse()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.GetFavouriteByIdAsync(It.IsAny<Guid>())).ReturnsAsync(
//               new FavouriteLocationDto()
//               {
//                   Id = Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),
//                   Latitude = 51.51f,
//                   Longitude = -0.13f,
//                   LocationName = "London"
//               });

//            mockFavouriteLocationsRepository.Setup(s => s.DeleteFavouriteAsync(It.IsAny<FavouriteLocationDto>())).ReturnsAsync(false);

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.RemoveFromFavouritesAync(Guid.NewGuid());

//            //assert
//            mockFavouriteLocationsRepository.Verify(x => x.GetFavouriteByIdAsync(It.IsAny<Guid>()), Times.Once);
//            mockFavouriteLocationsRepository.Verify(x => x.DeleteFavouriteAsync(It.IsAny<FavouriteLocationDto>()), Times.Once);
//            Assert.False(result);
//        }

//        [Fact]
//        public async Task UpdateFavouriteAsync_UpdatesLocation()
//        {
//            //arrange
//            var logger = Mock.Of<ILogger<FavouriteWeatherLocationsService>>();

//            var mockFavouriteLocationsRepository = new Mock<IFavouriteLocationsRepository>();
//            mockFavouriteLocationsRepository.Setup(s => s.UpdateFavouriteAsync(It.IsAny<Guid>(),It.IsAny<double>(), It.IsAny<double>(), It.IsAny<string>())).ReturnsAsync(
//                new FavouriteLocationDto()
//                {
//                    Id = Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),
//                    Latitude = 51.51f,
//                    Longitude = -0.13f,
//                    LocationName = "London"
//                });

//            var sut = new FavouriteWeatherLocationsService(mockFavouriteLocationsRepository.Object, logger);

//            //act
//            var result = await sut.UpdateFavouriteAsync(Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"),51.51f, 0.13f, "London");


//            //assert
//            Assert.NotNull(result);
//            Assert.IsType<FavouriteLocation>(result);
//            Assert.Equal("London", result.LocationName);
//            Assert.Equal(51.51f, result.Latitude);
//            Assert.Equal(-0.13f, result.Longitude);
//            Assert.Equal(Guid.Parse("110d11b9-f9c7-44bf-8f4c-fe9d8f9c57e8"), result.Id);
//        }
//    }
//}
