using System;
using System.Threading.Tasks;
using Moq;
using Renting.Microservice.ApplicationCore.UseCases.RentVehicle;
using Renting.Microservice.Domain.Entities;
using Renting.Microservice.Domain.Exceptions;
using Renting.Microservice.Domain.Interfaces;
using Renting.Microservice.Domain.ValueObjects;
using Xunit;

namespace Renting.Microservice.UnitTests.ApplicationCore
{
    public class RentVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> vehicleRepo;
        private readonly Mock<IRentalRepository> rentalRepo;
        private readonly Mock<IRentVehicleOutputPort> outputPort;
        private readonly RentVehicleUseCase useCase;

        public RentVehicleUseCaseTests()
        {
            vehicleRepo = new Mock<IVehicleRepository>();
            rentalRepo = new Mock<IRentalRepository>();
            outputPort = new Mock<IRentVehicleOutputPort>();
            useCase = new RentVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, outputPort.Object);
        }

        [Fact]
        public async Task ExecuteWhenVehicleNotFoundShouldCallNotFound()
        {
            vehicleRepo.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Vehicle)null);

            await useCase.Execute(new RentVehicleInput(Guid.NewGuid(), "RENTER01"));

            outputPort.Verify(p => p.NotFoundHandle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteWhenRenterHasActiveRentalShouldThrow()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = CreateVehicle(vehicleId);
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            rentalRepo.Setup(r => r.HasActiveRental(It.IsAny<RenterId>())).ReturnsAsync(true);

            await Assert.ThrowsAsync<RenterAlreadyHasActiveRentalException>(
                () => useCase.Execute(new RentVehicleInput(vehicleId, "RENTER01")));
        }

        [Fact]
        public async Task ExecuteWhenValidShouldCallStandardHandle()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = CreateVehicle(vehicleId);
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            rentalRepo.Setup(r => r.HasActiveRental(It.IsAny<RenterId>())).ReturnsAsync(false);

            await useCase.Execute(new RentVehicleInput(vehicleId, "RENTER01"));

            outputPort.Verify(p => p.StandardHandle(It.IsAny<RentVehicleOutput>()), Times.Once);
            vehicleRepo.Verify(r => r.Update(It.Is<Vehicle>(v => !v.IsAvailable)), Times.Once);
            rentalRepo.Verify(r => r.Add(It.IsAny<Rental>()), Times.Once);
        }

        private static Vehicle CreateVehicle(Guid id)
        {
            return new Vehicle(
                new VehicleId(id),
                new LicensePlate("1234ABC"),
                new Brand("Toyota"),
                new Model("Corolla"),
                new ManufactureDate(DateTime.UtcNow.AddYears(-1)));
        }
    }
}
