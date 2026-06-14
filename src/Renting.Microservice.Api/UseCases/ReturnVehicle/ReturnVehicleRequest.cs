using System;
using MediatR;

namespace Renting.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleRequest : IRequest<IWebApiPresenter>
    {
        public Guid VehicleId { get; set; }
    }
}
