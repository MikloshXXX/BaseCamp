// <copyright file="ResidentResponse.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Responses
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;

    /// <summary>
    /// Resident response model class.
    /// </summary>
    public class ResidentResponse
    {
        /// <summary>
        /// Gets or sets property that contains an ID of a resident.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets property that contains a name of a resident.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property that contains all apartments where resident lives.
        /// </summary>
        public List<FlatResponse> Flats { get; set; }
    }
}
