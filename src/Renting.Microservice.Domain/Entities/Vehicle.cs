using System;
using Renting.Microservice.Domain.Exceptions;
using Renting.Microservice.Domain.ValueObjects;

namespace Renting.Microservice.Domain.Entities
{
    /// <summary>
    /// Represents a vehicle in the rental fleet. This is the Aggregate Root.
    /// </summary>
    public class Vehicle
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vehicle"/> class.
        /// </summary>
        /// <param name="id">Vehicle unique identifier.</param>
        /// <param name="licensePlate">License plate number.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="manufactureDate">Date of manufacture.</param>
        public Vehicle(
            VehicleId id,
            LicensePlate licensePlate,
            Brand brand,
            Model model,
            ManufactureDate manufactureDate)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            LicensePlate = licensePlate ?? throw new ArgumentNullException(nameof(licensePlate));
            Brand = brand ?? throw new ArgumentNullException(nameof(brand));
            Model = model ?? throw new ArgumentNullException(nameof(model));
            ManufactureDate = manufactureDate ?? throw new ArgumentNullException(nameof(manufactureDate));
            IsAvailable = true;
        }

        /// <summary>
        /// Gets the vehicle unique identifier.
        /// </summary>
        public VehicleId Id { get; }

        /// <summary>
        /// Gets the license plate.
        /// </summary>
        public LicensePlate LicensePlate { get; }

        /// <summary>
        /// Gets the brand.
        /// </summary>
        public Brand Brand { get; }

        /// <summary>
        /// Gets the model.
        /// </summary>
        public Model Model { get; }

        /// <summary>
        /// Gets the manufacture date.
        /// </summary>
        public ManufactureDate ManufactureDate { get; }

        /// <summary>
        /// Gets a value indicating whether the vehicle is available for rent.
        /// </summary>
        public bool IsAvailable { get; private set; }

        /// <summary>
        /// Marks the vehicle as rented (unavailable).
        /// </summary>
        public void MarkAsRented()
        {
            if (!IsAvailable)
            {
                throw new VehicleNotAvailableException(Id.ToString());
            }

            IsAvailable = false;
        }

        /// <summary>
        /// Marks the vehicle as returned (available again).
        /// </summary>
        public void MarkAsReturned()
        {
            IsAvailable = true;
        }
    }
}
