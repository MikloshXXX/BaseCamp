// <copyright file="FlatRequest.cs" company="PlaceholderCompany">
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
    /// Flat request model class.
    /// </summary>
    public class FlatRequest
    {
        private readonly FlatRequestValidator validator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatRequest"/> class.
        /// </summary>
        public FlatRequest()
        {
            this.validator = new FlatRequestValidator();
        }

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
