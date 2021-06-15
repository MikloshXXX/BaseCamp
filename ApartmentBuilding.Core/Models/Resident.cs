// <copyright file="Resident.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// .
    /// </summary>
    public class Resident
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resident"/> class.
        /// </summary>
        /// <param name="iD">Resident's ID.</param>
        /// <param name="name">Resident's name.</param>
        /// <param name="flats">List of flats where the resident lives.</param>
        public Resident(int iD = 0, string name = "", string password = "")
        {
            this.ID = iD;
            this.Name = name;
            this.Password = password;
            this.Flats = new List<Flat>();
        }

        /// <summary>
        /// Gets or sets property that contains an ID of a resident.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets property that contains a name of a resident.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property that contains a password of a resident.
        /// </summary>
        public string Password { get; set; }

        ///// <summary>
        ///// Gets or sets property that contains all apartments where resident lives.
        ///// </summary>
        public List<Flat> Flats { get; set; }
    }
}
