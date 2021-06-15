// <copyright file="SQLResidentRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using Dapper;
    using MySqlConnector;

    /// <summary>
    /// Class that implements the repository interface for working with residents SQL.
    /// </summary>
    public class SQLResidentRepository : IRepository<Resident>
    {
        private string provider = "server=localhost;port=3306;user=root;password=root;database=apartmentbuild";

        /// <summary>
        /// Creates a resident entity in the database.
        /// </summary>
        /// <param name="entity">Creating resident.</param>
        /// <returns>Success of the operation.</returns>
        public async Task<bool> Create(Resident entity)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.ExecuteAsync($"INSERT INTO `residents`(`Name`, `Password`) VALUES ('{entity.Name}','{entity.Password}')");
                }

                return true;
        }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes a resident from the database.
        /// </summary>
        /// <param name="id">ID of deleting resident.</param>
        /// <returns>Success of the operation.</returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.ExecuteAsync($"DELETE FROM `residents` WHERE ID = {id}");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the list of all residents from the database.
        /// </summary>
        /// <returns>List of residents.</returns>
        public async Task<IEnumerable<Resident>> Get()
        {
            using (var connection = new MySqlConnection(this.provider))
            {
                var result = await connection.QueryAsync<Resident>("SELECT * FROM `residents`");
                foreach (Resident r in result.ToList())
                {
                    var flatsResult = await connection.QueryAsync<Flat>($"SELECT * FROM `apartments` WHERE ResidentID = {r.ID}");
                    r.Flats = flatsResult.ToList();
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a spicific resident from the database by its id.
        /// </summary>
        /// <param name="id">ID of the resident.</param>
        /// <returns>Resident entity.</returns>
        public async Task<Resident> GetByID(int id)
        {
            using (var connection = new MySqlConnection(this.provider))
            {
                var result = await connection.QuerySingleAsync<Resident>($"SELECT * FROM `residents` WHERE ID = {id}");
                var flatsResult = await connection.QueryAsync<Flat>($"SELECT * FROM `apartments` WHERE ResidentID = {result.ID}");
                result.Flats = flatsResult.ToList();
                return result;
            }
        }

        /// <summary>
        /// Updates a specific resident from the database by its id.
        /// </summary>
        /// <param name="id">ID of updating resident.</param>
        /// <param name="entity">New resident.</param>
        /// <returns>Success of the opeation.</returns>
        public async Task<bool> Update(int id, Resident entity)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.ExecuteAsync($"UPDATE `residents` SET `Name`='{entity.Name}',`Password`='{entity.Password}' WHERE ID = {id}");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
