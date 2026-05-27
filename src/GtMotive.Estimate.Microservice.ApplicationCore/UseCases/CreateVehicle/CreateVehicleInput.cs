using System;

namespace GtMotive.Estimate.Microservice.ApplicationCore.UseCases.CreateVehicle
{
    /// <summary>
    /// Input message for the Create Vehicle use case.
    /// </summary>
    public sealed class CreateVehicleInput : IUseCaseInput
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CreateVehicleInput"/> class.
        /// </summary>
        /// <param name="licensePlate">License plate number.</param>
        /// <param name="brand">Vehicle brand.</param>
        /// <param name="model">Vehicle model.</param>
        /// <param name="manufactureDate">Date of manufacture.</param>
        public CreateVehicleInput(string licensePlate, string brand, string model, DateTime manufactureDate)
        {
            LicensePlate = licensePlate;
            Brand = brand;
            Model = model;
            ManufactureDate = manufactureDate;
        }

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
