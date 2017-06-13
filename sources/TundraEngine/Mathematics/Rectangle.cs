using System;
using System.Runtime.InteropServices;
using MessagePack;

namespace TundraEngine.Mathematics
{
    /// <summary>
    /// A rectangle, with the origin at the upper left.
    /// </summary>
    [MessagePackObject]
    [StructLayout (LayoutKind.Sequential, Pack = 4)]
    public struct Rectangle : IEquatable<Rectangle>
    {
        [Key (0)] public readonly int X;
        [Key (1)] public readonly int Y;
        [Key (2)] public readonly int Width;
        [Key (3)] public readonly int Height;

        public static readonly Rectangle Empty;

        public Rectangle (int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }
        
        [DllImport (LibName, EntryPoint = "SDL_RectEmpty", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IsEmptyNative (ref Rectangle rectangle);

        /// <summary>
        /// Returns true if the rectangle has no area.
        /// </summary>
        public bool IsEmpty ()
        {
            Rectangle rectangle = this;
            return IsEmptyNative (ref rectangle);
        }
        
        [DllImport (LibName, EntryPoint = "SDL_HasIntersection", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool HasIntersectionNative (ref Rectangle a, ref Rectangle b);

        /// <summary>
        /// Determine whether two rectangles intersect.
        /// </summary>
        /// <returns> True if there is an intersection, false otherwise. </returns>
        public bool HasIntersection (Rectangle other)
        {
            Rectangle rectangle = this;
            return HasIntersectionNative (ref rectangle, ref other);
        }

        [DllImport (LibName, EntryPoint = "SDL_IntersectRect", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IntersectNative (ref Rectangle a, ref Rectangle b, out Rectangle result);

        /// <summary>
        /// Calculate the intersection of two rectangles.
        /// </summary>
        /// <returns> True if there is an intersection, false otherwise. </returns>
        public bool Intersect (Rectangle other, out Rectangle result)
        {
            Rectangle rectangle = this;
            return IntersectNative (ref this, ref other, out result);
        }

        [DllImport (LibName, EntryPoint = "SDL_UnionRect", CallingConvention = CallingConvention.Cdecl)]
        private static extern void UnionNative (ref Rectangle a, ref Rectangle b, out Rectangle result);

        /// <summary>
        /// Calculate the union of two rectangles.
        /// </summary>
        public void Union (Rectangle other, out Rectangle result)
        {
            Rectangle rectangle = this;
            return UnionNative (ref this, ref other, out result);
        }

        [DllImport (LibName, EntryPoint = "SDL_EnclosePoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool EnclosePointsNative (
            [In ()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 1)]
            Point[] points, 
            int count,
            ref Rectangle clip,
            out Rectangle result);

        [DllImport (LibName, EntryPoint = "SDL_EnclosePoints", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool EnclosePointsNative (
            [In ()] [MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.Struct, SizeParamIndex = 1)]
            Point[] points,
            int count,
            IntPtr clip,
            out Rectangle result);

        /// <summary>
        /// Calculate a minimal rectangle enclosing a set of points.
        /// </summary>
        /// <returns/> True if any points were within the clipping rect.
        public static bool EnclosePoints (Point[] points, int count, out Rectangle result)
        {
            return EnclosePointsNative (points, count, IntPtr.Zero, out result);
        }

        /// <summary>
        /// Calculate a minimal rectangle enclosing a set of points.
        /// </summary>
        /// <returns/> True if any points were within the clipping rect.
        public static bool EnclosePoints (Point[] points, int count, Rectangle clip, out Rectangle result)
        {
            return EnclosePointsNative (points, count, clip, out result);
        }

        [DllImport (LibName, EntryPoint = "SDL_IntersectRectAndLine", CallingConvention = CallingConvention.Cdecl)]
        private static extern bool IntersectRectangleAndLineNative (ref Rectangle rectangle, ref int x1, ref int y1, ref int x2, ref int y2);

        /// <summary>
        /// Calculate the intersection of a rectangle and line segment.
        /// </summary>
        /// <returns> True if there is an intersection, false otherwise. </returns>
        public bool IntersectRectangleAndLine (ref int x1, ref int y1, ref int x2, ref int y2)
        {
            Rectangle rectangle = this;
            return IntersectRectangleAndLineNative (ref rectangle, ref x1, ref y1, ref x2, ref y2);
        }
    }
}