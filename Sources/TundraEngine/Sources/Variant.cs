using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    public enum VariantType : byte
    {
        Byte,
        Short,
        UShort,
        Int,
        UInt,
        Float,
        Double,
        String,
        Vector2,
        Vector3,
        Vector4,
        Entity,
        Struct
    }

    [StructLayout (LayoutKind.Explicit, Pack = 1)]
    public struct Variant
    {
        [FieldOffset (0)] public VariantType Type;
        [FieldOffset (1)] public byte Byte;
        [FieldOffset (1)] public short Short;
        [FieldOffset (1)] public ushort UShort;
        [FieldOffset (1)] public int Int;
        [FieldOffset (1)] public uint UInt;
        [FieldOffset (1)] public float Float;
        [FieldOffset (1)] public double Double;
        [FieldOffset (1)] public string String;
        [FieldOffset (1)] public Vector2 Vector2;
        [FieldOffset (1)] public Vector3 Vector3;
        [FieldOffset (1)] public Vector4 Vector4;
        [FieldOffset (1)] public Entity Entity;
        [FieldOffset (1)] public IntPtr Struct;

        public static Variant NewByte (byte value)
        {
            return new Variant { Type = VariantType.Byte, Byte = value };
        }

        public static Variant NewShort (short value)
        {
            return new Variant { Type = VariantType.Short, Short = value };
        }
    }
}