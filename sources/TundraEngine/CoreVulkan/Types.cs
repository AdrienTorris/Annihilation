using System;
using System.Text;

namespace Vulkan
{
    public struct Bool32 : IEquatable<Bool32>
    {
        private readonly int _value;

        public Bool32(bool value) => _value = value ? 1 : 0;

        public Bool32(int value) => _value = value;

        public bool Equals(Bool32 other) => _value == other._value;

        public override bool Equals(object obj) => obj is Bool32 && Equals((Bool32)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(Bool32 left, Bool32 right) => left.Equals(right);

        public static bool operator !=(Bool32 left, Bool32 right) => !left.Equals(right);

        public static implicit operator bool(Bool32 value) => value._value != 0;

        public static implicit operator Bool32(bool value) => new Bool32(value);

        public static implicit operator int(Bool32 value) => value._value;

        public static implicit operator Bool32(int value) => new Bool32(value);

        public override string ToString() => ((bool)this).ToString();
    }

    public struct DeviceSize : IEquatable<DeviceSize>
    {
        private readonly ulong _value;

        public DeviceSize(ulong value) => _value = value;

        public bool Equals(DeviceSize other) => _value == other._value;

        public override bool Equals(object obj) => obj is DeviceSize && Equals((DeviceSize)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(DeviceSize left, DeviceSize right) => left.Equals(right);

        public static bool operator !=(DeviceSize left, DeviceSize right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator ulong(DeviceSize deviceSize) => deviceSize._value;

        public static implicit operator DeviceSize(ulong value) => new DeviceSize(value);
    }

    public struct SampleMask : IEquatable<SampleMask>
    {
        private uint _value;

        public SampleMask(uint value) => _value = value;

        public bool Equals(SampleMask other) => _value == other._value;

        public override bool Equals(object obj) => obj is SampleMask && Equals((SampleMask)obj);

        public override int GetHashCode() => _value.GetHashCode();

        public static bool operator ==(SampleMask left, SampleMask right) => left.Equals(right);

        public static bool operator !=(SampleMask left, SampleMask right) => !left.Equals(right);

        public override string ToString() => _value.ToString();

        public static implicit operator uint(SampleMask SampleMask) => SampleMask._value;

        public static implicit operator SampleMask(uint value) => new SampleMask(value);
    }

    public struct Size
    {
        private ulong _value;
    }
    
    public struct Version : IEquatable<Version>, IComparable<Version>
    {
        private uint _value;

        public uint Major => _value >> 22;

        public uint Minor => (_value >> 12) & 0x03FF;

        public uint Patch => _value & 0x0FFF;

        public static Version Zero => new Version(0, 0, 0);
        public static Version One => new Version(1, 0, 0);

        public Version(uint major, uint Minor, uint patch) => _value = major << 22 | Minor << 12 | patch;

        public bool Equals(Version other) => _value == other._value;

        public int CompareTo(Version other) => _value.CompareTo(other._value);

        public static implicit operator uint(Version version) => version._value;

        public override string ToString() => $"{Major}.{Minor}.{Patch}";
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
        public fixed byte Name[Vk.MaxExtensionNameSize];

        public bool Compare(Text text)
        {
            fixed (byte* namePtr = Name)
            {
                for (int i = 0; i < Vk.MaxExtensionNameSize; ++i)
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
                for (int i = 0; i < Vk.MaxExtensionNameSize; ++i)
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