using System;
using System.Threading.Tasks;
using Moq;
using Renting.Microservice.ApplicationCore.UseCases.ReturnVehicle;
using Renting.Microservice.Domain.Entities;
using Renting.Microservice.Domain.Interfaces;
using Renting.Microservice.Domain.ValueObjects;
using Xunit;

namespace Renting.Microservice.UnitTests.ApplicationCore
{
    public class ReturnVehicleUseCaseTests
    {
        private readonly Mock<IVehicleRepository> vehicleRepo;
        private readonly Mock<IRentalRepository> rentalRepo;
        private readonly Mock<IReturnVehicleOutputPort> outputPort;
        private readonly ReturnVehicleUseCase useCase;

        public ReturnVehicleUseCaseTests()
        {
            vehicleRepo = new Mock<IVehicleRepository>();
            rentalRepo = new Mock<IRentalRepository>();
            outputPort = new Mock<IReturnVehicleOutputPort>();
            useCase = new ReturnVehicleUseCase(vehicleRepo.Object, rentalRepo.Object, outputPort.Object);
        }

        [Fact]
        public async Task ExecuteWhenVehicleNotFoundShouldCallNotFound()
        {
            vehicleRepo.Setup(r => r.GetById(It.IsAny<Guid>())).ReturnsAsync((Vehicle)null);

            await useCase.Execute(new ReturnVehicleInput(Guid.NewGuid()));

            outputPort.Verify(p => p.NotFoundHandle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteWhenNoActiveRentalShouldCallNotFound()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = CreateRentedVehicle(vehicleId);
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            rentalRepo.Setup(r => r.GetActiveByVehicleId(vehicleId)).ReturnsAsync((Rental)null);

            await useCase.Execute(new ReturnVehicleInput(vehicleId));

            outputPort.Verify(p => p.NotFoundHandle(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task ExecuteWhenValidShouldCompleteRentalAndReturnVehicle()
        {
            var vehicleId = Guid.NewGuid();
            var vehicle = CreateRentedVehicle(vehicleId);
            var rental = new Rental(Guid.NewGuid(), new VehicleId(vehicleId), new RenterId("RENTER01"));
            vehicleRepo.Setup(r => r.GetById(vehicleId)).ReturnsAsync(vehicle);
            rentalRepo.Setup(r => r.GetActiveByVehicleId(vehicleId)).ReturnsAsync(rental);

            await useCase.Execute(new ReturnVehicleInput(vehicleId));

            outputPort.Verify(p => p.StandardHandle(It.IsAny<ReturnVehicleOutput>()), Times.Once);
            vehicleRepo.Verify(r => r.Update(It.Is<Vehicle>(v => v.IsAvailable)), Times.Once);
            rentalRepo.Verify(r => r.Update(It.Is<Rental>(re => !re.IsActive)), Times.Once);
        }

        private static Vehicle CreateRentedVehicle(Guid id)
        {
            var vehicle = new Vehicle(
                new VehicleId(id),
                new LicensePlate("5678DEF"),
                new Brand("Ford"),
                new Model("Focus"),
                new ManufactureDate(DateTime.UtcNow.AddYears(-2)));
            vehicle.MarkAsRented();
            return vehicle;
        }
    }
}
