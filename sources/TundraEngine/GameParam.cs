using System;
using System.Runtime.InteropServices;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public enum ParamType : byte
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
    public struct GameParam
    {
        [FieldOffset (0)] public ParamType Type;
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

        public static GameParam NewByte (byte value)
        {
            return new GameParam { Type = ParamType.Byte, Byte = value };
        }

        public static GameParam NewShort (short value)
        {
            return new GameParam { Type = ParamType.Short, Short = value };
        }
    }
}