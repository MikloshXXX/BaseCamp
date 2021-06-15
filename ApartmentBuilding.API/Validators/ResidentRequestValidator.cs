// <copyright file="ResidentRequestValidator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Validators
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using FluentValidation;

    /// <summary>
    /// Resident validator with fluent validation.
    /// </summary>
    public class ResidentRequestValidator : AbstractValidator<ResidentRequest>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentRequestValidator"/> class.
        /// </summary>
        public ResidentRequestValidator()
        {
            var msg = "Error in {PropertyName}: value {PropertyValue}";

            this.RuleFor(_ => _.Name)
                .Must(_ => _.All(_ => char.IsWhiteSpace(_) || char.IsLetter(_)))
                .WithMessage(msg);

            this.RuleFor(_ => _.Password)
                .Length(4, 20)
                .WithMessage(msg);
        }
    }
}
