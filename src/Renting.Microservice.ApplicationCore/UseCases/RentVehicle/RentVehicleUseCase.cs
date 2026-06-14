using System;
using System.Threading.Tasks;
using Renting.Microservice.Domain.Entities;
using Renting.Microservice.Domain.Exceptions;
using Renting.Microservice.Domain.Interfaces;
using Renting.Microservice.Domain.ValueObjects;

namespace Renting.Microservice.ApplicationCore.UseCases.RentVehicle
{
    /// <summary>
    /// Rents a vehicle to a given renter.
    /// Enforces business rules: one active rental per renter, vehicle must be available.
    /// </summary>
    public sealed class RentVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IRentalRepository rentalRepository,
        IRentVehicleOutputPort outputPort) : IRentVehicleUseCase
    {
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
