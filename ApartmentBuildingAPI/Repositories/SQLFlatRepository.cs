// <copyright file="SQLFlatRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API
{
    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Models;
    using MySqlConnector;

    /// <summary>
    /// Class that implements the repository interface for working with flats SQL.
    /// </summary>
    public class SQLFlatRepository : IRepository<Flat>
    {
        private MySqlCommand cmd;
        private DatabaseConnection dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLFlatRepository"/> class.
        /// </summary>
        public SQLFlatRepository()
        {
            this.dbConnection = new DatabaseConnection();
            this.dbConnection.OpenConnection();
        }

        /// <summary>
        /// Creates flat entity in the database.
        /// </summary>
        /// <param name="entity">Creating flat.</param>
        /// <returns>Success of the operation.</returns>
        public bool Create(Flat entity)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "INSERT INTO `flats`(`apartmentNumber`, `floorArea`, `residentID`) VALUES (@apartmentNumber,@floorArea,@residentID)";
                this.cmd.CommandText = sql;
                MySqlParameter aptNum = new MySqlParameter("@apartmentNumber", MySqlDbType.Int32);
                aptNum.Value = entity.ApartmentNumber;
                this.cmd.Parameters.Add(aptNum);
                MySqlParameter flArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
                flArea.Value = entity.FloorArea;
                this.cmd.Parameters.Add(flArea);
                MySqlParameter resNm = new MySqlParameter("@residentID", MySqlDbType.Int32);
                resNm.Value = entity.ResidentID;
                this.cmd.Parameters.Add(resNm);
                this.cmd.ExecuteNonQuery();
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
        public bool Delete(int id)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "DELETE FROM `flats` WHERE `apartmentNumber` = @aptNum";
                this.cmd.CommandText = sql;
                MySqlParameter aptNum = new MySqlParameter("@aptNum", MySqlDbType.Int32);
                aptNum.Value = id;
                this.cmd.Parameters.Add(aptNum);
                this.cmd.ExecuteNonQuery();
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
        public List<Flat> Get()
        {
            try
            {
                List<Flat> flats = new List<Flat>();
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "SELECT `apartmentNumber`, `floorArea`, `residentID` FROM `flats`";
                this.cmd.CommandText = sql;
                using (DbDataReader reader = this.cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int flatNumberIndex = reader.GetOrdinal("apartmentNumber");
                        int flatNumber = reader.GetInt32(flatNumberIndex);

                        int floorAreaIndex = reader.GetOrdinal("floorArea");
                        float floorArea = reader.GetFloat(floorAreaIndex);

                        int residentIDIndex = reader.GetOrdinal("residentID");
                        if (reader.IsDBNull(residentIDIndex))
                        {
                            flats.Add(new Flat(flatNumber, floorArea));
                        }
                        else
                        {
                            int residentID = reader.GetInt32(residentIDIndex);
                            flats.Add(new Flat(flatNumber, floorArea, residentID));
                        }
                    }

                    return flats;
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
        public Flat GetByID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates the flat by apartment number.
        /// </summary>
        /// <param name="id">Apartment number.</param>
        /// <param name="entity">New apartments.</param>
        /// <returns>Sucess of the operation.</returns>
        public bool Update(int id, Flat entity)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "UPDATE `flats` SET " +
                    "`floorArea` = @floorArea, `residentID` = @residentID WHERE `apartmentNumber` = @aptNum";
                this.cmd.CommandText = sql;
                MySqlParameter aptNum = new MySqlParameter("@aptNum", MySqlDbType.Int32);
                aptNum.Value = entity.ApartmentNumber;
                MySqlParameter floorArea = new MySqlParameter("@floorArea", MySqlDbType.Float);
                floorArea.Value = entity.FloorArea;
                MySqlParameter residentName = new MySqlParameter("@residentID", MySqlDbType.Int32);
                residentName.Value = entity.ResidentID;
                this.cmd.Parameters.Add(aptNum);
                this.cmd.Parameters.Add(floorArea);
                this.cmd.Parameters.Add(residentName);
                this.cmd.ExecuteNonQuery();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
