using System;
using System.Text;
using Engine;

namespace Vulkan
{
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

                    if (*(namePtr + i) != *(text.ByteArray + i))
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