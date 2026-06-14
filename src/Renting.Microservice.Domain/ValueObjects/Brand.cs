using System;

namespace Renting.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle brand (manufacturer).
    /// </summary>
    public sealed class Brand : IEquatable<Brand>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Brand"/> class.
        /// </summary>
        /// <param name="value">The brand name.</param>
        public Brand(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Brand cannot be empty.", nameof(value));
            }

            Value = value.Trim();
        }

        /// <summary>
        /// Gets the brand name.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determines whether two <see cref="Brand"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(Brand left, Brand right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="Brand"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(Brand left, Brand right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(Brand other)
        {
            if (other is null)
            {
                return false;
            }

            return string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            return Equals(obj as Brand);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return StringComparer.OrdinalIgnoreCase.GetHashCode(Value);
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value;
        }
    }
}
