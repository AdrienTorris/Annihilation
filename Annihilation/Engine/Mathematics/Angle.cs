using System;
using System.Globalization;

namespace Engine.Mathematics
{
    public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>
    {
        public float Radians;
        
        public const float Degree = 0.002777777777777778f;
        public const float Minute = 0.000046296296296296f;
        public const float Second = 0.000000771604938272f;
        public const float Radian = 0.159154943091895336f;
        public const float Milliradian = 0.0001591549431f;
        public const float Gradian = 0.0025f;

        public Angle(float angle, AngleType type)
        {
            switch (type)
            {
                case AngleType.Revolution:
                    Radians = MathUtility.RevolutionsToRadians(angle);
                    break;
                case AngleType.Degree:
                    Radians = MathUtility.DegreesToRadians(angle);
                    break;
                case AngleType.Radian:
                    Radians = angle;
                    break;
                case AngleType.Gradian:
                    Radians = MathUtility.GradiansToRadians(angle);
                    break;
                default:
                    Radians = 0.0f;
                    break;
            }
        }

        public Angle(float arcLength, float radius)
        {
            Radians = arcLength / radius;
        }

        public void GetBytes(out byte[] bytes)
        {
            float[] floats = new float[1] { Radians };
            bytes = new byte[floats.Length * 4];
            Buffer.BlockCopy(floats, 0, bytes, 0, bytes.Length);
        }

        public void Wrap()
        {
            float newangle = (float)Math.IEEERemainder(Radians, MathUtility.TwoPi);
            if (newangle <= -MathUtility.Pi)
                newangle += MathUtility.TwoPi;
            else if (newangle > MathUtility.Pi)
                newangle -= MathUtility.TwoPi;
            Radians = newangle;
        }

        public void WrapPositive()
        {
            float newangle = Radians % MathUtility.TwoPi;
            if (newangle < 0.0)
                newangle += MathUtility.TwoPi;
            Radians = newangle;
        }
        
        public float Revolutions
        {
            get { return MathUtility.RadiansToRevolutions(Radians); }
            set { Radians = MathUtility.RevolutionsToRadians(value); }
        }
        
        public float Degrees
        {
            get { return MathUtility.RadiansToDegrees(Radians); }
            set { Radians = MathUtility.DegreesToRadians(value); }
        }
        
        public float Minutes
        {
            get
            {
                float degrees = MathUtility.RadiansToDegrees(Radians);
                if (degrees < 0)
                {
                    float degreesfloor = (float)Math.Ceiling(degrees);
                    return (degrees - degreesfloor) * 60.0f;
                }
                else
                {
                    float degreesfloor = (float)Math.Floor(degrees);
                    return (degrees - degreesfloor) * 60.0f;
                }
            }
            set
            {
                float degrees = MathUtility.RadiansToDegrees(Radians);
                float degreesfloor = (float)Math.Floor(degrees);
                degreesfloor += value / 60.0f;
                Radians = MathUtility.DegreesToRadians(degreesfloor);
            }
        }
        
        public float Seconds
        {
            get
            {
                float degrees = MathUtility.RadiansToDegrees(Radians);
                if (degrees < 0)
                {
                    float degreesfloor = (float)Math.Ceiling(degrees);
                    float minutes = (degrees - degreesfloor) * 60.0f;
                    float minutesfloor = (float)Math.Ceiling(minutes);
                    return (minutes - minutesfloor) * 60.0f;
                }
                else
                {
                    float degreesfloor = (float)Math.Floor(degrees);
                    float minutes = (degrees - degreesfloor) * 60.0f;
                    float minutesfloor = (float)Math.Floor(minutes);
                    return (minutes - minutesfloor) * 60.0f;
                }
            }
            set
            {
                float degrees = MathUtility.RadiansToDegrees(Radians);
                float degreesfloor = (float)Math.Floor(degrees);
                float minutes = (degrees - degreesfloor) * 60.0f;
                float minutesfloor = (float)Math.Floor(minutes);
                minutesfloor += value / 60.0f;
                degreesfloor += minutesfloor / 60.0f;
                Radians = MathUtility.DegreesToRadians(degreesfloor);
            }
        }
        
        public float Milliradians
        {
            get { return Radians / (Milliradian * MathUtility.TwoPi); }
            set { Radians = value * (Milliradian * MathUtility.TwoPi); }
        }
        
        public float Gradians
        {
            get { return MathUtility.RadiansToGradians(Radians); }
            set { Radians = MathUtility.GradiansToRadians(value); }
        }
        
        public bool IsRight
        {
            get { return Radians == MathUtility.PiOverTwo; }
        }
        
        public bool IsStraight
        {
            get { return Radians == MathUtility.Pi; }
        }
        
        public bool IsFullRotation
        {
            get { return Radians == MathUtility.TwoPi; }
        }
        
        public bool IsOblique
        {
            get { return WrapPositive(this).Radians != MathUtility.PiOverTwo; }
        }
        
        public bool IsAcute
        {
            get { return Radians > 0.0 && Radians < MathUtility.PiOverTwo; }
        }
        
        public bool IsObtuse
        {
            get { return Radians > MathUtility.PiOverTwo && Radians < MathUtility.Pi; }
        }
        
        public bool IsReflex
        {
            get { return Radians > MathUtility.Pi && Radians < MathUtility.TwoPi; }
        }
        
        public Angle Complement
        {
            get { return new Angle(MathUtility.PiOverTwo - Radians, AngleType.Radian); }
        }
        
        public Angle Supplement
        {
            get { return new Angle(MathUtility.Pi - Radians, AngleType.Radian); }
        }

        public static Angle Wrap(Angle value)
        {
            value.Wrap();
            return value;
        }

        public static Angle WrapPositive(Angle value)
        {
            value.WrapPositive();
            return value;
        }

        public static Angle Min(Angle left, Angle right)
        {
            if (left.Radians < right.Radians)
                return left;
            return right;
        }

        public static Angle Max(Angle left, Angle right)
        {
            if (left.Radians > right.Radians)
                return left;
            return right;
        }

        public static Angle Add(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians, AngleType.Radian);
        }

