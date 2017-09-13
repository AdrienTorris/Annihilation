using System;

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

    public struct Function
    {
        
    }
}