using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using MessagePack;
using SDL2;

namespace TundraEngine.Mathematics
{
    [MessagePackObject]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Point : IEquatable<Point>
    {
        [Key(0)] public readonly int X;
        [Key(1)] public readonly int Y;

        public static readonly Point Zero;

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        [DllImport (LibName, EntryPoint = "SDL_PointInRect", CallingConvention = CallingConvention.Cdecl)]
        private extern static bool IsInsideRectangleNative (ref Point point, ref Rectangle rectangle);

        [MethodImpl (MethodImplOptions.AggressiveInlining)]
        public bool IsInsideRectangle (Rectangle rectangle)
        {
            Point point = this;
            return IsInsideRectangleNative (ref point, ref rectangle);
        }
        
        public bool Equals(Point other)
        {
            return other.X == X && other.Y == Y;
        }
        
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (obj.GetType() != typeof(Point)) return false;
            return Equals((Point)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point left, Point right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Point left, Point right)
        {
            return !left.Equals(right);
        }

        public override string ToString()
        {
            return string.Format("({0},{1})", X, Y);
        }

        public static explicit operator Point(Vector2 value)
        {
            return new Point((int)value.X, (int)value.Y);
        }

        public static implicit operator Vector2(Point value)
        {
            return new Vector2(value.X, value.Y);
        }
    }
}