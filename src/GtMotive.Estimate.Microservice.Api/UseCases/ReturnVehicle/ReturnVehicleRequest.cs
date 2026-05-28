using System;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        public Guid VehicleId { get; set; }
    }
}
