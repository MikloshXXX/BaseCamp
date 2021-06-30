using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentBuilding.Core.Repositories
{
    public interface IResidentRepository<T>
    {
        /// <summary>
        /// Method returns list of elements in the collection.
        /// </summary>
        /// <returns>Collection of elements.</returns>
        public Task<IEnumerable<T>> Get();

        /// <summary>
        /// Method returns specific element by its ID.
        /// </summary>
        /// <param name="id">ID of specific element.</param>
        /// <returns>Specific element by its ID.</returns>
        public Task<T> GetByID(int id);

        /// <summary>
        /// Method for creating an entity and add it to the collection.
        /// </summary>
        /// <param name="entity">Created entity.</param>
        /// <returns>Succes of the operation.</returns>
        public Task<bool> Create(T entity);

        /// <summary>
        /// Method for deleting an entity from the collection by ID.
        /// </summary>
        /// <param name="id">ID of deleting element.</param>
        /// <returns>Succes of the operation.</returns>
        public Task<bool> Delete(int id);

        /// <summary>
        /// Method for updating an specific element from the collection.
        /// </summary>
        /// <param name="id">ID of updating entity.</param>
        /// <param name="entity">New entity.</param>
        /// <returns>Succes of the operation.</returns>
        public Task<bool> Update(int id, T entity);

        /// <summary>
        /// Updates refresh token and expiry time of the resident.
        /// </summary>
        /// <param name="id">ID.</param>
        /// <param name="refreshToken">RefreshToken.</param>
        /// <param name="expiryTime">RefreshToken expiry time.</param>
        /// <returns>Success of the operation.</returns>
        public Task<bool> UpdateRefreshToken(int id, string refreshToken, DateTime? expiryTime = null);
    }
}
