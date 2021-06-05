namespace ApartmentBuilding.API.Requests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ApartmentBuilding.Core.Models;

    /// <summary>
    /// ResidentRequest class.
    /// </summary>
    public class ResidentRequest
    {
        /// <summary>
        /// Gets or sets property that contains name of the resident.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents ResidentRequest as Resident.
        /// </summary>
        /// <returns>Resident.</returns>
        public Resident ToModel()
        {
            return new Resident(default, this.Name);
        }
    }
}
