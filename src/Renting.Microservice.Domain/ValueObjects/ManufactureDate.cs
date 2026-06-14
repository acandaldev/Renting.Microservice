using System;

namespace Renting.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle manufacture date. Enforces the business rule that
    /// vehicles older than 5 years cannot be part of the fleet.
    /// </summary>
    public sealed class ManufactureDate : IEquatable<ManufactureDate>
    {
        /// <summary>
        /// Maximum vehicle age in years allowed in the fleet.
        /// </summary>
        public const int MaxAgeInYears = 5;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManufactureDate"/> class.
        /// </summary>
        /// <param name="value">The manufacture date.</param>
        public ManufactureDate(DateTime value)
        {
            if (value > DateTime.UtcNow)
            {
                throw new ArgumentException("Manufacture date cannot be in the future.", nameof(value));
            }

            var ageLimit = DateTime.UtcNow.AddYears(-MaxAgeInYears);
            if (value < ageLimit)
            {
                throw new Exceptions.VehicleTooOldException(value, MaxAgeInYears);
            }

            Value = value.Date;
        }

        /// <summary>
        /// Gets the manufacture date value.
        /// </summary>
        public DateTime Value { get; }

        /// <summary>
        /// Determines whether two <see cref="ManufactureDate"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(ManufactureDate left, ManufactureDate right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="ManufactureDate"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(ManufactureDate left, ManufactureDate right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(ManufactureDate other)
        {
            if (other is null)
            {
                return false;
            }

            return Value == other.Value;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as ManufactureDate);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
        }
    }
}
