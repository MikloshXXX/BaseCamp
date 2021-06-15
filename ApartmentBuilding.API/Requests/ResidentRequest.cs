// <copyright file="ResidentRequest.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Validators;

    /// <summary>
    /// Resident request model class.
    /// </summary>
    public class ResidentRequest
    {
        private readonly ResidentRequestValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentRequest"/> class.
        /// </summary>
        public ResidentRequest()
        {
            this.validator = new ResidentRequestValidator();
        }

        /// <summary>
        /// Gets or sets property that contains a name of a resident.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property that contains a password of a resident.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Validate current object.
        /// </summary>
        /// <returns>True if object passed validation.</returns>
        public bool Validate()
        {
            var result = this.validator.Validate(this);
            if (result.IsValid)
            {
                return true;
            }

            foreach (var error in result.Errors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return false;
        }
    }
}
