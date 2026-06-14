using MediatR;

namespace Renting.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesRequest : IRequest<IWebApiPresenter>
    {
    }
}
