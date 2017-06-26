using System.Runtime.InteropServices;

using TundraEngine.Mathematics;

namespace TundraEngine.EntityComponent
{
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct Transform2DComponent
    {
        public Vector2 Position;
        public Angle Rotation;
        public Vector2 Scale;

        public static readonly StringId32 Type = "Transform2D";
        public static readonly int Size = sizeof(Vector2) * 2 + sizeof(Angle);

        public void GetBytes(out byte[] bytes)
        {
            Position.GetBytes(out byte[] positionBytes);
            Rotation.GetBytes(out byte[] rotationBytes);
            Scale.GetBytes(out byte[] scaleBytes);

            bytes = new byte[positionBytes.Length + rotationBytes.Length + scaleBytes.Length];
            int from = 0;
            int to = positionBytes.Length;
            for (int i = from; i < to; ++i) { bytes[i] = positionBytes[i]; }
            from = to;
            to += rotationBytes.Length;
            for (int i = from; i < to; ++i) { bytes[i] = rotationBytes[i]; }
            from = to;
            to += scaleBytes.Length;
            for (int i = from; i < to; ++i) { bytes[i] = scaleBytes[i]; }
        }
    }
}