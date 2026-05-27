using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Output message for the Return Vehicle use case.
    /// </summary>
    public sealed class ReturnVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleOutput"/> class.
        /// </summary>
        /// <param name="vehicleId">The returned vehicle identifier.</param>
        /// <param name="endDate">The rental end date.</param>
        public ReturnVehicleOutput(Guid vehicleId, DateTime endDate)
        {
            VehicleId = vehicleId;
            EndDate = endDate;
        }

        /// <summary>
        /// Gets the returned vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }

        /// <summary>
        /// Gets the rental end date.
        /// </summary>
        public DateTime EndDate { get; }
    }
}
