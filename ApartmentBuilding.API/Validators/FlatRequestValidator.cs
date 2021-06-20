// <copyright file="FlatRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.Core.Models;
    using FluentValidation;

    /// <summary>
    /// Flat validator with fluent validation.
    /// </summary>
    public class FlatRequestValidator : AbstractValidator<FlatRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatRequestValidator"/> class.
        /// </summary>
        public FlatRequestValidator()
        {
            var msg = "Error in {PropertyName}: value {PropertyValue}";

            this.RuleFor(_ => _.FloorArea)
                .Must(_ => _ > 10 && _ < 999)
                .WithMessage(msg);

            this.RuleFor(_ => _.Rent)
                .Must(_ => _ > 0)
                .WithMessage(msg);

            this.RuleFor(_ => _.ResidentID)
                .Must(_ => _ == null || _ > 0)
                .WithMessage(msg);
        }
    }
}
