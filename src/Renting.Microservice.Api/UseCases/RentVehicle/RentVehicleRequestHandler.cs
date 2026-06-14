using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Renting.Microservice.ApplicationCore.UseCases.RentVehicle;

namespace Renting.Microservice.Api.UseCases.RentVehicle
{
    public sealed class RentVehicleRequestHandler : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly IRentVehicleUseCase useCase;
        private readonly RentVehiclePresenter presenter;

        public RentVehicleRequestHandler(IRentVehicleUseCase useCase, RentVehiclePresenter presenter)
        {
            this.useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.VehicleId, request.RenterId);
            await useCase.Execute(input);

            return presenter;
        }
    }
}
