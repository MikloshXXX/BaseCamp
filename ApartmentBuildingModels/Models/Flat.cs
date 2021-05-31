// <copyright file="Flat.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Models
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
        public Flat(int apartmentNumber, float floorArea, int? residentID = null)
        {
            this.ApartmentNumber = apartmentNumber;
            this.FloorArea = floorArea;
            this.Rent = floorArea * 100f;
            this.IsOccupied = false;
            if (residentID != null)
            {
                this.IsOccupied = true;
            }

            this.ResidentID = residentID;
        }

        /// <summary>
        /// Gets property that contains apartment number in an apartment building.
        /// </summary>
        public int ApartmentNumber { get; private set; }

        /// <summary>
        /// Gets property that contains floor area of an apartments.
        /// </summary>
        public float FloorArea { get; private set; }

        /// <summary>
        /// Gets property that contains the rent for the current apartments.
        /// </summary>
        public float Rent { get; private set; }

        /// <summary>
        /// Gets a value indicating whether someone lives in the apartment.
        /// </summary>
        public bool IsOccupied { get; private set; }

        /// <summary>
        /// Gets property that contains resident who lives in this apartments.
        /// </summary>
        public int? ResidentID { get; private set; }
    }
}
