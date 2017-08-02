using System;
using System.Runtime.InteropServices;

namespace TundraEngine.Vulkan
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct Bool32 : IEquatable<Bool32>
    {
        private readonly int boolValue;
        
        public Bool32(bool boolValue)
        {
            this.boolValue = boolValue ? 1 : 0;
        }

        public Bool32(int boolValue)
        {
            this.boolValue = boolValue;
        }
        
        public bool Equals(Bool32 other)
        {
            return this.boolValue == other.boolValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Bool32 && Equals((Bool32)obj);
        }

        public override int GetHashCode()
        {
            return this.boolValue;
        }
        
        public static bool operator ==(Bool32 left, Bool32 right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(Bool32 left, Bool32 right)
        {
            return !left.Equals(right);
        }
        
        public static implicit operator bool(Bool32 booleanValue)
        {
            return booleanValue.boolValue != 0;
        }
        
        public static implicit operator Bool32(bool boolValue)
        {
            return new Bool32(boolValue);
        }

        public override string ToString()
        {
            return string.Format("{0}", boolValue != 0);
        }

        public static implicit operator int(Bool32 booleanValue)
        {
            return booleanValue.boolValue;
        }

        public static implicit operator Bool32(int boolValue)
        {
            return new Bool32(boolValue);
        }
    }
}