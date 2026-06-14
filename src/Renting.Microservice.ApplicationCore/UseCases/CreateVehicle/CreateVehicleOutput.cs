using System;

namespace Renting.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Output message for the Create Vehicle use case.
    /// </summary>
    public sealed class CreateVehicleOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleOutput"/> class.
        /// </summary>
        /// <param name="vehicleId">The created vehicle identifier.</param>
        public CreateVehicleOutput(Guid vehicleId)
        {
            VehicleId = vehicleId;
        }

        /// <summary>
        /// Gets the created vehicle identifier.
        /// </summary>
        public Guid VehicleId { get; }
    }
}
