// <copyright file="FlatJSON.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for formatting flat from JSON to C# Object.
    /// </summary>
    public class FlatJSON
    {
        /// <summary>
        /// Gets or sets an apartments number of the apartment.
        /// </summary>
        public int ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets a floor area of the apartment.
        /// </summary>
        public float FloorArea { get; set; }

        /// <summary>
        /// Gets or sets a resident id of the apartment.
        /// </summary>
        public int? ResidentID { get; set; }
    }
}
