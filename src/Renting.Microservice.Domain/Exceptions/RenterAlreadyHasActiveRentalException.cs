using System;

namespace Renting.Microservice.Domain.Exceptions
{
    /// <summary>
    /// Thrown when a renter tries to rent a vehicle but already has an active rental.
    /// </summary>
    public sealed class RenterAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public RenterAlreadyHasActiveRentalException()
            : base()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">The error message.</param>
        /// <param name="innerException">The inner exception.</param>
        public RenterAlreadyHasActiveRentalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="renterId">The renter identifier.</param>
        public RenterAlreadyHasActiveRentalException(string renterId)
            : base($"Renter '{renterId}' already has an active rental. Only one active rental per renter is allowed.")
        {
        }
    }
}
