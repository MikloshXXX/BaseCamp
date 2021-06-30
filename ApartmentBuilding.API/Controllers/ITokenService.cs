// <copyright file="ITokenService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System.Collections.Generic;
    using System.Security.Claims;

    /// <summary>
    /// Service for generating access and refresh tokens.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates access token for the resident.
        /// </summary>
        /// <param name="claims">Claims.</param>
        /// <returns>Access token.</returns>
        string GenerateAccessToken(IEnumerable<Claim> claims);

        /// <summary>
        /// Generates refresh token for the resident.
        /// </summary>
        /// <returns>Refresh token.</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Returns ClaimsPrincipal by token.
        /// </summary>
        /// <param name="token">Access token.</param>
        /// <returns>ClaimsPrincipal.</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}