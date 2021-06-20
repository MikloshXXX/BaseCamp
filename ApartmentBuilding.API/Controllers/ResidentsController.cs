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
        private readonly IRepository<Resident> repository;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentsController"/> class.
        /// </summary>
        /// <param name="repository">repo param.</param>
        /// <param name="mapper">mapper param.</param>
        /// /// <param name="configuration">configuration param.</param>
        public ResidentsController(IRepository<Resident> repository, IMapper mapper, IConfiguration configuration)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.configuration = configuration;
        }

        /// <summary>
        /// Returns collection of the residents from the repository.
        /// </summary>
        /// <returns>List of the residents.</returns>
        [Authorize(Roles = "Administator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await this.repository.Get();
            var result = models.ToList().Select(_ => this.mapper.Map<ResidentResponse>(_));
            return this.Ok(result.ToList());
        }

        /// <summary>
        /// Returns specific resident entity from the repository.
        /// </summary>
        /// <param name="id">ID of resident.</param>
        /// <returns>Resident.</returns>
        [Authorize(Roles = "Administator")]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.repository.GetByID(id);
            return this.Ok(this.mapper.Map<ResidentResponse>(result));
        }

        /// <summary>
        /// Creates an entity of resident in the repository.
        /// </summary>
        /// <param name="resident">Resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ResidentRequest resident)
        {
            if (resident.Validate())
            {
                var model = this.mapper.Map<Resident>(resident);
                return this.Ok(await this.repository.Create(model));
            }

            return this.ValidationProblem();
        }

        /// <summary>
        /// Updates an entity of resident in the repository.
        /// </summary>
        /// <param name="id">ID of updating resident entity.</param>
        /// <param name="resident">New resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] ResidentRequest resident)
        {
            if (resident.Validate())
            {
                var model = this.mapper.Map<Resident>(resident);
                return this.Ok(await this.repository.Update(id, model));
            }

            return this.ValidationProblem();
        }

        /// <summary>
        /// Deletes an entity of resident from the repository by its ID.
        /// </summary>
        /// <param name="id">ID of deleting resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return this.Ok(await this.repository.Delete(id));
        }

        /// <summary>
        /// Returns jwt token.
        /// </summary>
        /// <param name="resident">Resident.</param>
        /// <returns>Token.</returns>
        [HttpPost]
        [Route("token")]
        public async Task<IActionResult> Token([FromBody]AuthenticateResidentRequest resident)
        {
            var identity = await this.GetIdentity(resident);
            if (identity == null)
            {
                return this.BadRequest();
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["AuthSettings:key"]));
            var jwt = new JwtSecurityToken(
                issuer: this.configuration["AuthSettings:Issuer"],
                audience: this.configuration["AuthSettings:Audience"],
                claims: identity.Claims,
                expires: DateTime.Now.AddDays(30),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                access_token = encodedJwt,
            };

            return new JsonResult(response);
        }

        private async Task<ClaimsIdentity> GetIdentity(AuthenticateResidentRequest resident)
        {
            var r = await this.repository.GetByID(resident.ID);
            if (r != null)
            {
                if (r.Password == resident.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, r.ID.ToString()),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, r.Role.ToString()),
                    };
                    ClaimsIdentity claimsIdentity =
                        new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
                    return claimsIdentity;
                }
            }

            return null;
        }
    }
}
