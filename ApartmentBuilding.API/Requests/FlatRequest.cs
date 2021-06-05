// <copyright file="FlatRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;

    /// <summary>
    /// FlatRequest class.
    /// </summary>
    public class FlatRequest
    {
        /// <summary>
        /// Gets or sets property that contains floor area of an apartments.
        /// </summary>
        public int FloorArea { get; set; }

        /// <summary>
        /// Gets or sets property that contains resident who lives in this apartments.
        /// </summary>
        public int? ResidentID { get; set; }

        /// <summary>
        /// Represents FlatRequest as Flat.
        /// </summary>
        /// <returns>Flat.</returns>
        public Flat ToModel()
        {
            return new Flat(default, this.FloorArea, this.ResidentID);
        }
    }
}
