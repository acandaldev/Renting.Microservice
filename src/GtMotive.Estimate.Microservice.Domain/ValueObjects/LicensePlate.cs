using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle license plate number.
    /// </summary>
    public sealed class LicensePlate : IEquatable<LicensePlate>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> class.
        /// </summary>
        /// <param name="value">The license plate string.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("License plate cannot be empty.", nameof(value));
            }

            Value = value.Trim().ToUpperInvariant();
        }

        /// <summary>
        /// Gets the license plate string.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determines whether two <see cref="LicensePlate"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(LicensePlate left, LicensePlate right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="LicensePlate"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(LicensePlate left, LicensePlate right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(LicensePlate other)
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
            return Equals(obj as LicensePlate);
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
