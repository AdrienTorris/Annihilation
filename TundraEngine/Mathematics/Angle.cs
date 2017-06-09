using System;
using System.Globalization;
using TundraEngine.MessagePack;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    public struct Angle : IComparable, IComparable<Angle>, IEquatable<Angle>
    {
        [Key(0)] private float _radians;
        
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
                    _radians = MathUtility.RevolutionsToRadians(angle);
                    break;
                case AngleType.Degree:
                    _radians = MathUtility.DegreesToRadians(angle);
                    break;
                case AngleType.Radian:
                    _radians = angle;
                    break;
                case AngleType.Gradian:
                    _radians = MathUtility.GradiansToRadians(angle);
                    break;
                default:
                    _radians = 0.0f;
                    break;
            }
        }

        public Angle(float arcLength, float radius)
        {
            _radians = arcLength / radius;
        }

        public void Wrap()
        {
            float newangle = (float)Math.IEEERemainder(_radians, MathUtility.TwoPi);
            if (newangle <= -MathUtility.Pi)
                newangle += MathUtility.TwoPi;
            else if (newangle > MathUtility.Pi)
                newangle -= MathUtility.TwoPi;
            _radians = newangle;
        }

        public void WrapPositive()
        {
            float newangle = _radians % MathUtility.TwoPi;
            if (newangle < 0.0)
                newangle += MathUtility.TwoPi;
            _radians = newangle;
        }

        [IgnoreMember]
        public float Revolutions
        {
            get { return MathUtility.RadiansToRevolutions(_radians); }
            set { _radians = MathUtility.RevolutionsToRadians(value); }
        }

        [IgnoreMember]
        public float Degrees
        {
            get { return MathUtility.RadiansToDegrees(_radians); }
            set { _radians = MathUtility.DegreesToRadians(value); }
        }

        [IgnoreMember]
        public float Minutes
        {
            get
            {
                float degrees = MathUtility.RadiansToDegrees(_radians);
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
                float degrees = MathUtility.RadiansToDegrees(_radians);
                float degreesfloor = (float)Math.Floor(degrees);
                degreesfloor += value / 60.0f;
                _radians = MathUtility.DegreesToRadians(degreesfloor);
            }
        }

        [IgnoreMember]
        public float Seconds
        {
            get
            {
                float degrees = MathUtility.RadiansToDegrees(_radians);
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
                float degrees = MathUtility.RadiansToDegrees(_radians);
                float degreesfloor = (float)Math.Floor(degrees);
                float minutes = (degrees - degreesfloor) * 60.0f;
                float minutesfloor = (float)Math.Floor(minutes);
                minutesfloor += value / 60.0f;
                degreesfloor += minutesfloor / 60.0f;
                _radians = MathUtility.DegreesToRadians(degreesfloor);
            }
        }


        public float Radians
        {
            get { return _radians; }
            set { _radians = value; }
        }

        [IgnoreMember]
        public float Milliradians
        {
            get { return _radians / (Milliradian * MathUtility.TwoPi); }
            set { _radians = value * (Milliradian * MathUtility.TwoPi); }
        }

        [IgnoreMember]
        public float Gradians
        {
            get { return MathUtility.RadiansToGradians(_radians); }
            set { _radians = MathUtility.GradiansToRadians(value); }
        }

        [IgnoreMember]
        public bool IsRight
        {
            get { return _radians == MathUtility.PiOverTwo; }
        }

        [IgnoreMember]
        public bool IsStraight
        {
            get { return _radians == MathUtility.Pi; }
        }

        [IgnoreMember]
        public bool IsFullRotation
        {
            get { return _radians == MathUtility.TwoPi; }
        }

        [IgnoreMember]
        public bool IsOblique
        {
            get { return WrapPositive(this)._radians != MathUtility.PiOverTwo; }
        }

        [IgnoreMember]
        public bool IsAcute
        {
            get { return _radians > 0.0 && _radians < MathUtility.PiOverTwo; }
        }

        [IgnoreMember]
        public bool IsObtuse
        {
            get { return _radians > MathUtility.PiOverTwo && _radians < MathUtility.Pi; }
        }

        [IgnoreMember]
        public bool IsReflex
        {
            get { return _radians > MathUtility.Pi && _radians < MathUtility.TwoPi; }
        }

        [IgnoreMember]
        public Angle Complement
        {
            get { return new Angle(MathUtility.PiOverTwo - _radians, AngleType.Radian); }
        }

        [IgnoreMember]
        public Angle Supplement
        {
            get { return new Angle(MathUtility.Pi - _radians, AngleType.Radian); }
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
            if (left._radians < right._radians)
                return left;
            return right;
        }

        public static Angle Max(Angle left, Angle right)
        {
            if (left._radians > right._radians)
                return left;
            return right;
        }

        public static Angle Add(Angle left, Angle right)
        {
            return new Angle(left._radians + right._radians, AngleType.Radian);
        }

        public static Angle Subtract(Angle left, Angle right)
        {
            return new Angle(left._radians - right._radians, AngleType.Radian);
        }

        public static Angle Multiply(Angle left, Angle right)
        {
            return new Angle(left._radians * right._radians, AngleType.Radian);
        }

        public static Angle Divide(Angle left, Angle right)
        {
            return new Angle(left._radians / right._radians, AngleType.Radian);
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
            return left._radians == right._radians;
        }

        public static bool operator !=(Angle left, Angle right)
        {
            return left._radians != right._radians;
        }

        public static bool operator <(Angle left, Angle right)
        {
            return left._radians < right._radians;
        }

        public static bool operator >(Angle left, Angle right)
        {
            return left._radians > right._radians;
        }

        public static bool operator <=(Angle left, Angle right)
        {
            return left._radians <= right._radians;
        }

        public static bool operator >=(Angle left, Angle right)
        {
            return left._radians >= right._radians;
        }

        public static Angle operator +(Angle value)
        {
            return value;
        }

        public static Angle operator -(Angle value)
        {
            return new Angle(-value._radians, AngleType.Radian);
        }

        public static Angle operator +(Angle left, Angle right)
        {
            return new Angle(left._radians + right._radians, AngleType.Radian);
        }

        public static Angle operator -(Angle left, Angle right)
        {
            return new Angle(left._radians - right._radians, AngleType.Radian);
        }

        public static Angle operator *(Angle left, Angle right)
        {
            return new Angle(left._radians * right._radians, AngleType.Radian);
        }

        public static Angle operator /(Angle left, Angle right)
        {
            return new Angle(left._radians / right._radians, AngleType.Radian);
        }

        public int CompareTo(object other)
        {
            if (other == null)
                return 1;
            if (!(other is Angle))
                throw new ArgumentException("Argument must be of type Angle.", "other");
            float radians = ((Angle)other)._radians;
            if (this._radians > radians)
                return 1;
            if (this._radians < radians)
                return -1;
            return 0;
        }

        public int CompareTo(Angle other)
        {
            if (this._radians > other._radians)
                return 1;
            if (this._radians < other._radians)
                return -1;
            return 0;
        }

        public bool Equals(Angle other)
        {
            return this == other;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.CurrentCulture, MathUtility.RadiansToDegrees(_radians).ToString("0.##°"));
        }

        public string ToString(string format)
        {
            if (format == null)
                return ToString();
            return string.Format(CultureInfo.CurrentCulture, "{0}°", MathUtility.RadiansToDegrees(_radians).ToString(format, CultureInfo.CurrentCulture));
        }

        public string ToString(IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, MathUtility.RadiansToDegrees(_radians).ToString("0.##°"));
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (format == null)
                return ToString(formatProvider);
            return string.Format(formatProvider, "{0}°", MathUtility.RadiansToDegrees(_radians).ToString(format, CultureInfo.CurrentCulture));
        }

        public override int GetHashCode()
        {
            return (int)(BitConverter.DoubleToInt64Bits(_radians) % int.MaxValue);
        }

        public override bool Equals(object obj)
        {
            return (obj is Angle) && (this == (Angle)obj);
        }
    }
}
