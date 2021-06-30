// <copyright file="IdentityNameService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// IIdentityNameService implementation.
    /// </summary>
    public class IdentityNameService : IIdentityNameService
    {
        /// <summary>
        /// Returns identity name.
        /// </summary>
        /// <param name="httpContext">Current httpcontext.</param>
        /// <returns>Name.</returns>
        public string GetIdentityName(HttpContext httpContext)
        {
            return httpContext.User.Identity.Name;
        }
    }
}
