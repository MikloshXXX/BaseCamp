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
    public class AuthControllerTests
    {
        [Fact]
        public async Task Login_ReturnsOkResultWithAccessAndRefreshTokens_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident()
            {
                ID = 1,
                Password = "password",
            });
            repository.Setup(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(token => token.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GenerateRefreshToken()).Returns(It.IsAny<string>());

            var controller = new AuthController(repository.Object, tokenService.Object);

            // Act
            var result = await controller.Login(new AuthenticateResidentRequest()
            {
                ID = 1,
                Password = "password",
            });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            repository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
            repository.Verify(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once);
        }

        [Fact]
        public async Task Login_ReturnsUnauthorized_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident());
            repository.Setup(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(token => token.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GenerateRefreshToken()).Returns(It.IsAny<string>());

            var controller = new AuthController(repository.Object, tokenService.Object);

            // Act
            var result = await controller.Login(new AuthenticateResidentRequest()
            {
                ID = 1,
                Password = "password",
            });

            // Assert
            result.Should().BeOfType<UnauthorizedResult>();

            repository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
            repository.Verify(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Login_ReturnsBadRequest_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident()
            {
                ID = 1,
                Password = "password",
            });
            repository.Setup(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(token => token.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GenerateRefreshToken()).Returns(It.IsAny<string>());

            var controller = new AuthController(repository.Object, tokenService.Object);

            // Act
            var result = await controller.Login(null);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            repository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Never);
            repository.Verify(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        }

        [Fact]
        public async Task Refresh_ReturnsOkResultWithAccessAndRefreshTokens_WhenModelIsValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident()
            {
                ID = 1,
                Password = "password",
                RefreshToken = "refreshtoken",
                RefreshTokenExpiryTime = DateTime.Now.AddSeconds(10),
            });
            repository.Setup(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(token => token.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GenerateRefreshToken()).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GetPrincipalFromExpiredToken(It.IsAny<string>())).Returns(new ClaimsPrincipal());
            tokenService.SetupGet(token => token.GetPrincipalFromExpiredToken(It.IsAny<string>()).Identity.Name).Returns("1");

            var controller = new AuthController(repository.Object, tokenService.Object);

            // Act
            var result = await controller.Refresh(new TokenApiModel()
            {
                AccessToken ="accesstoken",
                RefreshToken = "refreshtoken",
            });

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            repository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
            repository.Verify(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), null), Times.Once);
        }

        [Fact]
        public async Task Refresh_ReturnsBadRequest_WhenModelIsNotValid()
        {
            // Arrange
            var repository = new Mock<IResidentRepository<Resident>>();
            repository.Setup(repo => repo.GetByID(It.IsAny<int>())).ReturnsAsync(new Resident()
            {
                ID = 1,
                Password = "password",
                RefreshToken = "refreshtoken",
                RefreshTokenExpiryTime = DateTime.Now.AddSeconds(10),
            });
            repository.Setup(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<DateTime>())).ReturnsAsync(true);

            var tokenService = new Mock<ITokenService>();
            tokenService.Setup(token => token.GenerateAccessToken(It.IsAny<List<Claim>>())).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GenerateRefreshToken()).Returns(It.IsAny<string>());
            tokenService.Setup(token => token.GetPrincipalFromExpiredToken(It.IsAny<string>())).Returns(new ClaimsPrincipal());
            tokenService.SetupGet(token => token.GetPrincipalFromExpiredToken(It.IsAny<string>()).Identity.Name).Returns("1");

            var controller = new AuthController(repository.Object, tokenService.Object);

            // Act
            var result = await controller.Refresh(new TokenApiModel()
            {
                AccessToken = "accesstoken",
                RefreshToken = "incorrect_refreshtoken",
            });

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            repository.Verify(repo => repo.GetByID(It.IsAny<int>()), Times.Once);
            repository.Verify(repo => repo.UpdateRefreshToken(It.IsAny<int>(), It.IsAny<string>(), null), Times.Never);
        }
    }
}
