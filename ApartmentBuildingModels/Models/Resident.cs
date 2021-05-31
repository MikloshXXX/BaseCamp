// <copyright file="Resident.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Models
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
        public Resident(int iD, string name, List<Flat> flats = null)
        {
            this.ID = iD;
            this.Name = name;
            if (flats != null)
            {
                this.Flats = flats;
            }
            else
            {
                this.Flats = new List<Flat>();
            }
        }

        /// <summary>
        /// Gets property that contains ID of the resident.
        /// </summary>
        public int ID { get; private set; }

        /// <summary>
        /// Gets property that contains name of the resident.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets property that contains all apartments where resident lives.
        /// </summary>
        public List<Flat> Flats { get; private set; }
    }
}
