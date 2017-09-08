using System.Runtime.InteropServices;

namespace Engine
{
    public enum ValueType : byte
    {
        Unknown,
        Uint8,
        Uint16,
        Uint32,
        Uint64,
        Int8,
        Int16,
        Int32,
        Int64,
        Float,
        Double,
        Bool,
        String
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct Value
    {
        [FieldOffset(0)] public ValueType Type;
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
                case ValueType.Uint8: return Uint8.ToString();
                case ValueType.Uint16: return Uint16.ToString();
                case ValueType.Uint32: return Uint32.ToString();
                case ValueType.Uint64: return Uint64.ToString();
                case ValueType.Int8: return Int8.ToString();
                case ValueType.Int16: return Int16.ToString();
                case ValueType.Int32: return Int32.ToString();
                case ValueType.Int64: return Int64.ToString();
                case ValueType.Float: return Float.ToString();
                case ValueType.Double: return Double.ToString();
                case ValueType.Bool: return Bool.ToString();
                case ValueType.String: return new string(String);
                default: return "";
            }
        }

        public Value(byte value)
        {
            this = default(Value);
            Type = ValueType.Uint8;
            Uint8 = value;
        }

        public Value(ushort value)
        {
            this = default(Value);
            Type = ValueType.Uint16;
            Uint16 = value;
        }

        public Value(uint value)
        {
            this = default(Value);
            Type = ValueType.Uint32;
            Uint32 = value;
        }

        public Value(ulong value)
        {
            this = default(Value);
            Type = ValueType.Uint64;
            Uint64 = value;
        }

        public Value(sbyte value)
        {
            this = default(Value);
            Type = ValueType.Int8;
            Int8 = value;
        }

        public Value(short value)
        {
            this = default(Value);
            Type = ValueType.Int16;
            Int16 = value;
        }

        public Value(int value)
        {
            this = default(Value);
            Type = ValueType.Int32;
            Int32 = value;
        }

        public Value(long value)
        {
            this = default(Value);
            Type = ValueType.Int64;
            Int64 = value;
        }

        public Value(float value)
        {
            this = default(Value);
            Type = ValueType.Float;
            Float = value;
        }

        public Value(double value)
        {
            this = default(Value);
            Type = ValueType.Double;
            Double = value;
        }

        public Value(bool value)
        {
            this = default(Value);
            Type = ValueType.Bool;
            Bool = value;
        }

        public Value(char* value)
        {
            this = default(Value);
            Type = ValueType.String;
            String = value;
        }

        public Value(string value)
        {
            this = default(Value);
            Type = ValueType.String;
            fixed (char* pointer = value)
            {
                String = pointer;
            }
        }
    }
}