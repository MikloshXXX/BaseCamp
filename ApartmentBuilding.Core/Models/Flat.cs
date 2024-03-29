﻿// <copyright file="Flat.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Apartment entity class.
    /// </summary>
    public class Flat
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Flat"/> class.
        /// </summary>
        /// <param name="apartmentNumber">Apartment number.</param>
        /// <param name="floorArea">Floor area.</param>
        /// /// <param name="residentID">Resident`s ID who lives in this apartment.</param>
        public Flat(int apartmentNumber = 0, float floorArea = 0, float rent = 0, int? residentID = null)
        {
            this.ApartmentNumber = apartmentNumber;
            this.FloorArea = floorArea;
            this.Rent = rent;
            this.ResidentID = residentID;
        }

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
