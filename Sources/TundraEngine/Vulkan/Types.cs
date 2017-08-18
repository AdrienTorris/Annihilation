﻿using System;

namespace Vulkan
{
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
            return boolValue == other.boolValue;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is Bool32 && Equals((Bool32)obj);
        }

        public override int GetHashCode()
        {
            return boolValue;
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

    public struct DeviceSize : IEquatable<DeviceSize>
    {
        private readonly ulong value;

        public DeviceSize(ulong value)
        {
            this.value = value;
        }

        public bool Equals(DeviceSize other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is DeviceSize && Equals((DeviceSize)obj);
        }

        public override int GetHashCode()
        {
            return (int)value;
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

    public struct SampleMask : IEquatable<SampleMask>
    {
        private readonly uint value;

        public SampleMask(uint value)
        {
            this.value = value;
        }

        public bool Equals(SampleMask other)
        {
            return value == other.value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is SampleMask && Equals((SampleMask)obj);
        }

        public override int GetHashCode()
        {
            return (int)value;
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