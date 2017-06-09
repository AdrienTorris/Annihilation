using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using TundraEngine.MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Vector2 : IEquatable<Vector2>
    {
        [Key(0)] public readonly float X;
        [Key(1)] public readonly float Y;

        public static readonly Vector2 Zero = new Vector2();
        public static readonly Vector2 Right = new Vector2(1.0f, 0.0f);
        public static readonly Vector2 Up = new Vector2(0.0f, 1.0f);
        public static readonly Vector2 One = new Vector2(1.0f, 1.0f);

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vector2(float value)
        {
            X = Y = value;
        }

        public float this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0: return X;
                    case 1: return Y;
                }
                throw new ArgumentOutOfRangeException("index", "Indices for Vector2 run from 0 to 1, inclusive.");
            }
        }

        public bool IsNormalized()
        {
            return Math.Abs((X * X) + (Y * Y) - 1f) < MathUtility.ZeroTolerance;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float Length()
        {
            return (float)Math.Sqrt((X * X) + (Y * Y));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float LengthSquared()
        {
            return (X * X) + (Y * Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = new Vector2(left.X + right.X, left.Y + right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subtract(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = new Vector2(left.X - right.X, left.Y - right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Multiply(ref Vector2 value, float scale, out Vector2 result)
        {
            result = new Vector2(value.X * scale, value.Y * scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Modulate(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = new Vector2(left.X * right.X, left.Y * right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Divide(ref Vector2 value, float scale, out Vector2 result)
        {
            result = new Vector2(value.X / scale, value.Y / scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Demodulate(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            result = new Vector2(left.X / right.X, left.Y / right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Negate(ref Vector2 value, out Vector2 result)
        {
            result = new Vector2(-value.X, -value.Y);
        }

        public static void Barycentric(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, float amount1, float amount2, out Vector2 result)
        {
            result = new Vector2((value1.X + (amount1 * (value2.X - value1.X))) + (amount2 * (value3.X - value1.X)),
                (value1.Y + (amount1 * (value2.Y - value1.Y))) + (amount2 * (value3.Y - value1.Y)));
        }

        public static void Clamp(ref Vector2 value, ref Vector2 min, ref Vector2 max, out Vector2 result)
        {
            float x = value.X;
            x = (x > max.X) ? max.X : x;
            x = (x < min.X) ? min.X : x;
            float y = value.Y;
            y = (y > max.Y) ? max.Y : y;
            y = (y < min.Y) ? min.Y : y;
            result = new Vector2(x, y);
        }

        public static void Distance(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            result = (float)Math.Sqrt((x * x) + (y * y));
        }

        public static void DistanceSquared(ref Vector2 value1, ref Vector2 value2, out float result)
        {
            float x = value1.X - value2.X;
            float y = value1.Y - value2.Y;
            result = (x * x) + (y * y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Dot(ref Vector2 left, ref Vector2 right, out float result)
        {
            result = (left.X * right.X) + (left.Y * right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Normalize(ref Vector2 value, out Vector2 result)
        {
            float length = value.Length();
            if (length > MathUtility.ZeroTolerance)
            {
                float inv = 1.0f / length;
                result = new Vector2(value.X * inv, value.Y * inv);
            }
            else
            {
                result = new Vector2(value.X, value.Y);
            }
        }

        public static void Lerp(ref Vector2 start, ref Vector2 end, float amount, out Vector2 result)
        {
            float x = start.X + ((end.X - start.X) * amount);
            float y = start.Y + ((end.Y - start.Y) * amount);
            result = new Vector2(x, y);
        }

        public static void SmoothStep(ref Vector2 start, ref Vector2 end, float amount, out Vector2 result)
        {
            amount = (amount > 1.0f) ? 1.0f : ((amount < 0.0f) ? 0.0f : amount);
            amount = (amount * amount) * (3.0f - (2.0f * amount));
            float x = start.X + ((end.X - start.X) * amount);
            float y = start.Y + ((end.Y - start.Y) * amount);
            result = new Vector2(x, y);
        }

        public static void Hermite(ref Vector2 value1, ref Vector2 tangent1, ref Vector2 value2, ref Vector2 tangent2, float amount, out Vector2 result)
        {
            float squared = amount * amount;
            float cubed = amount * squared;
            float part1 = ((2.0f * cubed) - (3.0f * squared)) + 1.0f;
            float part2 = (-2.0f * cubed) + (3.0f * squared);
            float part3 = (cubed - (2.0f * squared)) + amount;
            float part4 = cubed - squared;
            float x = (((value1.X * part1) + (value2.X * part2)) + (tangent1.X * part3)) + (tangent2.X * part4);
            float y = (((value1.Y * part1) + (value2.Y * part2)) + (tangent1.Y * part3)) + (tangent2.Y * part4);
            result = new Vector2(x, y);
        }

        public static void CatmullRom(ref Vector2 value1, ref Vector2 value2, ref Vector2 value3, ref Vector2 value4, float amount, out Vector2 result)
        {
            float squared = amount * amount;
            float cubed = amount * squared;
            float x = 0.5f * ((((2.0f * value2.X) + ((-value1.X + value3.X) * amount)) +
                (((((2.0f * value1.X) - (5.0f * value2.X)) + (4.0f * value3.X)) - value4.X) * squared)) +
                ((((-value1.X + (3.0f * value2.X)) - (3.0f * value3.X)) + value4.X) * cubed));
            float y = 0.5f * ((((2.0f * value2.Y) + ((-value1.Y + value3.Y) * amount)) +
                (((((2.0f * value1.Y) - (5.0f * value2.Y)) + (4.0f * value3.Y)) - value4.Y) * squared)) +
                ((((-value1.Y + (3.0f * value2.Y)) - (3.0f * value3.Y)) + value4.Y) * cubed));
            result = new Vector2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Max(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            float x = (left.X > right.X) ? left.X : right.X;
            float y = (left.Y > right.Y) ? left.Y : right.Y;
            result = new Vector2(x, y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Min(ref Vector2 left, ref Vector2 right, out Vector2 result)
        {
            float x = (left.X < right.X) ? left.X : right.X;
            float y = (left.Y < right.Y) ? left.Y : right.Y;
            result = new Vector2(x, y);
        }

        public static void Reflect(ref Vector2 vector, ref Vector2 normal, out Vector2 result)
        {
            float dot = (vector.X * normal.X) + (vector.Y * normal.Y);
            float x = vector.X - ((2.0f * dot) * normal.X);
            float y = vector.Y - ((2.0f * dot) * normal.Y);
            result = new Vector2(x, y);
        }

        public static void Transform(ref Vector2 vector, ref Quaternion rotation, out Vector2 result)
        {
            float x = rotation.X + rotation.X;
            float y = rotation.Y + rotation.Y;
            float z = rotation.Z + rotation.Z;
            float wz = rotation.W * z;
            float xx = rotation.X * x;
            float xy = rotation.X * y;
            float yy = rotation.Y * y;
            float zz = rotation.Z * z;
            result = new Vector2((vector.X * (1.0f - yy - zz)) + (vector.Y * (xy - wz)), (vector.X * (xy + wz)) + (vector.Y * (1.0f - xx - zz)));
        }

        public static void Transform(Vector2[] source, ref Quaternion rotation, Vector2[] destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            float x = rotation.X + rotation.X;
            float y = rotation.Y + rotation.Y;
            float z = rotation.Z + rotation.Z;
            float wz = rotation.W * z;
            float xx = rotation.X * x;
            float xy = rotation.X * y;
            float yy = rotation.Y * y;
            float zz = rotation.Z * z;
            float num1 = (1.0f - yy - zz);
            float num2 = (xy - wz);
            float num3 = (xy + wz);
            float num4 = (1.0f - xx - zz);
            for (int i = 0; i < source.Length; ++i)
            {
                destination[i] = new Vector2(
                    (source[i].X * num1) + (source[i].Y * num2),
                    (source[i].X * num3) + (source[i].Y * num4));
            }
        }

        public static void Transform(ref Vector2 vector, ref Matrix transform, out Vector4 result)
        {
            result = new Vector4(
                (vector.X * transform.M11) + (vector.Y * transform.M21) + transform.M41,
                (vector.X * transform.M12) + (vector.Y * transform.M22) + transform.M42,
                (vector.X * transform.M13) + (vector.Y * transform.M23) + transform.M43,
                (vector.X * transform.M14) + (vector.Y * transform.M24) + transform.M44);
        }

        public static void Transform(Vector2[] source, ref Matrix transform, Vector4[] destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            for (int i = 0; i < source.Length; ++i)
            {
                Transform(ref source[i], ref transform, out destination[i]);
            }
        }

        public static void TransformCoordinate(ref Vector2 coordinate, ref Matrix transform, out Vector2 result)
        {
            float x = (coordinate.X * transform.M11) + (coordinate.Y * transform.M21) + transform.M41;
            float y = (coordinate.X * transform.M12) + (coordinate.Y * transform.M22) + transform.M42;
            float z = (coordinate.X * transform.M13) + (coordinate.Y * transform.M23) + transform.M43;
            float w = 1f / ((coordinate.X * transform.M14) + (coordinate.Y * transform.M24) + transform.M44);
            result = new Vector2(x * w, y * w);
        }

        public static void TransformCoordinate(Vector2[] source, ref Matrix transform, Vector2[] destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            for (int i = 0; i < source.Length; ++i)
            {
                TransformCoordinate(ref source[i], ref transform, out destination[i]);
            }
        }

        public static void TransformNormal(ref Vector2 normal, ref Matrix transform, out Vector2 result)
        {
            result = new Vector2(
                (normal.X * transform.M11) + (normal.Y * transform.M21),
                (normal.X * transform.M12) + (normal.Y * transform.M22));
        }

        public static void TransformNormal(Vector2[] source, ref Matrix transform, Vector2[] destination)
        {
            if (source == null)
                throw new ArgumentNullException("source");
            if (destination == null)
                throw new ArgumentNullException("destination");
            if (destination.Length < source.Length)
                throw new ArgumentOutOfRangeException("destination", "The destination array must be of same length or larger length than the source array.");
            for (int i = 0; i < source.Length; ++i)
            {
                TransformNormal(ref source[i], ref transform, out destination[i]);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X + right.X, left.Y + right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator +(Vector2 value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X - right.X, left.Y - right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator -(Vector2 value)
        {
            return new Vector2(-value.X, -value.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 left, Vector2 right)
        {
            return new Vector2(left.X * right.X, left.Y * right.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(float scale, Vector2 value)
        {
            return new Vector2(value.X * scale, value.Y * scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator *(Vector2 value, float scale)
        {
            return new Vector2(value.X * scale, value.Y * scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value, float scale)
        {
            return new Vector2(value.X / scale, value.Y / scale);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(float numerator, Vector2 value)
        {
            return new Vector2(numerator / value.X, numerator / value.Y);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Vector2 operator /(Vector2 value, Vector2 by)
        {
            return new Vector2(value.X / by.X, value.Y / by.Y);
        }

        public static bool operator ==(Vector2 left, Vector2 right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Vector2 left, Vector2 right)
        {
            return !left.Equals(right);
        }

        public static explicit operator Vector3(Vector2 value)
        {
            return new Vector3(value, 0.0f);
        }

        public static explicit operator Vector4(Vector2 value)
        {
            return new Vector4(value, 0.0f, 0.0f);
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1}", X, Y);
        }

        public string ToString(string format)
        {
            if (format == null)
                return ToString();
            return string.Format(CultureInfo.CurrentCulture, "X:{0} Y:{1}", X.ToString(format, CultureInfo.CurrentCulture), Y.ToString(format, CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "X:{0} Y:{1}", X, Y);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                ToString(formatProvider);
            return string.Format(formatProvider, "X:{0} Y:{1}", X.ToString(format, formatProvider), Y.ToString(format, formatProvider));
        }

        public override int GetHashCode()
        {
            return X.GetHashCode() + Y.GetHashCode();
        }

        public bool Equals(Vector2 other)
        {
            return Math.Abs(other.X - X) < MathUtility.ZeroTolerance && Math.Abs(other.Y - Y) < MathUtility.ZeroTolerance;
        }

        public override bool Equals(object value)
        {
            if (value == null)
                return false;
            if (value.GetType() != GetType())
                return false;
            return Equals((Vector2)value);
        }
    }
}