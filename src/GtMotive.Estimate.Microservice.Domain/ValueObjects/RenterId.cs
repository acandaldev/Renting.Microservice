using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Unique identifier for a renter (person who rents a vehicle).
    /// </summary>
    public sealed class RenterId : IEquatable<RenterId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenterId"/> class.
        /// </summary>
        /// <param name="value">The renter identifier (e.g. DNI, passport, etc.).</param>
        public RenterId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Renter ID cannot be empty.", nameof(value));
            }

            Value = value.Trim();
        }

        /// <summary>
        /// Gets the renter identifier string.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determines whether two <see cref="RenterId"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(RenterId left, RenterId right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="RenterId"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(RenterId left, RenterId right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(RenterId other)
        {
            if (other is null)
            {
                return false;
            }

            return string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as RenterId);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return StringComparer.Ordinal.GetHashCode(Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value;
        }
    }
}
