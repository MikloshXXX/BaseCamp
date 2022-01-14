// <copyright file="ResidentsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IdentityModel.Tokens.Jwt;
    using System.Linq;
    using System.Security.Claims;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.API.Responses;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using ApartmentBuilding.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.IdentityModel.Tokens;

    /// <summary>
    /// Controller for residents entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentsController : ControllerBase
    {
        private readonly IResidentRepository<Resident> repository;
        private readonly IMapper mapper;
        private readonly IIdentityNameService identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentsController"/> class.
        /// </summary>
        /// <param name="repository">repo param.</param>
        /// <param name="mapper">mapper param.</param>
        /// <param name="configuration">configuration param.</param>
        /// <param name="identity">identity param.</param>
        public ResidentsController(IResidentRepository<Resident> repository, IMapper mapper, IConfiguration configuration, IIdentityNameService identity)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.identity = identity;
        }

        /// <summary>
        /// Returns collection of the residents from the repository.
        /// </summary>
        /// <returns>List of the residents.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await this.repository.Get();
            if (models != null)
            {
                var result = models.ToList().Select(_ => this.mapper.Map<ResidentResponse>(_));
                return this.Ok(result.ToList());
            }

            return this.NoContent();
        }

        /// <summary>
        /// Returns specific resident entity from the repository.
        /// </summary>
        /// <param name="id">ID of resident.</param>
        /// <returns>Resident by id.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (this.identity.GetIdentityName(this.HttpContext) == id.ToString())
            {
                var result = await this.repository.GetByID(id);
                if (result != null)
                {
                    return this.Ok(this.mapper.Map<ResidentResponse>(result));
                }

                return this.NoContent();
            }

            return this.Forbid();
        }

        /// <summary>
        /// Creates an entity of resident in the repository.
        /// </summary>
        /// <param name="resident">Resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Register([FromBody] ResidentRequest resident)
        {
            if (resident.Validate())
            {
                var model = this.mapper.Map<Resident>(resident);
                bool result = await this.repository.Create(model);
                if (result)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(new { errorText = "Resident was not created" });
            }

            return this.BadRequest(new { errorText = "Validation problem" });
        }

        /// <summary>
        /// Updates an entity of resident in the repository.
        /// </summary>
        /// <param name="id">ID of updating resident entity.</param>
        /// <param name="resident">New resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ResidentRequest resident)
        {
            if (resident.Validate())
            {
                var model = this.mapper.Map<Resident>(resident);
                bool result = await this.repository.Update(id, model);
                if (result)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(new { errorText = "Resident was not changed" });
            }

            return this.BadRequest(new { errorText = "Validation problem" });
        }

        /// <summary>
        /// Deletes an entity of resident from the repository by its ID.
        /// </summary>
        /// <param name="id">ID of deleting resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            bool result = await this.repository.Delete(id);
            if (result)
            {
                return this.Ok(result);
            }

            return this.BadRequest(new { errorText = "Resident was not deleted" });
        }
    }
}
