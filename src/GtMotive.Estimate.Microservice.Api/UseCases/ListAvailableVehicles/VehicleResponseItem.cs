using System;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class VehicleResponseItem
    {
        public Guid Id { get; set; }

        public string LicensePlate { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public DateTime ManufactureDate { get; set; }
    }
}
