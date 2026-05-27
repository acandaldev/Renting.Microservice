using System;
using System.Globalization;

namespace GtMotive.Estimate.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a vehicle's manufacture date exceeds the maximum allowed age for the fleet.
    /// </summary>
    public sealed class VehicleTooOldException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        public VehicleTooOldException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        public VehicleTooOldException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VehicleTooOldException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleTooOldException"/> class.
        /// </summary>
        /// <param name="manufactureDate">The manufacture date of the rejected vehicle.</param>
        /// <param name="maxAgeInYears">The maximum allowed age in years.</param>
        public VehicleTooOldException(DateTime manufactureDate, int maxAgeInYears)
            : base(string.Format(
                CultureInfo.InvariantCulture,
                "Vehicle with manufacture date {0:yyyy-MM-dd} exceeds the maximum allowed age of {1} years.",
                manufactureDate,
                maxAgeInYears))
        {
        }
    }
}
