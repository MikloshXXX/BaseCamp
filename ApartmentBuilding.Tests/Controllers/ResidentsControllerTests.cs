using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApartmentBuilding.API.Controllers;
using ApartmentBuilding.API.Requests;
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
        public async Task Get_ReturnsListOfResidents_WhenModelStateIsValid()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
