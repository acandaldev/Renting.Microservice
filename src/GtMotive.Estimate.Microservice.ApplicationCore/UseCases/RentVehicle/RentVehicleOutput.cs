using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Output message for the Rent Vehicle use case.
    /// </summary>
    public sealed class RentVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleOutput"/> class.
        /// </summary>
        /// <param name="rentalId">The created rental identifier.</param>
        /// <param name="vehicleId">The rented vehicle identifier.</param>
        /// <param name="startDate">The rental start date.</param>
        public RentVehicleOutput(Guid rentalId, Guid vehicleId, DateTime startDate)
        {
            RentalId = rentalId;
            VehicleId = vehicleId;
            StartDate = startDate;
        }

        /// <summary>
        /// Gets the rental identifier.
        /// </summary>
        public Guid RentalId { get; }

        /// <summary>
        /// Gets the rented vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the rental start date.
        /// </summary>
        public DateTime StartDate { get; }
    }
}
