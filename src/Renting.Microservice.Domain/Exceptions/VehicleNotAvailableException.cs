using System;

namespace Renting.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when trying to rent a vehicle that is not available.
    /// </summary>
    public sealed class VehicleNotAvailableException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        public VehicleNotAvailableException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="vehicleId">The vehicle identifier.</param>
        public VehicleNotAvailableException(string vehicleId)
            : base($"Vehicle '{vehicleId}' is not available for rent.")
        {
        }
    }
}
