// <copyright file="FlatProfile.cs" company="PlaceholderCompany">
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
    /// Flat profile for auto mapper.
    /// </summary>
    public class FlatProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FlatProfile"/> class.
        /// </summary>
        public FlatProfile()
        {
            this.CreateMap<FlatRequest, Flat>();
            this.CreateMap<Flat, FlatResponse>();
        }
    }
}
