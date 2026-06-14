using System;
using Renting.Microservice.Domain.Exceptions;
using Renting.Microservice.Domain.ValueObjects;
using Xunit;

namespace Renting.Microservice.UnitTests.Domain
{
    public class ManufactureDateTests
    {
        [Fact]
        public void ManufactureDateWithValidDateShouldCreate()
        {
            var date = DateTime.UtcNow.AddYears(-2);
            var manufactureDate = new ManufactureDate(date);

            Assert.Equal(date.Date, manufactureDate.Value);
        }

        [Fact]
        public void ManufactureDateOlderThanFiveYearsShouldThrow()
        {
            var tooOld = DateTime.UtcNow.AddYears(-6);

            Assert.Throws<VehicleTooOldException>(() => new ManufactureDate(tooOld));
        }

        [Fact]
        public void ManufactureDateInFutureShouldThrow()
        {
            var future = DateTime.UtcNow.AddDays(1);

            Assert.Throws<ArgumentException>(() => new ManufactureDate(future));
        }
    }
}
