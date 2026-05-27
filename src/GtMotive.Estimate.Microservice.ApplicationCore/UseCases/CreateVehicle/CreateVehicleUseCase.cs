using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Creates a new vehicle and adds it to the fleet.
    /// </summary>
    public sealed class CreateVehicleUseCase : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly ICreateVehicleOutputPort outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="outputPort">Output port for the result.</param>
        public CreateVehicleUseCase(
            IVehicleRepository vehicleRepository,
            ICreateVehicleOutputPort outputPort)
        {
            this.vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <inheritdoc/>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicleId = new VehicleId(Guid.NewGuid());
            var licensePlate = new LicensePlate(input.LicensePlate);
            var brand = new Brand(input.Brand);
            var model = new Domain.ValueObjects.Model(input.Model);
            var manufactureDate = new ManufactureDate(input.ManufactureDate);

            var vehicle = new Vehicle(vehicleId, licensePlate, brand, model, manufactureDate);

            await vehicleRepository.Add(vehicle);

            outputPort.StandardHandle(new CreateVehicleOutput(vehicleId.Value));
        }
    }
}
