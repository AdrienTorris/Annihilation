using System;
using System.Runtime.InteropServices;

namespace Engine.Rendering
{
    [StructLayout(LayoutKind.Sequential, Size = 8)]
    public struct DeviceSize : IEquatable<DeviceSize>
    {
        private readonly ulong value;
        
        public DeviceSize(ulong value)
        {
            this.value = value;
        }
        
        public bool Equals(DeviceSize other)
        {
            return this.value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DeviceSize && Equals((DeviceSize)obj);
        }

        public override int GetHashCode()
        {
            return (int)this.value;
        }
        
        public static bool operator ==(DeviceSize left, DeviceSize right)
        {
            return left.Equals(right);
        }
        
        public static bool operator !=(DeviceSize left, DeviceSize right)
        {
            return !left.Equals(right);
        }
        
        public override string ToString()
        {
            return value.ToString();
        }

        public static implicit operator ulong(DeviceSize deviceSize)
        {
            return deviceSize.value;
        }

        public static implicit operator DeviceSize(ulong value)
        {
            return new DeviceSize(value);
        }
    }
}