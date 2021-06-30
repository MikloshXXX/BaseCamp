// <copyright file="TokenApiModel.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Class that describes token model.
    /// </summary>
    public class TokenApiModel
    {
        /// <summary>
        /// Gets or sets property that contains access token of the resident.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets property that contains refresh token of the resident.
        /// </summary>
        public string RefreshToken { get; set; }
    }
}
