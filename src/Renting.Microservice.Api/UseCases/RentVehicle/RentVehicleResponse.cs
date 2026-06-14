using System;

namespace Renting.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehicleResponse
    {
        public Guid RentalId { get; set; }

        public Guid VehicleId { get; set; }

        public DateTime StartDate { get; set; }
    }
}
