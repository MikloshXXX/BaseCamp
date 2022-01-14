using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using ApartmentBuilding.API.Controllers;
using ApartmentBuilding.API.Requests;
using ApartmentBuilding.API.Responses;
using ApartmentBuilding.Core.Models;
using ApartmentBuilding.Core.Repositories;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace ApartmentBuilding.Tests.Controllers
{
    public class FlatsControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResultWithFlat_WithParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Flat());

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<FlatResponse>(It.IsAny<Flat>())).Returns(new FlatResponse());

            var identity = new Mock<IIdentityNameService>();
            identity.Setup(u => u.GetIdentityName(It.IsAny<HttpContext>())).Returns("1");

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<FlatResponse>();

            repository.Verify(_ => _.GetByID(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync((Flat)null);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<FlatResponse>(It.IsAny<Flat>())).Returns(new FlatResponse());

            var identity = new Mock<IIdentityNameService>();
            identity.Setup(u => u.GetIdentityName(It.IsAny<HttpContext>())).Returns("-1");

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Get(-1);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            repository.Verify(_ => _.GetByID(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOkResultWithListOfFlats_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync(new List<Flat>());

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<FlatResponse>(It.IsAny<Flat>())).Returns(new FlatResponse());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<List<FlatResponse>>();

            repository.Verify(_ => _.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync((List<Flat>)null);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<FlatResponse>(It.IsAny<Flat>())).Returns(new FlatResponse());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            repository.Verify(_ => _.Get(), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Create(It.IsAny<Flat>())).ReturnsAsync(true);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Post(new FlatRequest() { 
                FloorArea = 100,
                Rent = 1000,
                ResidentID = 1,
            });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);

            repository.Verify(_ => _.Create(It.IsAny<Flat>()), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Create(It.IsAny<Flat>())).ReturnsAsync(true);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Post(new FlatRequest()
            {
                FloorArea = 1,
                Rent = 1000,
                ResidentID = 1,
            });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();


            repository.Verify(_ => _.Create(It.IsAny<Flat>()), Times.Never);
        }

        [Fact]
        public async Task Put_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Flat>())).ReturnsAsync(true);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Put(It.IsAny<int>(), new FlatRequest()
            {
                FloorArea = 100,
                Rent = 1000,
                ResidentID = 1,
            });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);

            repository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Flat>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestResultWithTrue_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Flat>())).ReturnsAsync(false);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Put(It.IsAny<int>(), new FlatRequest()
            {
                FloorArea = 1,
                Rent = 1000,
                ResidentID = 1,
            });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();


            repository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Flat>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Delete(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);

            repository.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Delete_ReturnsBadRequestResultWithTrue_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Flat>>();
            repository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false);

            var residentRepository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<Flat>(It.IsAny<FlatRequest>())).Returns(new Flat());

            var identity = new Mock<IIdentityNameService>();

            var controller = new FlatsController(repository.Object, residentRepository.Object, mapper.Object, identity.Object);

            // Act
            var result = await controller.Delete(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            repository.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
