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
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.API.Responses;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using ApartmentBuilding.Data;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentsController"/> class.
        /// </summary>
        /// <param name="repository">repo param.</param>
        /// /// <param name="mapper">mapper param?.</param>
        public ResidentsController(IRepository<Resident> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returns collection of the residents from the repository.
        /// </summary>
        /// <returns>List of the residents.</returns>
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
        [Authorize]
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
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return this.Ok(await this.repository.Delete(id));
        }

        /// <summary>
        /// Token.
        /// </summary>
        /// <param name="id">Resident id.</param>
        /// <returns>IActionResult.</returns>
        //[HttpPost("/token")]
        //public async Task<IActionResult> Token(int id)
        //{
        //    var identity = await this.GetIdentity(id);
        //    if (identity == null)
        //    {
        //        return this.BadRequest(new { errorText = "Invalid resident." });
        //    }

        //    var now = DateTime.UtcNow;

        //    var jwt = new JwtSecurityToken(
        //        issuer: AuthOptions.Issuer,
        //        audience: AuthOptions.Audience,
        //        notBefore: now,
        //        claims: identity.Claims,
        //        expires: now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime)),
        //        signingCredentials: new Microsoft.IdentityModel.Tokens.SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
        //    var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        //    var response = new
        //    {
        //        access_token = encodedJwt,
        //        username = identity.Name,
        //    };

        //    return new JsonResult(response);
        //}

        /// <summary>
        /// Returns identity.
        /// </summary>
        /// <param name="id">id of the resident.</param>
        /// <returns>ClaimsIdentity of the resident.</returns>
        //public async Task<ClaimsIdentity> GetIdentity(int id)
        //{
        //    Resident resident = await this.repository.GetByID(id);
        //    if (resident != null)
        //    {
        //        var claims = new List<Claim>
        //        {
        //            new Claim(ClaimsIdentity.DefaultNameClaimType, resident.Name),
        //            new Claim(ClaimValueTypes.Integer32, resident.ID.ToString()),
        //        };
        //        ClaimsIdentity claimsIdentity =
        //        new ClaimsIdentity(claims, "Token");
        //        return claimsIdentity;
        //    }

        //    return null;
        //}
    }
}
