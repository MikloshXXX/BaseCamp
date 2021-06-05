// <copyright file="IRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Core.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;

    /// <summary>
    /// Class that implements the repository pattern.
    /// </summary>
    /// <typeparam name="T">Current entity.</typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// Method returns list of elements in the collection.
        /// </summary>
        /// <returns>Collection of elements.</returns>
        public List<T> Get();

        /// <summary>
        /// Method returns specific element by its ID.
        /// </summary>
        /// <param name="id">ID of specific element.</param>
        /// <returns>Specific element by its ID.</returns>
        public T GetByID(int id);

        /// <summary>
        /// Method for creating an entity and add it to the collection.
        /// </summary>
        /// <param name="entity">Created entity.</param>
        /// <returns>Succes of the operation.</returns>
        public bool Create(T entity);

        /// <summary>
        /// Method for deleting an entity from the collection by ID.
        /// </summary>
        /// <param name="id">ID of deleting element.</param>
        /// <returns>Succes of the operation.</returns>
        public bool Delete(int id);

        /// <summary>
        /// Method for updating an specific element from the collection.
        /// </summary>
        /// <param name="id">ID of updating entity.</param>
        /// <param name="entity">New entity.</param>
        /// <returns>Succes of the operation.</returns>
        public bool Update(int id, T entity);
    }
}
