using System;
using System.Threading.Tasks;
using Renting.Microservice.Domain.Interfaces;

namespace Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle
{
    /// <summary>
    /// Returns a rented vehicle, making it available again.
    /// </summary>
    public sealed class ReturnVehicleUseCase : IReturnVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IRentalRepository rentalRepository;
        private readonly IReturnVehicleOutputPort outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReturnVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="rentalRepository">Rental repository.</param>
        /// <param name="outputPort">Output port for the result.</param>
        public ReturnVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IReturnVehicleOutputPort outputPort)
        {
            this.vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            this.rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicle = await vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                outputPort.NotFoundHandle($"Vehicle with ID '{input.VehicleId}' was not found.");
                return;
            }

            var rental = await rentalRepository.GetActiveByVehicleId(input.VehicleId);
            if (rental is null)
            {
                outputPort.NotFoundHandle($"No active rental found for vehicle '{input.VehicleId}'.");
                return;
            }

            rental.Complete();
            await rentalRepository.Update(rental);

            vehicle.MarkAsReturned();
            await vehicleRepository.Update(vehicle);

            outputPort.StandardHandle(new ReturnVehicleOutput(vehicle.Id.Value, rental.EndDate.Value));
        }
    }
}
