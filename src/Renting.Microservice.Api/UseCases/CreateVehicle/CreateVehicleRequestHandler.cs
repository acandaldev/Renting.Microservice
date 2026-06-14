using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Renting.Microservice.ApplicationCore.UseCases.CreateVehicle;

namespace Renting.Microservice.Api.UseCases.CreateVehicle
{
    public sealed class CreateVehicleRequestHandler : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly ICreateVehicleUseCase useCase;
        private readonly CreateVehiclePresenter presenter;

        public CreateVehicleRequestHandler(ICreateVehicleUseCase useCase, CreateVehiclePresenter presenter)
        {
            this.useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
            this.presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));
        }

        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(
                request.LicensePlate,
                request.Brand,
                request.Model,
                request.ManufactureDate);

            await useCase.Execute(input);

            return presenter;
        }
    }
}
