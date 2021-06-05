// <copyright file="FlatController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for apartment entities.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class FlatController : ControllerBase
    {
        private readonly IRepository<Flat> repository;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatController"/> class.
        /// </summary>
        /// <param name="repository">?.</param>
        public FlatController(IRepository<Flat> repository)
        {
            this.repository = repository;
        }

        /// <summary>
        /// Returs a whole collection of apartments from repository.
        /// </summary>
        /// <returns>List of apartments.</returns>
        [HttpGet]
        public IActionResult Get()
        {
            return this.Ok(this.repository.Get());
        }

        /// <summary>
        /// Returns specific apartment entity from repository.
        /// </summary>
        /// <param name="id">ID of the apartment.</param>
        /// <returns>Apartment.</returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            return this.Ok(this.repository.Get().Find(_ => _.ApartmentNumber == id));
        }

        /// <summary>
        /// Create an entity of apartment in the repository.
        /// </summary>
        /// <param name="flat">Apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpPost]
        public IActionResult Post([FromBody] FlatRequest flat)
        {
            return this.Ok(this.repository.Create(flat.ToModel()));
        }

        /// <summary>
        /// Update an entity of apartments in the repository.
        /// </summary>
        /// <param name="id">ID of the updating apartment.</param>
        /// <param name="flat">New apartment.</param>
        /// <returns>Sucess of the opartion.</returns>
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] FlatRequest flat)
        {
            return this.Ok(this.repository.Update(id, flat.ToModel()));
        }

        /// <summary>
        /// Delete specific apartment from the repository.
        /// </summary>
        /// <param name="id">ID of the deleting apartment.</param>
        /// <returns>Sucess of the operation.</returns>
        [HttpDelete("{id}")]
        public IActionResult DeleteFlat([FromRoute] int id)
        {
            return this.Ok(this.repository.Delete(id));
        }
    }
}
