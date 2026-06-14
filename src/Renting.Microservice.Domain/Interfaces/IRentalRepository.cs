using System;
using System.Threading.Tasks;
using Renting.Microservice.Domain.Entities;
using Renting.Microservice.Domain.ValueObjects;

namespace Renting.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Port for rental persistence operations.
    /// </summary>
    public interface IRentalRepository
    {
        /// <summary>
        /// Adds a new rental record.
        /// </summary>
        /// <param name="rental">The rental to add.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Add(Rental rental);

        /// <summary>
        /// Checks if a renter has an active (not returned) rental.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        /// <returns>True if the renter has an active rental.</returns>
        Task<bool> HasActiveRental(RenterId renterId);

        /// <summary>
        /// Gets the active rental for a specific vehicle.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        /// <returns>The active rental if found; otherwise, null.</returns>
        Task<Rental> GetActiveByVehicleId(Guid vehicleId);

        /// <summary>
        /// Updates an existing rental record.
        /// </summary>
        /// <param name="rental">The rental with updated data.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task Update(Rental rental);
    }
}
