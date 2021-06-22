// <copyright file="FlatResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Flat response model class.
    /// </summary>
    public class FlatResponse
    {
        /// <summary>
        /// Gets or sets property that contains apartment number in an apartment building.
        /// </summary>
        public int ApartmentNumber { get; set; }

        /// <summary>
        /// Gets or sets property that contains floor area of an apartments.
        /// </summary>
        public float FloorArea { get; set; }

        /// <summary>
        /// Gets or sets property that contains the rent for the current apartments.
        /// </summary>
        public float Rent { get; set; }

        /// <summary>
        /// Gets or sets property that contains resident who lives in this apartments.
        /// </summary>
        public int? ResidentID { get; set; }
    }
}
