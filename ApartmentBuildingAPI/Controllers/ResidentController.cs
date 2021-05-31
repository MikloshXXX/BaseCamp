// <copyright file="ResidentController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Models;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for residents entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ResidentController : ControllerBase
    {
        private readonly IRepository<Resident> repository;
        private readonly IRepository<Flat> flatRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentController"/> class.
        /// </summary>
        /// <param name="repository">r?.</param>
        /// <param name="flatRepository">f?.</param>
        public ResidentController(IRepository<Resident> repository, IRepository<Flat> flatRepository)
        {
            this.repository = repository;
            this.flatRepository = flatRepository;
        }

        /// <summary>
        /// Returns collection of the residents from the repository.
        /// </summary>
        /// <returns>List of the residents.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.repository.Get());
        }

        /// <summary>
        /// Returns specific resident entity from the repository.
        /// </summary>
        /// <param name="id">ID of resident.</param>
        /// <returns>Resident.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return this.Ok(this.repository.Get().Find(_ => _.ID == id));
        }

        /// <summary>
        /// Creates an entity of resident in the repository.
        /// </summary>
        /// <param name="resident">Resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] ResidentJSON resident)
        {
            return this.Ok(this.repository.Create(new Resident(resident.ID, resident.Name)));
        }

        /// <summary>
        /// Updates an entity of resident in the repository.
        /// </summary>
        /// <param name="id">ID of updating resident entity.</param>
        /// <param name="resident">New resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ResidentJSON resident)
        {
            return this.Ok(this.repository.Update(id, new Resident(resident.ID, resident.Name)));
        }

        /// <summary>
        /// Deletes an entity of resident from the repository by its ID.
        /// </summary>
        /// <param name="id">ID of deleting resident.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return this.Ok(this.repository.Delete(id));
        }
    }
}
