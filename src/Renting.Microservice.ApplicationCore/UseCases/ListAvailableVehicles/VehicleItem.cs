using System;

namespace Renting.Microservice.ApplicationCore.UseCases.ListAvailableVehicles
{
    /// <summary>
    /// Represents a single vehicle item in the list output.
    /// </summary>
    public sealed class VehicleItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleItem"/> class.
        /// </summary>
        /// <param name="id">Vehicle identifier.</param>
        /// <param name="licensePlate">License plate.</param>
        /// <param name="brand">Brand.</param>
        /// <param name="model">Model.</param>
        /// <param name="manufactureDate">Manufacture date.</param>
        public VehicleItem(Guid id, string licensePlate, string brand, string model, DateTime manufactureDate)
        {
            Id = id;
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
        }

        /// <summary>
        /// Gets the vehicle identifier.
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Gets the license plate.
        /// </summary>
        public string LicensePlate { get; }

        /// <summary>
        /// Gets the brand.
        /// </summary>
        public string Brand { get; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public string Model { get; }

        /// <summary>
        /// Gets the manufacture date.
        /// </summary>
        public DateTime ManufactureDate { get; }
    }
}
