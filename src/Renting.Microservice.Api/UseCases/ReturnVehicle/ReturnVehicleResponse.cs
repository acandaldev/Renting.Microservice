using System;

namespace Renting.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleResponse
    {
        public Guid VehicleId { get; set; }

        public DateTime EndDate { get; set; }
    }
}
