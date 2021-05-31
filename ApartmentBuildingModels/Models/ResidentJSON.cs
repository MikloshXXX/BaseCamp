// <copyright file="ResidentJSON.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class for formatting resident from JSON to C# Object.
    /// </summary>
    public class ResidentJSON
    {
        /// <summary>
        /// Gets or sets the ID of the resident.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the name of the resident.
        /// </summary>
        public string Name { get; set; }
    }
}
