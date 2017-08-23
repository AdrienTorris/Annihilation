using System;
using System.Collections.Generic;
using System.Text;

namespace CoreVulkan
{
    public class VulkanException : Exception
    {
        public Result Result { get; private set; }

        public VulkanException(Result result)
            : base($"A Vulkan error of type [{result}] occurred.")
        {
            Result = result;
        }
    }

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
        private uint _value;

        public SampleMask(uint value)
        {
            this._value = value;
        }

        public bool Equals(SampleMask other)
        {
            return _value == other._value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            return obj is SampleMask && Equals((SampleMask)obj);
        }

        public override int GetHashCode()
        {
            return (int)_value;
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
            return _value.ToString();
        }

        public static implicit operator uint(SampleMask SampleMask)
        {
            return SampleMask._value;
        }

        public static implicit operator SampleMask(uint value)
        {
            return new SampleMask(value);
        }
    }

    public struct Size
    {
        public ulong Handle;
    }
    
    public struct Version
    {
        private uint _value;

        public static Version One => new Version(1, 0, 0);

        public Version(uint major, uint Minor, uint patch)
        {
            _value = major << 22 | Minor << 12 | patch;
        }

        public uint Major => _value >> 22;

        public uint Minor => (_value >> 12) & 0x3ff;

        public uint Patch => (_value >> 22) & 0xfff;

        public static implicit operator uint(Version version)
        {
            return version._value;
        }
    }
    
    public unsafe struct Text
    {
        public byte* Handle;

        public static Text Null = new Text();

        public Text(byte* handle)
        {
            Handle = handle;
        }

        public Text(IntPtr handle)
        {
            Handle = (byte*)handle;
        }

        public Text(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            fixed (byte* ptr = &bytes[0])
            {
                Handle = ptr;
            }
        }

        public override string ToString()
        {
            byte* counter = Handle;
            while (*counter != 0)
            {
                counter++;
            }
            int count = (int)(counter - Handle);

            return Encoding.UTF8.GetString(Handle, count);
        }

        public static implicit operator string(Text text)
        {
            return text.ToString();
        }

        public static implicit operator Text(string text)
        {
            return new Text(text);
        }
    }

    public unsafe struct ExtensionName
    {
        public fixed byte Name[Vulkan.MaxExtensionNameSize];

        public bool Compare(Text text)
        {
            fixed (byte* namePtr = Name)
            {
                for (int i = 0; i < Vulkan.MaxExtensionNameSize; ++i)
                {
                    if (*(namePtr + i) == 0)
                    {
                        return true;
                    }

                    if (*(namePtr + i) != *(text.Handle + i))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        public bool Compare(string str)
        {
            int strByteCount = Encoding.UTF8.GetMaxByteCount(str.Length);
            byte[] strBytes = new byte[strByteCount];
            Encoding.UTF8.GetBytes(str, 0, str.Length, strBytes, 0);
            fixed (byte* namePtr = Name)
            fixed (byte* strPtr = &strBytes[0])
            {
                for (int i = 0; i < Vulkan.MaxExtensionNameSize; ++i)
                {
                    if (*(namePtr + i) == 0)
                    {
                        return true;
                    }

                    if (*(namePtr + i) != *(strPtr + i))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}