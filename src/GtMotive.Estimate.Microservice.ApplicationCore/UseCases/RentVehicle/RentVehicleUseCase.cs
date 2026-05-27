using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.Interfaces;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Rents a vehicle to a given renter.
    /// Enforces business rules: one active rental per renter, vehicle must be available.
    /// </summary>
    public sealed class RentVehicleUseCase : IRentVehicleUseCase
    {
        private readonly IVehicleRepository vehicleRepository;
        private readonly IRentalRepository rentalRepository;
        private readonly IRentVehicleOutputPort outputPort;

        /// <summary>
        /// Initializes a new instance of the <see cref="RentVehicleUseCase"/> class.
        /// </summary>
        /// <param name="vehicleRepository">Vehicle repository.</param>
        /// <param name="rentalRepository">Rental repository.</param>
        /// <param name="outputPort">Output port for the result.</param>
        public RentVehicleUseCase(
            IVehicleRepository vehicleRepository,
            IRentalRepository rentalRepository,
            IRentVehicleOutputPort outputPort)
        {
            this.vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
            this.rentalRepository = rentalRepository ?? throw new ArgumentNullException(nameof(rentalRepository));
            this.outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));
        }

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var vehicle = await vehicleRepository.GetById(input.VehicleId);
            if (vehicle is null)
            {
                outputPort.NotFoundHandle($"Vehicle with ID '{input.VehicleId}' was not found.");
                return;
            }

            var renterId = new RenterId(input.RenterId);

            var hasActiveRental = await rentalRepository.HasActiveRental(renterId);
            if (hasActiveRental)
            {
                throw new RenterAlreadyHasActiveRentalException(renterId.Value);
            }

            vehicle.MarkAsRented();
            await vehicleRepository.Update(vehicle);

            var rental = new Rental(Guid.NewGuid(), vehicle.Id, renterId);
            await rentalRepository.Add(rental);

            outputPort.StandardHandle(new RentVehicleOutput(rental.Id, vehicle.Id.Value, rental.StartDate));
        }
    }
}
