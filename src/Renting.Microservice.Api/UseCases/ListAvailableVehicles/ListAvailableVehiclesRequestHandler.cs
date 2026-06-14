using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles;

namespace Renting.Microservice.Api.UseCases.ListAvailableVehicles
{
    public sealed class ListAvailableVehiclesRequestHandler : IRequestHandler<ListAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly IListAvailableVehiclesUseCase useCase;
        private readonly ListAvailableVehiclesPresenter presenter;

        public ListAvailableVehiclesRequestHandler(
            IListAvailableVehiclesUseCase useCase,
            ListAvailableVehiclesPresenter presenter)
        {
            this.useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await useCase.Execute(new ListAvailableVehiclesInput());

            return presenter;
        }
    }
}
