using System;
using System.Runtime.InteropServices;
using TundraEngine.MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Matrix
    {
        [Key(0)] public readonly float M11;
        [Key(1)] public readonly float M21;
        [Key(2)] public readonly float M31;
        [Key(3)] public readonly float M41;
        [Key(4)] public readonly float M12;
        [Key(5)] public readonly float M22;
        [Key(6)] public readonly float M32;
        [Key(7)] public readonly float M42;
        [Key(8)] public readonly float M13;
        [Key(9)] public readonly float M23;
        [Key(10)] public readonly float M33;
        [Key(11)] public readonly float M43;
        [Key(12)] public readonly float M14;
        [Key(13)] public readonly float M24;
        [Key(14)] public readonly float M34;
        [Key(15)] public readonly float M44;

        public static readonly Matrix Zero = new Matrix();
        public static readonly Matrix Identity = new Matrix(1.0f, 1.0f, 1.0f, 1.0f);

        public Matrix(float value)
        {
            M11 = M21 = M31 = M41 =
            M12 = M22 = M32 = M42 =
            M13 = M23 = M33 = M43 =
            M14 = M24 = M34 = M44 = value;
        }

        public Matrix(float M11, float M12, float M13, float M14,
                      float M21, float M22, float M23, float M24,
                      float M31, float M32, float M33, float M34,
                      float M41, float M42, float M43, float M44)
        {
            this.M11 = M11; this.M12 = M12; this.M13 = M13; this.M14 = M14;
            this.M21 = M21; this.M22 = M22; this.M23 = M23; this.M24 = M24;
            this.M31 = M31; this.M32 = M32; this.M33 = M33; this.M34 = M34;
            this.M41 = M41; this.M42 = M42; this.M43 = M43; this.M44 = M44;
        }

        public Matrix(float M11, float M22, float M33, float M44)
        {
            this.M11 = M11;
            this.M22 = M22;
            this.M33 = M33;
            this.M44 = M44;
            M12 = M13 = M14 = M21 = M23 = M24 = M31 = M32 = M34 = M41 = M42 = M43 = 0f;
        }
    }
}
