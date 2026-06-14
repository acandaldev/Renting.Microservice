using System;

namespace Renting.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Input message for the Rent Vehicle use case.
    /// </summary>
    public sealed class RentVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleInput"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle to rent.</param>
        /// <param name="renterId">The renter identifier.</param>
        public RentVehicleInput(Guid vehicleId, string renterId)
        {
            VehicleId = vehicleId;
            RenterId = renterId;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the renter identifier.
        /// </summary>
        public string RenterId { get; }
    }
}
