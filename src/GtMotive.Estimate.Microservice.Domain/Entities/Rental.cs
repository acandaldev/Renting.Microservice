using System;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a rental record linking a vehicle to a renter for a period of time.
    /// </summary>
    public class Rental
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Rental"/> class.
        /// </summary>
        /// <param name="id">Rental unique identifier.</param>
        /// <param name="vehicleId">The rented vehicle identifier.</param>
        /// <param name="renterId">The renter identifier.</param>
        public Rental(Guid id, VehicleId vehicleId, RenterId renterId)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Rental ID cannot be empty.", nameof(id));
            }

            Id = id;
            VehicleId = vehicleId ?? throw new ArgumentNullException(nameof(vehicleId));
            RenterId = renterId ?? throw new ArgumentNullException(nameof(renterId));
            StartDate = DateTime.UtcNow;
            EndDate = null;
        }

        /// <summary>
        /// Gets the rental unique identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the rented vehicle identifier.
        /// </summary>
        public VehicleId VehicleId { get; }

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        public RenterId RenterId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }

        /// <summary>
        /// Gets the rental end date. Null if the rental is still active.
        /// </summary>
        public DateTime? EndDate { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the rental is currently active.
        /// </summary>
        public bool IsActive => EndDate == null;

        /// <summary>
        /// Completes the rental by setting the end date.
        /// </summary>
        public void Complete()
        {
            EndDate = DateTime.UtcNow;
        }
    }
}
