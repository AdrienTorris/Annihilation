using System;
using System.Runtime.InteropServices;

using TundraEngine.Mathematics;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadState : IEquatable<GamepadState>
    {
        /// <summary>
        /// A boolean indicating the connect status of this gamepad. <c>true</c> if connected, otherwise <c>false</c>.
        /// </summary>
        public bool IsConnected;
        /// <summary>
        /// Bitmask of the gamepad buttons.
        /// </summary>
        public GamepadButton Buttons;
        /// <summary>
        /// Left thumbstick x-axis/y-axis value. The value is in the range [-1.0f, 1.0f] for both axis.
        /// </summary>
        public Vector2 LeftThumb;
        /// <summary>
        /// Right thumbstick x-axis/y-axis value. The value is in the range [-1.0f, 1.0f] for both axis.
        /// </summary>
        public Vector2 RightThumb;
        /// <summary>
        /// The left trigger analog control in the range [0, 1.0f]. See remarks.
        /// </summary>
        /// <remarks>
        /// Some controllers are not supporting the range of value and may act as a simple button returning only 0 or 1.
        /// </remarks>
        public float LeftTrigger;
        /// <summary>
        /// The right trigger analog control in the range [0, 1.0f]. See remarks.
        /// </summary>
        /// <remarks>
        /// Some controllers are not supporting the range of value and may act as a simple button returning only 0 or 1.
        /// </remarks>
        public float RightTrigger;

        public bool Equals(GamepadState other)
        {
            return IsConnected.Equals(other.IsConnected) && Buttons.Equals(other.Buttons) && LeftThumb.Equals(other.LeftThumb) && RightThumb.Equals(other.RightThumb) && LeftTrigger.Equals(other.LeftTrigger) && RightTrigger.Equals(other.RightTrigger);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is GamepadState && Equals((GamepadState)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = IsConnected.GetHashCode();
                hashCode = (hashCode * 397) ^ Buttons.GetHashCode();
                hashCode = (hashCode * 397) ^ LeftThumb.GetHashCode();
                hashCode = (hashCode * 397) ^ RightThumb.GetHashCode();
                hashCode = (hashCode * 397) ^ LeftTrigger.GetHashCode();
                hashCode = (hashCode * 397) ^ RightTrigger.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(GamepadState left, GamepadState right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(GamepadState left, GamepadState right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("IsConnected: {0}, Buttons: {1}, LeftThumb: {2}, RightThumb: {3}, LeftTrigger: {4}, RightTrigger: {5}", IsConnected, Buttons, LeftThumb, RightThumb, LeftTrigger, RightTrigger);
        }
    }
}