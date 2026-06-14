using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Renting.Microservice.Domain.Entities;

namespace Renting.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for vehicle persistence operations.
    /// </summary>
    public interface IVehicleRepository
    {
        /// <summary>
        /// Adds a new vehicle to the fleet.
        /// </summary>
        /// <param name="vehicle">The vehicle to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(Vehicle vehicle);

        /// <summary>
        /// Gets a vehicle by its identifier.
        /// </summary>
        /// <param name="id">The vehicle identifier.</param>
        /// <returns>The vehicle if found; otherwise, null.</returns>
        Task<Vehicle> GetById(Guid id);

        /// <summary>
        /// Gets all available vehicles in the fleet.
        /// </summary>
        /// <returns>A list of available vehicles.</returns>
        Task<IReadOnlyList<Vehicle>> GetAvailable();

        /// <summary>
        /// Updates an existing vehicle.
        /// </summary>
        /// <param name="vehicle">The vehicle with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(Vehicle vehicle);
    }
}
