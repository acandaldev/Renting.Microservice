using System;
using System.Linq;
using System.Threading.Tasks;
using Renting.Microservice.Domain.Interfaces;

namespace Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Lists all vehicles currently available for rent.
    /// </summary>
    public sealed class ListAvailableVehiclesUseCase : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IListAvailableVehiclesOutputPort outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ListAvailableVehiclesUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="outputPort">Output port for the result.</param>
        public ListAvailableVehiclesUseCase(
            IVehicleRepository vehicleRepository,
            IListAvailableVehiclesOutputPort outputPort)
        {
            this.vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <inheritdoc/>
        public async Task Execute(ListAvailableVehiclesInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicles = await vehicleRepository.GetAvailable();

            var items = vehicles
                .Select(v => new VehicleItem(
                    v.Id.Value,
                    v.LicensePlate.Value,
                    v.Brand.Value,
                    v.Model.Value,
                    v.ManufactureDate.Value))
                .ToList()
                .AsReadOnly();

            outputPort.StandardHandle(new ListAvailableVehiclesOutput(items));
        }
    }
}
