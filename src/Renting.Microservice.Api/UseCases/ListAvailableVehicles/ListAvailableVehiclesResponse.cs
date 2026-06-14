using System.Collections.Generic;

namespace Renting.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesResponse
    {
        public IReadOnlyList<VehicleResponseItem> Vehicles { get; set; }
    }
}
