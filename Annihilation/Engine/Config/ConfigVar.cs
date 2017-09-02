using System.Runtime.InteropServices;

namespace Engine.Config
{
    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct ConfigVar
    {
        [FieldOffset(0)] public ConfigVarType Type;
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
        [FieldOffset(1)] public char* String;

        public override string ToString()
        {
            switch (Type)
            {
                case ConfigVarType.Uint8: return Uint8.ToString();
                case ConfigVarType.Uint16: return Uint16.ToString();
                case ConfigVarType.Uint32: return Uint32.ToString();
                case ConfigVarType.Uint64: return Uint64.ToString();
                case ConfigVarType.Int8: return Int8.ToString();
                case ConfigVarType.Int16: return Int16.ToString();
                case ConfigVarType.Int32: return Int32.ToString();
                case ConfigVarType.Int64: return Int64.ToString();
                case ConfigVarType.Float: return Float.ToString();
                case ConfigVarType.Double: return Double.ToString();
                case ConfigVarType.Bool: return Bool.ToString();
                case ConfigVarType.String: return new string(String);
                default: return "";
            }
        }

        public ConfigVar(byte value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Uint8;
            Uint8 = value;
        }

        public ConfigVar(ushort value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Uint16;
            Uint16 = value;
        }

        public ConfigVar(uint value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Uint32;
            Uint32 = value;
        }

        public ConfigVar(ulong value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Uint64;
            Uint64 = value;
        }

        public ConfigVar(sbyte value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Int8;
            Int8 = value;
        }

        public ConfigVar(short value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Int16;
            Int16 = value;
        }

        public ConfigVar(int value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Int32;
            Int32 = value;
        }

        public ConfigVar(long value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Int64;
            Int64 = value;
        }

        public ConfigVar(float value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Float;
            Float = value;
        }

        public ConfigVar(double value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Double;
            Double = value;
        }

        public ConfigVar(bool value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.Bool;
            Bool = value;
        }

        public ConfigVar(string value)
        {
            this = default(ConfigVar);
            Type = ConfigVarType.String;
            fixed (char* pointer = value)
            {
                String = pointer;
            }
        }
    }
}