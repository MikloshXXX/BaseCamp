// <copyright file="AuthController.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ApartmentBuilding.API.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using System.Threading.Tasks;
    using ApartmentBuilding.API.Requests;
    using ApartmentBuilding.Core.Models;
    using ApartmentBuilding.Core.Repositories;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// Controller for auth.
    /// </summary>
    public class AuthController : ControllerBase
    {
        private readonly IResidentRepository<Resident> repository;
        private readonly ITokenService tokenService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthController"/> class.
        /// </summary>
        /// <param name="repository">Resident repo.</param>
        /// <param name="tokenService">IToken service.</param>
        public AuthController(IResidentRepository<Resident> repository, ITokenService tokenService)
        {
            this.repository = repository;
            this.tokenService = tokenService;
        }

        /// <summary>
        /// Return access and refresh tokens by iD and password of the resident.
        /// </summary>
        /// <param name="request">ID and password.</param>
        /// <returns>Access and Refresh tokens.</returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] AuthenticateResidentRequest request)
        {
            if (request == null)
            {
                return this.BadRequest("Invalid cliend request");
            }

            var resident = await this.repository.GetByID(request.ID);
            if (resident != null)
            {
                if (resident.Password == request.Password)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, request.ID.ToString()),
                        new Claim(ClaimTypes.Role, resident.Role.ToString()),
                    };

                    var accessToken = this.tokenService.GenerateAccessToken(claims);
                    var refreshToken = this.tokenService.GenerateRefreshToken();

                    await this.repository.UpdateRefreshToken(request.ID, refreshToken, DateTime.Now.AddDays(7));

                    return this.Ok(new
                    {
                        AccessToken = accessToken,
                        RefreshToken = refreshToken,
                    });
                }
            }

            return this.Unauthorized();
        }

        /// <summary>
        /// Refresh current access token by refresh token.
        /// </summary>
        /// <param name="token">Access and refresh tokens.</param>
        /// <returns>New access and refresh tokens.</returns>
        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> Refresh([FromBody]TokenApiModel token)
        {
            if (token is null)
            {
                return this.BadRequest("Invalid client request");
            }

            string accessToken = token.AccessToken;
            string refreshToken = token.RefreshToken;

            var principal = this.tokenService.GetPrincipalFromExpiredToken(accessToken);
            var id = principal.Identity.Name;

            var resident = await this.repository.GetByID(Convert.ToInt32(id));
            if (resident == null || resident.RefreshToken != refreshToken || resident.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return this.BadRequest("Invalid client request");
            }

            var newAccessToken = this.tokenService.GenerateAccessToken(principal.Claims);
            var newRefreshToken = this.tokenService.GenerateRefreshToken();

            await this.repository.UpdateRefreshToken(Convert.ToInt32(id), newRefreshToken);

            return this.Ok(new
            {
                AccessToken = newAccessToken,
                RefreshToken = newRefreshToken,
            });
        }
    }
}
