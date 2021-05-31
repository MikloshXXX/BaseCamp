// <copyright file="DatabaseConnection.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MySqlConnector;

    /// <summary>
    /// Class containing the connection to the MySQL database.
    /// </summary>
    public class DatabaseConnection
    {
        private readonly MySqlConnection connection = new MySqlConnection("server=localhost;port=3306;user=root;password=root;database=flathouse");

        /// <summary>
        /// Method that opens the connection to the database.
        /// </summary>
        public void OpenConnection()
        {
            if (this.connection.State == System.Data.ConnectionState.Closed)
            {
                this.connection.Open();
            }
        }

        /// <summary>
        /// Method that closes the connection to the database.
        /// </summary>
        public void CloseConnection()
        {
            if (this.connection.State == System.Data.ConnectionState.Open)
            {
                this.connection.Close();
            }
        }

        /// <summary>
        /// Method the returs current connection to the database.
        /// </summary>
        /// <returns>Database connection.</returns>
        public MySqlConnection GetConnection()
        {
            return this.connection;
        }
    }
}
