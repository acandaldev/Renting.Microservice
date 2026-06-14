using System;
using System.Collections.Generic;

namespace Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Output message for the List Available Vehicles use case.
    /// </summary>
    public sealed class ListAvailableVehiclesOutput : IUseCaseOutput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesOutput"/> class.
        /// </summary>
        /// <param name="vehicles">The list of available vehicles.</param>
        public ListAvailableVehiclesOutput(IReadOnlyList<VehicleItem> vehicles)
        {
            Vehicles = vehicles ?? throw new ArgumentNullException(nameof(vehicles));
        }

        /// <summary>
        /// Gets the list of available vehicles.
        /// </summary>
        public IReadOnlyList<VehicleItem> Vehicles { get; }
    }
}
