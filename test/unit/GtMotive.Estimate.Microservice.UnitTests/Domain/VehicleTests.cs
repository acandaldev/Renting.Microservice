using System;
using GtMotive.Estimate.Microservice.Domain.Entities;
using GtMotive.Estimate.Microservice.Domain.Exceptions;
using GtMotive.Estimate.Microservice.Domain.ValueObjects;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Domain
{
    public class VehicleTests
    {
        [Fact]
        public void VehicleShouldBeAvailableWhenCreated()
        {
            var vehicle = CreateValidVehicle();

            Assert.True(vehicle.IsAvailable);
        }

        [Fact]
        public void MarkAsRentedShouldSetUnavailable()
        {
            var vehicle = CreateValidVehicle();

            vehicle.MarkAsRented();

            Assert.False(vehicle.IsAvailable);
        }

        [Fact]
        public void MarkAsRentedWhenAlreadyRentedShouldThrow()
        {
            var vehicle = CreateValidVehicle();
            vehicle.MarkAsRented();

            Assert.Throws<VehicleNotAvailableException>(() => vehicle.MarkAsRented());
        }

        [Fact]
        public void MarkAsReturnedShouldSetAvailable()
        {
            var vehicle = CreateValidVehicle();
            vehicle.MarkAsRented();

            vehicle.MarkAsReturned();

            Assert.True(vehicle.IsAvailable);
        }

        private static Vehicle CreateValidVehicle()
        {
            return new Vehicle(
                new VehicleId(Guid.NewGuid()),
                new LicensePlate("1234ABC"),
                new Brand("Toyota"),
                new Model("Corolla"),
                new ManufactureDate(DateTime.UtcNow.AddYears(-1)));
        }
    }
}
