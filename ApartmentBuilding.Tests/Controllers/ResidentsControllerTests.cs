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
    public class ResidentsControllerTests
    {
        [Fact]
        public async Task Get_ReturnsOkResultWithResident_WithParameter()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident());

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<ResidentResponse>(It.IsAny<Resident>())).Returns(new ResidentResponse());

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();
            identity.Setup(u => u.GetIdentityName(It.IsAny<HttpContext>())).Returns("1");

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Get(1);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<ResidentResponse>();

            repository.Verify(_ => _.GetByID(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithParameter()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync((Resident)null);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();
            identity.Setup(u => u.GetIdentityName(It.IsAny<HttpContext>())).Returns("-1");

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Get(-1);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            repository.Verify(_ => _.GetByID(It.IsAny<int>()), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsOkResultWithListOfResident_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync(new List<Resident>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<ResidentResponse>(It.IsAny<Resident>())).Returns(new ResidentResponse());

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<List<ResidentResponse>>();

            repository.Verify(_ => _.Get(), Times.Once);
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync((List<Resident>)null);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

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
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Create(It.IsAny<Resident>())).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Register(new ResidentRequest() { Name = "name", Password = "password" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);

            repository.Verify(_ => _.Create(It.IsAny<Resident>()), Times.Once);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Register(new ResidentRequest() { Name = "", Password = "" });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();
            repository.Verify(_ => _.Create(It.IsAny<Resident>()), Times.Never);
        }

        [Fact]
        public async Task Put_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
                        // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<Resident>())).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Put(It.IsAny<int>(), new ResidentRequest() { Name = "name", Password = "password" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);

            repository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Resident>()), Times.Once);
        }

        [Fact]
        public async Task Put_ReturnsBadRequestResultWithTrue_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Put(It.IsAny<int>(), new ResidentRequest() { Name = "", Password = "" });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            repository.Verify(_ => _.Update(It.IsAny<int>(), It.IsAny<Resident>()), Times.Never);
        }

        [Fact]
        public async Task Delete_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

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
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.Delete(It.IsAny<int>())).ReturnsAsync(false);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var identity = new Mock<IIdentityNameService>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object, identity.Object);

            // Act
            var result = await controller.Delete(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            repository.Verify(_ => _.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
