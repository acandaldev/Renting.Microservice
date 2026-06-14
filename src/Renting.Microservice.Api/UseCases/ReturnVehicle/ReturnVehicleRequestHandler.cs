using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle;

namespace Renting.Microservice.Api.UseCases.ReturnVehicle
{
    public sealed class ReturnVehicleRequestHandler : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly IReturnVehicleUseCase useCase;
        private readonly ReturnVehiclePresenter presenter;

        public ReturnVehicleRequestHandler(IReturnVehicleUseCase useCase, ReturnVehiclePresenter presenter)
        {
            this.useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.VehicleId);
            await useCase.Execute(input);

            return presenter;
        }
    }
}
