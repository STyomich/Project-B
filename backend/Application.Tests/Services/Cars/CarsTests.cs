using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Services.CarService;
using AutoMapper;
using Core.Domain.Entities;
using Core.DTOs.Entities;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using Xunit;

namespace Application.Tests.Services.Cars
{
    public class CarsTests
    {
        private DataContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "CreateCarTestDb_" + Guid.NewGuid())
                .Options;
            var dbContext = new DataContext(options);
            dbContext.Database.EnsureCreated();
            return dbContext;
        }
        public class FakeFailingDataContext : DataContext
        {
            public FakeFailingDataContext(DbContextOptions<DataContext> options) : base(options) { }

            public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
            {
                return Task.FromResult(0); 
            }
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenCarIsNull()
        {
            // Arrange
            var context = GetDbContext();
            var mapperMock = new Mock<IMapper>();
            var handler = new CreateNewCar.Handler(context, mapperMock.Object);
            var command = new CreateNewCar.Command { Car = null };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Car is required", result.Error);
        }

        [Fact]
        public async Task Handle_ReturnsFailure_WhenSaveFails()
        {
            // Arrange:
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "SaveFailsTestDb")
                .Options;

            var fakeContext = new FakeFailingDataContext(options);
            var mapperMock = new Mock<IMapper>();

            var carDto = new CarDto { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), CarTopicId = Guid.NewGuid(), OwnersDescription = "Test description" };
            var carEntity = new Car { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), CarTopicId = Guid.NewGuid(), OwnersDescription = "Test description" };

            mapperMock.Setup(m => m.Map<Car>(carDto)).Returns(carEntity);

            var handler = new CreateNewCar.Handler(fakeContext, mapperMock.Object);
            var command = new CreateNewCar.Command { Car = carDto };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal("Failed to create car", result.Error);
        }

        [Fact]
        public async Task Handle_ReturnsSuccess_WhenCarIsCreated()
        {
            // Arrange
            var context = GetDbContext();
            var mapperMock = new Mock<IMapper>();

            var carDto = new CarDto { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), CarTopicId = Guid.NewGuid(), OwnersDescription = "Test description" };
            var carEntity = new Car { Id = Guid.NewGuid(), UserId = Guid.NewGuid(), CarTopicId = Guid.NewGuid(), OwnersDescription = "Test description" };

            mapperMock.Setup(m => m.Map<Car>(carDto)).Returns(carEntity);
            mapperMock.Setup(m => m.Map<CarDto>(It.IsAny<Car>())).Returns(carDto);

            var handler = new CreateNewCar.Handler(context, mapperMock.Object);
            var command = new CreateNewCar.Command { Car = carDto };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
        }
    }
}