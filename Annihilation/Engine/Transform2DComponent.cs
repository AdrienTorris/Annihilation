using System;
using System.Numerics;
using System.Runtime.InteropServices;

using Engine.Mathematics;

namespace Engine.EntityComponent
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct Transform2DComponent
    {
        public Vector2 Position;
        public Angle Rotation;
        public Vector2 Scale;

        public static readonly StringHash32 Type = "Transform2D";
        public static readonly int Size = sizeof(Vector2) * 2 + sizeof(Angle);

        public void GetBytes(out byte[] bytes)
        {
            float[] floats = new float[5] { Position.X, Position.Y, Rotation.Radians, Scale.X, Scale.Y };
            bytes = new byte[floats.Length * 4];
            Buffer.BlockCopy(floats, 0, bytes, 0, bytes.Length);
        }
    }
}