        public static Angle Subtract(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians, AngleType.Radian);
        }

        public static Angle Multiply(Angle left, Angle right)
        {
            return new Angle(left.Radians * right.Radians, AngleType.Radian);
        }

        public static Angle Divide(Angle left, Angle right)
        {
            return new Angle(left.Radians / right.Radians, AngleType.Radian);
        }

        public static Angle ZeroAngle
        {
            get { return new Angle(0.0f, AngleType.Radian); }
        }

        public static Angle RightAngle
        {
            get { return new Angle(MathUtility.PiOverTwo, AngleType.Radian); }
        }

        public static Angle StraightAngle
        {
            get { return new Angle(MathUtility.Pi, AngleType.Radian); }
        }

        public static Angle FullRotationAngle
        {
            get { return new Angle(MathUtility.TwoPi, AngleType.Radian); }
        }

        public static bool operator ==(Angle left, Angle right)
        {
            return left.Radians == right.Radians;
        }

        public static bool operator !=(Angle left, Angle right)
        {
            return left.Radians != right.Radians;
        }

        public static bool operator <(Angle left, Angle right)
        {
            return left.Radians < right.Radians;
        }

        public static bool operator >(Angle left, Angle right)
        {
            return left.Radians > right.Radians;
        }

        public static bool operator <=(Angle left, Angle right)
        {
            return left.Radians <= right.Radians;
        }

        public static bool operator >=(Angle left, Angle right)
        {
            return left.Radians >= right.Radians;
        }

        public static Angle operator +(Angle value)
        {
            return value;
        }

        public static Angle operator -(Angle value)
        {
            return new Angle(-value.Radians, AngleType.Radian);
        }

        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left.Radians + right.Radians, AngleType.Radian);
        }

        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left.Radians - right.Radians, AngleType.Radian);
        }

        public static Angle operator *(Angle left, Angle right)
        {
            return new Angle(left.Radians * right.Radians, AngleType.Radian);
        }

        public static Angle operator /(Angle left, Angle right)
        {
            return new Angle(left.Radians / right.Radians, AngleType.Radian);
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;
            if (!(other is Angle))
                throw new ArgumentException("Argument must be of type Angle.", "other");
            float radians = ((Angle)other).Radians;
            if (this.Radians > radians)
                return 1;
            if (this.Radians < radians)
                return -1;
            return 0;
        }

        public int CompareTo(Angle other)
        {
            if (this.Radians > other.Radians)
                return 1;
            if (this.Radians < other.Radians)
                return -1;
            return 0;
        }

        public bool Equals(Angle other)
        {
            return this == other;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, MathUtility.RadiansToDegrees(Radians).ToString("0.##°"));
        }

        public string ToString(string format)
        {
            if (format == null)
                return ToString();
            return string.Format(CultureInfo.CurrentCulture, "{0}°", MathUtility.RadiansToDegrees(Radians).ToString(format, CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, MathUtility.RadiansToDegrees(Radians).ToString("0.##°"));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return ToString(formatProvider);
            return string.Format(formatProvider, "{0}°", MathUtility.RadiansToDegrees(Radians).ToString(format, CultureInfo.CurrentCulture));
        }

        public override int GetHashCode()
        {
            return (int)(BitConverter.DoubleToInt64Bits(Radians) % int.MaxValue);
        }

        public override bool Equals(object obj)
        {
            return (obj is Angle) && (this == (Angle)obj);
        }
    }
}
