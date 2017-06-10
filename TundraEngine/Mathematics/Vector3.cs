using System;
using System.Runtime.InteropServices;
using MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector3
    {
        [Key(0)] public readonly float X;
        [Key(1)] public readonly float Y;
        [Key(2)] public readonly float Z;

        public static readonly Vector3 Zero = new Vector3();
        public static readonly Vector3 Right = new Vector3(1.0f, 0.0f, 0.0f);
        public static readonly Vector3 Up = new Vector3(0.0f, 1.0f, 0.0f);
        public static readonly Vector3 Forward = new Vector3(0.0f, 0.0f, 1.0f);
        public static readonly Vector3 One = new Vector3(1.0f, 1.0f, 1.0f);

        public Vector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vector3(float value)
        {
            X = Y = Z = value;
        }

        public Vector3(Vector2 v, float z)
        {
            X = v.X;
            Y = v.Y;
            Z = z;
        }
    }
}