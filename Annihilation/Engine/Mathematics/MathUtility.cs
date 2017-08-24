using System;
using System.Numerics;

namespace Engine.Mathematics
{
    public static class MathUtility
    {
        public const float ZeroTolerance = 1e-6f;
        public const double ZeroToleranceDouble = double.Epsilon * 8;
        public const float Pi = (float)Math.PI;
        public const float TwoPi = (float)(2 * Math.PI);
        public const float PiOverTwo = (float)(Math.PI / 2);
        public const float PiOverFour = (float)(Math.PI / 4);

        public unsafe static bool NearEqual(float a, float b)
        {
            // Check if the numbers are really close -- needed
            // when comparing numbers near zero.
            if (IsZero(a - b))
                return true;
            // Original from Bruce Dawson: http://randomascii.wordpress.com/2012/02/25/comparing-floating-point-numbers-2012-edition/
            int aInt = *(int*)&a;
            int bInt = *(int*)&b;
            // Different signs means they do not match.
            if ((aInt < 0) != (bInt < 0))
                return false;
            // Find the difference in ULPs.
            int ulp = Math.Abs(aInt - bInt);
            // Choose of maxUlp = 4
            // according to http://code.google.com/p/googletest/source/browse/trunk/include/gtest/Native/gtest-Native.h
            const int maxUlp = 4;
            return (ulp <= maxUlp);
        }

        public static bool IsZero(float a)
        {
            return Math.Abs(a) < ZeroTolerance;
        }

        public static bool IsZero(double a)
        {
            return Math.Abs(a) < ZeroToleranceDouble;
        }

        public static bool IsOne(float a)
        {
            return IsZero(a - 1.0f);
        }

        public static bool WithinEpsilon(float a, float b, float epsilon)
        {
            float num = a - b;
            return ((-epsilon <= num) && (num <= epsilon));
        }

        public static T[] Array<T>(T value, int length)
        {
            var result = new T[length];
            for (var i = 0; i < length; i++)
                result[i] = value;
            return result;
        }

        public static float RevolutionsToDegrees(float revolution)
        {
            return revolution * 360.0f;
        }

        public static float RevolutionsToRadians(float revolution)
        {
            return revolution * TwoPi;
        }

        public static float RevolutionsToGradians(float revolution)
        {
            return revolution * 400.0f;
        }

        public static float DegreesToRevolutions(float degree)
        {
            return degree / 360.0f;
        }

        public static float DegreesToRadians(float degree)
        {
            return degree * (Pi / 180.0f);
        }

        public static float RadiansToRevolutions(float radian)
        {
            return radian / TwoPi;
        }

        public static float RadiansToGradians(float radian)
        {
            return radian * (200.0f / Pi);
        }

        public static float GradiansToRevolutions(float gradian)
        {
            return gradian / 400.0f;
        }

        public static float GradiansToDegrees(float gradian)
        {
            return gradian * (9.0f / 10.0f);
        }

        public static float GradiansToRadians(float gradian)
        {
            return gradian * (Pi / 200.0f);
        }

        public static float RadiansToDegrees(float radian)
        {
            return radian * (180.0f / Pi);
        }

        public static float Clamp(float value, float min, float max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static double Clamp(double value, double min, double max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static int Clamp(int value, int min, int max)
        {
            return value < min ? min : value > max ? max : value;
        }

        public static float InverseLerp(float min, float max, float value)
        {
            if (IsZero(Math.Abs(max - min)))
                return float.NaN;
            return (value - min) / (max - min);
        }

        public static double InverseLerp(double min, double max, double value)
        {
            if (IsZero(Math.Abs(max - min)))
                return double.NaN;
            return (value - min) / (max - min);
        }

        public static double Lerp(double from, double to, double amount)
        {
            return (1 - amount) * from + amount * to;
        }

        public static float Lerp(float from, float to, float amount)
        {
            return (1 - amount) * from + amount * to;
        }

        public static byte Lerp(byte from, byte to, float amount)
        {
            return (byte)Lerp((float)from, (float)to, amount);
        }

        public static float SmoothStep(float amount)
        {
            return (amount <= 0) ? 0
                : (amount >= 1) ? 1
                : amount * amount * (3 - (2 * amount));
        }

        public static float SmootherStep(float amount)
        {
            return (amount <= 0) ? 0
                : (amount >= 1) ? 1
                : amount * amount * amount * (amount * ((amount * 6) - 15) + 10);
        }

        public static bool IsPow2(int x)
        {
            return ((x != 0) && (x & (x - 1)) == 0);
        }

        public static float SRgbToLinear(float sRgbValue)
        {
            if (sRgbValue < 0.04045f) return sRgbValue / 12.92f;
            return (float)Math.Pow((sRgbValue + 0.055) / 1.055, 2.4);
        }

        public static float LinearToSRgb(float linearValue)
        {
            if (linearValue < 0.0031308f) return linearValue * 12.92f;
            return (float)(1.055 * Math.Pow(linearValue, 1 / 2.4) - 0.055);
        }

        public static float Log2(float x)
        {
            return (float)Math.Log(x) / 0.6931471805599453f;
        }

        public static int Log2(int i)
        {
            var r = 0;
            while ((i >>= 1) != 0)
                ++r;
            return r;
        }

        public static int NextPowerOfTwo(int x)
        {
            if (x < 0)
                return 0;
            x--;
            x |= x >> 1;
            x |= x >> 2;
            x |= x >> 4;
            x |= x >> 8;
            x |= x >> 16;
            return x + 1;
        }

        public static float NextPowerOfTwo(float size)
        {
            return (float)Math.Pow(2, Math.Ceiling(Math.Log(size, 2)));
        }

        public static int PreviousPowerOfTwo(int size)
        {
            return 1 << (int)Math.Floor(Math.Log(size, 2));
        }

        public static float PreviousPowerOfTwo(float size)
        {
            return (float)Math.Pow(2, Math.Floor(Math.Log(size, 2)));
        }

        public static float Snap(float value, float gap)
        {
            if (gap == 0)
                return value;
            return (float)Math.Round((value / gap), MidpointRounding.AwayFromZero) * gap;
        }

        public static double Snap(double value, double gap)
        {
            if (gap == 0)
                return value;
            return Math.Round((value / gap), MidpointRounding.AwayFromZero) * gap;
        }

        public static Vector2 Snap(Vector2 value, float gap)
        {
            if (gap == 0)
                return value;
            return new Vector2(
                (float)Math.Round((value.X / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.Y / gap), MidpointRounding.AwayFromZero) * gap);
        }

        public static Vector3 Snap(Vector3 value, float gap)
        {
            if (gap == 0)
                return value;
            return new Vector3(
                (float)Math.Round((value.X / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.Y / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.Z / gap), MidpointRounding.AwayFromZero) * gap);
        }

        public static Vector4 Snap(Vector4 value, float gap)
        {
            if (gap == 0)
                return value;
            return new Vector4(
                (float)Math.Round((value.X / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.Y / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.Z / gap), MidpointRounding.AwayFromZero) * gap,
                (float)Math.Round((value.W / gap), MidpointRounding.AwayFromZero) * gap);
        }
    }
}
