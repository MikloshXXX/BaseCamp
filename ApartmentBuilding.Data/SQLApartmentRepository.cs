// <copyright file="SQLApartmentRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using Dapper;
    using MySqlConnector;

    /// <summary>
    ///     /// Class that implements the repository interface for working with flats SQL.
    /// </summary>
    /// </summary>
    public class SQLApartmentRepository : IRepository<Flat>
    {
        private string provider = "server=localhost;port=3306;user=root;password=root;database=apartmentbuild";

        /// <summary>
        /// Creates flat entity in the database.
        /// </summary>
        /// <param name="entity">Creating flat.</param>
        /// <returns>Success of the operation.</returns>
        public async Task<bool> Create(Flat entity)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    if (entity.ResidentID == null)
                    {
                        var result = await connection.ExecuteAsync($"INSERT INTO `apartments`(`FloorArea`, `Rent`, `ResidentID`) VALUES ({entity.FloorArea}, {entity.Rent}, null)");
                    }
                    else
                    {
                        var result = await connection.ExecuteAsync($"INSERT INTO `apartments`(`FloorArea`, `Rent`, `ResidentID`) VALUES ({entity.FloorArea}, {entity.Rent}, {entity.ResidentID})");
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Deletes flat entity from the database by its ID.
        /// </summary>
        /// <param name="id">Apartment number.</param>
        /// <returns>Success of the operation.</returns>
        public async Task<bool> Delete(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.ExecuteAsync($"DELETE FROM `apartments` WHERE ApartmentNumber = {id}");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Returns the entire collection of apartments in the database.
        /// </summary>
        /// <returns>Collection of apartments in the database.</returns>
        public async Task<IEnumerable<Flat>> Get()
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.QueryAsync<Flat>("SELECT * FROM apartments");
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// .
        /// </summary>
        /// <param name="id">Apartment number.</param>
        /// <returns>Flat.</returns>
        public async Task<Flat> GetByID(int id)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.QuerySingleAsync<Flat>($"SELECT * FROM apartments WHERE ApartmentNumber = {id}");
                    return result;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Updates the flat by apartment number.
        /// </summary>
        /// <param name="id">Apartment number.</param>
        /// <param name="entity">New apartments.</param>
        /// <returns>Sucess of the operation.</returns>
        public async Task<bool> Update(int id, Flat entity)
        {
            try
            {
                using (var connection = new MySqlConnection(this.provider))
                {
                    var result = await connection.ExecuteAsync($"UPDATE `apartments` SET " +
                        $"`FloorArea` = {entity.FloorArea}, `Rent` = {entity.Rent}, `ResidentID` = {entity.ResidentID} WHERE `ApartmentNumber` = {id}");
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
