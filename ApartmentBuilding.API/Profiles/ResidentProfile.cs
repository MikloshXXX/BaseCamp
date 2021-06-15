// <copyright file="ResidentProfile.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Profiles
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.API.Responses;
    using ApartmentBuilding.Core.Models;
    using AutoMapper;

    /// <summary>
    /// Resident profile for auto mapper.
    /// </summary>
    public class ResidentProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResidentProfile"/> class.
        /// </summary>
        public ResidentProfile()
        {
            this.CreateMap<ResidentRequest, Resident>();
            this.CreateMap<Resident, ResidentResponse>();
        }
    }
}
