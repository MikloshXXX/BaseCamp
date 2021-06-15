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
        private readonly IMapper mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatsController"/> class.
        /// </summary>
        /// <param name="repository">repo param.</param>
        /// /// <param name="mapper">mapper param.</param>
        public FlatsController(IRepository<Flat> repository, IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Returs a whole collection of apartments from repository.
        /// </summary>
        /// <returns>List of apartments.</returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var models = await this.repository.Get();
            var result = models.ToList().Select(_ => this.mapper.Map<FlatResponse>(_));
            return this.Ok(result.ToList());
        }

        /// <summary>
        /// Returns specific apartment entity from repository.
        /// </summary>
        /// <param name="id">ID of the apartment.</param>
        /// <returns>Apartment.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await this.repository.GetByID(id);
            return this.Ok(this.mapper.Map<FlatResponse>(result));
        }

        /// <summary>
        /// Create an entity of apartment in the repository.
        /// </summary>
        /// <param name="flat">Apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FlatRequest flat)
        {
            if (flat.Validate())
            {
                var model = this.mapper.Map<Flat>(flat);
                return this.Ok(await this.repository.Create(model));
            }

            return this.ValidationProblem();
        }

        /// <summary>
        /// Update an entity of apartments in the repository.
        /// </summary>
        /// <param name="id">ID of the updating apartment.</param>
        /// <param name="flat">New apartment.</param>
        /// <returns>Sucess of the opartion.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] FlatRequest flat)
        {
            if (flat.Validate())
            {
                var model = this.mapper.Map<Flat>(flat);
                return this.Ok(await this.repository.Update(id, model));
            }

            return this.ValidationProblem();
        }

        /// <summary>
        /// Delete specific apartment from the repository.
        /// </summary>
        /// <param name="id">ID of the deleting apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            return this.Ok(await this.repository.Delete(id));
        }
    }
}
