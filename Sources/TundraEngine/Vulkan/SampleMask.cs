using System;
using System.Runtime.InteropServices;

namespace Engine.Vulkan
{
    [StructLayout(LayoutKind.Sequential, Size = 4)]
    public struct SampleMask : IEquatable<SampleMask>
    {
        private readonly uint value;

        public SampleMask(uint value)
        {
            this.value = value;
        }

        public bool Equals(SampleMask other)
        {
            return this.value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is SampleMask && Equals((SampleMask)obj);
        }

        public override int GetHashCode()
        {
            return (int)this.value;
        }

        public static bool operator ==(SampleMask left, SampleMask right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(SampleMask left, SampleMask right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator uint(SampleMask SampleMask)
        {
            return SampleMask.value;
        }

        public static implicit operator SampleMask(uint value)
        {
            return new SampleMask(value);
        }
    }
}
