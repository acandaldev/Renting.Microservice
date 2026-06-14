using System;

namespace Renting.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Vehicle model name.
    /// </summary>
    public sealed class Model : IEquatable<Model>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Model"/> class.
        /// </summary>
        /// <param name="value">The model name.</param>
        public Model(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Model cannot be empty.", nameof(value));
            }

            Value = value.Trim();
        }

        /// <summary>
        /// Gets the model name.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Determines whether two <see cref="Model"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(Model left, Model right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="Model"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(Model left, Model right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(Model other)
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
            return Equals(obj as Model);
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
