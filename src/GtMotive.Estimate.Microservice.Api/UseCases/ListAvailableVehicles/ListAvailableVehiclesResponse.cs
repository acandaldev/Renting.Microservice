using System.Collections.Generic;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesResponse
    {
        public IReadOnlyList<VehicleResponseItem> Vehicles { get; set; }
    }
}
