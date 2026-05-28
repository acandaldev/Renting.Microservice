using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehicleRequest : IRequest<IWebApiPresenter>
    {
        required public Guid VehicleId { get; set; }

        public string RenterId { get; set; }
    }
}
