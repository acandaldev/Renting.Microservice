using System;

namespace GtMotive.Estimate.Microservice.Domain.ValueObjects
{
    /// <summary>
    /// Unique identifier for a vehicle in the fleet.
    /// </summary>
    public sealed class VehicleId : IEquatable<VehicleId>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> class.
        /// </summary>
        /// <param name="value">The unique identifier value.</param>
        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("Vehicle ID cannot be empty.", nameof(value));
            }

            Value = value;
        }

        /// <summary>
        /// Gets the underlying identifier value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Determines whether two <see cref="VehicleId"/> instances are equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are equal.</returns>
        public static bool operator ==(VehicleId left, VehicleId right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two <see cref="VehicleId"/> instances are not equal.
        /// </summary>
        /// <param name="left">The left operand.</param>
        /// <param name="right">The right operand.</param>
        /// <returns>True if both instances are not equal.</returns>
        public static bool operator !=(VehicleId left, VehicleId right)
        {
            return !Equals(left, right);
        }

        /// <inheritdoc/>
        public bool Equals(VehicleId other)
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
            return Equals(obj as VehicleId);
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
