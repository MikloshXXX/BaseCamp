using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentBuilding.API.Controllers;
using ApartmentBuilding.API.Requests;
using ApartmentBuilding.API.Responses;
using ApartmentBuilding.Core.Models;
using ApartmentBuilding.Core.Repositories;
using AutoMapper;
using FluentAssertions;
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
            var repository = new Mock<IRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident());

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<ResidentResponse>(It.IsAny<Resident>())).Returns(new ResidentResponse());

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Get(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<ResidentResponse>();
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync((Resident)null);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Get(It.IsAny<int>());

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Get_ReturnsOkResultWithListOfResident_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Resident>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync(new List<Resident>());

            var mapper = new Mock<IMapper>();
            mapper.Setup(map => map.Map<ResidentResponse>(It.IsAny<Resident>())).Returns(new ResidentResponse());

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().BeOfType<List<ResidentResponse>>();
        }

        [Fact]
        public async Task Get_ReturnsNoContentResult_WithoutParameter()
        {
            // Arrange
            var repository = new Mock<IRepository<Resident>>();
            repository.Setup(repo => repo.Get()).ReturnsAsync((List<Resident>)null);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Get();

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [Fact]
        public async Task Post_ReturnsOkResultWithTrue_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Resident>>();
            repository.Setup(repo => repo.Create(It.IsAny<Resident>())).ReturnsAsync(true);

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Post(new ResidentRequest() { Name = "name", Password = "password" });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;

            okResult.Value.Should().Be(true);
        }

        [Fact]
        public async Task Post_ReturnsBadRequest_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IRepository<Resident>>();

            var mapper = new Mock<IMapper>();

            var configuration = new Mock<IConfiguration>();

            var controller = new ResidentsController(repository.Object, mapper.Object, configuration.Object);

            // Act
            var result = await controller.Post(new ResidentRequest() { Name = "", Password = "" });

            // Assert
            result.Should().BeOfType<BadRequestResult>();
        }
    }
}
