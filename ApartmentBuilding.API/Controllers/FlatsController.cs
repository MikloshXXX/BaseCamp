// <copyright file="FlatsController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.API.Responses;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using AutoMapper;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for apartment entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlatsController : ControllerBase
    {
        private readonly IRepository<Flat> repository;
        private readonly IResidentRepository<Resident> residentRepository;
        private readonly IMapper mapper;
        private readonly IIdentityNameService identity;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatsController"/> class.
        /// </summary>
        /// <param name="repository">repo param.</param>
        /// <param name="residentRepository">resident repo param.</param>
        /// <param name="mapper">mapper param.</param>
        /// <param name="identity">identity param.</param>
        public FlatsController(IRepository<Flat> repository, IResidentRepository<Resident> residentRepository, IMapper mapper, IIdentityNameService identity)
        {
            this.repository = repository;
            this.residentRepository = residentRepository;
            this.mapper = mapper;
            this.identity = identity;
        }

        /// <summary>
        /// Returs a whole collection of apartments from repository.
        /// </summary>
        /// <returns>List of apartments.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await this.repository.Get();
            if (models != null)
            {
                var result = models.ToList().Select(_ => this.mapper.Map<FlatResponse>(_));
                return this.Ok(result.ToList());
            }

            return this.NoContent();
        }

        /// <summary>
        /// Returns specific apartment entity from repository.
        /// </summary>
        /// <param name="id">ID of the apartment.</param>
        /// <returns>Apartment.</returns>
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.repository.GetByID(id);
            if (result != null)
            {
                return this.Ok(this.mapper.Map<FlatResponse>(result));
            }

            return this.NoContent();
        }

        /// <summary>
        /// Returns resident's array of apartments.
        /// </summary>
        /// <param name="id">ID of the resident.</param>
        /// <returns>List of flats.</returns>
        [Authorize]
        [HttpGet("{id}/Flats")]
        public async Task<IActionResult> GetFlatsByResidentID(int id)
        {
            if (this.identity.GetIdentityName(this.HttpContext) == id.ToString())
            {
                var result = await this.residentRepository.GetByID(id);
                if (result != null)
                {
                    return this.Ok(result.Flats);
                }

                return this.BadRequest();
            }

            return this.Forbid();
        }

        /// <summary>
        /// Create an entity of apartment in the repository.
        /// </summary>
        /// <param name="flat">Apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlatRequest flat)
        {
            if (flat.Validate())
            {
                var model = this.mapper.Map<Flat>(flat);
                var result = await this.repository.Create(model);
                if (result)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(new { errorText = "Flat was not created" });
            }

            return this.BadRequest(new { errorText = "Validation problem" });
        }

        /// <summary>
        /// Update an entity of apartments in the repository.
        /// </summary>
        /// <param name="id">ID of the updating apartment.</param>
        /// <param name="flat">New apartment.</param>
        /// <returns>Sucess of the opartion.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FlatRequest flat)
        {
            if (flat.Validate())
            {
                var model = this.mapper.Map<Flat>(flat);
                bool result = await this.repository.Update(id, model);
                if (result)
                {
                    return this.Ok(result);
                }

                return this.BadRequest(new { errorText = "Flat was not changed" });
            }

            return this.BadRequest(new { errorText = "Validation problem" });
        }

        /// <summary>
        /// Delete specific apartment from the repository.
        /// </summary>
        /// <param name="id">ID of the deleting apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            bool result = await this.repository.Delete(id);
            if (result)
            {
                return this.Ok(result);
            }

            return this.BadRequest(new { errorText = "Flat was not deleted" });
        }
    }
}
