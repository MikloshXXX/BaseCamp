// <copyright file="AuthenticateResidentRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// AuthenticateResidentRequest model.
    /// </summary>
    public class AuthenticateResidentRequest
    {
        /// <summary>
        /// Gets or sets property that contains an ID of a resident.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets property that contains a password of a resident.
        /// </summary>
        public string Password { get; set; }
    }
}
