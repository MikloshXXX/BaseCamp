// <copyright file="Resident.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.Core.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// .
    /// </summary>
    public class Resident
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Resident"/> class.
        /// </summary>
        /// <param name="iD">Resident's ID.</param>
        /// <param name="name">Resident's name.</param>
        /// <param name="flats">List of flats where the resident lives.</param>
        public Resident(int iD = 0, string name = "", string password = "", ResidentRole role = ResidentRole.Citizen, string refreshToken = null, DateTime? refreshTokenExpiryTime = null)
        {
            this.ID = iD;
            this.Name = name;
            this.Password = password;
            this.Flats = new List<Flat>();
            this.Role = role;
            this.RefreshToken = refreshToken;
            this.RefreshTokenExpiryTime = refreshTokenExpiryTime;
        }

        /// <summary>
        /// Gets or sets property that contains an ID of a resident.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets property that contains a name of a resident.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets property that contains a password of a resident.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets property that contains all apartments where resident lives.
        /// </summary>
        public List<Flat> Flats { get; set; }

        /// <summary>
        /// Gets or sets property that contains the role of the resident.
        /// </summary>
        public ResidentRole Role { get; set; }

        /// <summary>
        /// Gets or sets property that contains the refresh token of the resident.
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Gets or sets property that contains expiry time of the token.
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}
