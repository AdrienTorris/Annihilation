using System.Runtime.InteropServices;

namespace Engine.Config
{
    [StructLayout(LayoutKind.Explicit)]
    public struct ConfigValue
    {
        [FieldOffset(0)] public ConfigValueType Type;
        [FieldOffset(1)] public byte Uint8;
        [FieldOffset(1)] public ushort Uint16;
        [FieldOffset(1)] public uint Uint32;
        [FieldOffset(1)] public ulong Uint64;
        [FieldOffset(1)] public sbyte Int8;
        [FieldOffset(1)] public short Int16;
        [FieldOffset(1)] public int Int32;
        [FieldOffset(1)] public long Int64;
        [FieldOffset(1)] public float Float;
        [FieldOffset(1)] public double Double;
        [FieldOffset(1)] public bool Bool;
        [FieldOffset(1)] public string String;

        public ConfigValue(byte value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Uint8;
            Uint8 = value;
        }

        public ConfigValue(ushort value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Uint16;
            Uint16 = value;
        }

        public ConfigValue(uint value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Uint32;
            Uint32 = value;
        }

        public ConfigValue(ulong value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Uint64;
            Uint64 = value;
        }

        public ConfigValue(sbyte value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Int8;
            Int8 = value;
        }

        public ConfigValue(short value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Int16;
            Int16 = value;
        }

        public ConfigValue(int value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Int32;
            Int32 = value;
        }

        public ConfigValue(long value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Int64;
            Int64 = value;
        }

        public ConfigValue(float value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Float;
            Float = value;
        }

        public ConfigValue(double value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Double;
            Double = value;
        }

        public ConfigValue(bool value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.Bool;
            Bool = value;
        }

        public ConfigValue(string value)
        {
            this = default(ConfigValue);
            Type = ConfigValueType.String;
            String = value;
        }
    }
}