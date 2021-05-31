// <copyright file="SQLResidentRepository.cs" company="PlaceholderCompany">
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
    /// Class that implements the repository interface for working with residents SQL.
    /// </summary>
    public class SQLResidentRepository : IRepository<Resident>
    {
        private MySqlCommand cmd;
        private DatabaseConnection dbConnection;

        /// <summary>
        /// Initializes a new instance of the <see cref="SQLResidentRepository"/> class.
        /// </summary>
        public SQLResidentRepository()
        {
            this.dbConnection = new DatabaseConnection();
            this.dbConnection.OpenConnection();
        }

        /// <summary>
        /// Creates a resident entity in the database.
        /// </summary>
        /// <param name="entity">Creating resident.</param>
        /// <returns>Success of the operation.</returns>
        public bool Create(Resident entity)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "INSERT INTO `residents`(`ID`, `name`) VALUES (@ID,@name)";
                this.cmd.CommandText = sql;
                MySqlParameter id = new MySqlParameter("@ID", MySqlDbType.Int32);
                id.Value = entity.ID;
                this.cmd.Parameters.Add(id);
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                name.Value = entity.Name;
                this.cmd.Parameters.Add(name);
                this.cmd.ExecuteNonQuery();
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
        public bool Delete(int id)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "DELETE FROM `residents` WHERE `ID` = @id";
                this.cmd.CommandText = sql;
                MySqlParameter idParam = new MySqlParameter("@id", MySqlDbType.Int32);
                idParam.Value = id;
                this.cmd.Parameters.Add(idParam);
                this.cmd.ExecuteNonQuery();
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
        public List<Resident> Get()
        {
            try
            {
                List<Resident> residents = new List<Resident>();
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "SELECT * FROM residents LEFT JOIN flats ON residents.ID = flats.residentID UNION SELECT * FROM residents RIGHT JOIN flats ON residents.ID = flats.residentID";
                this.cmd.CommandText = sql;
                using (DbDataReader reader = this.cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int idIndex = reader.GetOrdinal("ID");
                        if (!reader.IsDBNull(idIndex))
                        {
                            int id = reader.GetInt32(idIndex);

                            int nameIndex = reader.GetOrdinal("name");
                            string name = reader.GetString(nameIndex);

                            int aptNumIndex = reader.GetOrdinal("apartmentNumber");
                            int floorAreaIndex = reader.GetOrdinal("floorArea");

                            Resident specificResident = new Resident(id, name);
                            if (!residents.Any(_ => _.ID == specificResident.ID))
                            {
                                residents.Add(new Resident(id, name));
                            }

                            if (!reader.IsDBNull(aptNumIndex))
                            {
                                int aptNum = reader.GetInt32(aptNumIndex);
                                float floorArea = reader.GetFloat(floorAreaIndex);
                                residents.Find(_ => _.ID == specificResident.ID).Flats.Add(new Flat(aptNum, floorArea, specificResident.ID));
                            }
                        }
                    }

                    return residents;
                }
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Returns a spicific resident from the database by its id.
        /// </summary>
        /// <param name="id">ID of the resident.</param>
        /// <returns>Resident entity.</returns>
        public Resident GetByID(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a specific resident from the database by its id.
        /// </summary>
        /// <param name="id">ID of updating resident.</param>
        /// <param name="entity">New resident.</param>
        /// <returns>Success of the opeation.</returns>
        public bool Update(int id, Resident entity)
        {
            try
            {
                this.cmd = new MySqlCommand();
                this.cmd.Connection = this.dbConnection.GetConnection();
                string sql = "UPDATE `residents` SET " +
                    "`ID` = @id, `name` = @name WHERE `ID` = @id";
                this.cmd.CommandText = sql;
                MySqlParameter idNum = new MySqlParameter("@id", MySqlDbType.Int32);
                idNum.Value = entity.ID;
                MySqlParameter name = new MySqlParameter("@name", MySqlDbType.VarChar);
                name.Value = entity.Name;
                this.cmd.Parameters.Add(idNum);
                this.cmd.Parameters.Add(name);
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
