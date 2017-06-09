using System;
using System.Runtime.InteropServices;
using TundraEngine.MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector4
    {
        [Key(0)] public readonly float X;
        [Key(1)] public readonly float Y;
        [Key(2)] public readonly float Z;
        [Key(3)] public readonly float W;

        public static readonly Vector4 Zero = new Vector4();
        public static readonly Vector4 One = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);

        public Vector4(float x, float y, float z, float w)
        {
            X = x;
            Y = y;
            Z = z;
            W = w;
        }

        public Vector4(float value)
        {
            X = Y = Z = W = value;
        }
    }
}