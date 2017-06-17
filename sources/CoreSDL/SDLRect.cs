using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        /// <summary>
        /// The structure that defines a point
        /// </summary>
        /// <seealso cref="SDL_EnclosePoints"/>
        /// <seealso cref="SDL_PointInRect"/>
        [StructLayout (LayoutKind.Sequential, Pack = 4)]
        public struct SDL_Point
        {
            public readonly int X;
            public readonly int Y;
        }

        /// <summary>
        /// A rectangle, with the origin at the upper left.
        /// </summary>
        /// <seealso cref="SDL_RectEmpty"/>
        /// <seealso cref="SDL_RectEquals"/>
        /// <seealso cref="SDL_HasIntersection"/>
        /// <seealso cref="SDL_IntersectRect"/>
        /// <seealso cref="SDL_UnionRect"/>
        /// <seealso cref="SDL_EnclosePoints"/>
        [StructLayout (LayoutKind.Sequential, Pack = 4)]
        public struct SDL_Rect
        {
            public readonly int X;
            public readonly int Y;
            public readonly int Width;
            public readonly int Height;

            /// <summary>
            /// Returns true if point resides inside a rectangle.
            /// </summary>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public extern static bool SDL_PointInRect (ref SDL_Point point, ref SDL_Rect rectangle);

            /// <summary>
            /// Returns true if the rectangle has no area.
            /// </summary>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_RectEmpty (ref SDL_Rect rectangle);

            /// <summary>
            /// Determine whether two rectangles intersect.
            /// </summary>
            /// <returns> True if there is an intersection, false otherwise. </returns>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_HasIntersection (ref SDL_Rect a, ref SDL_Rect b);

            /// <summary>
            /// Calculate the intersection of two rectangles.
            /// </summary>
            /// <returns> True if there is an intersection, false otherwise. </returns>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_IntersectRect (ref SDL_Rect a, ref SDL_Rect b, out SDL_Rect result);

            /// <summary>
            /// Calculate the union of two rectangles.
            /// </summary>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern void SDL_UnionRect (ref SDL_Rect a, ref SDL_Rect b, out SDL_Rect result);

            /// <summary>
            /// Calculate a minimal rectangle enclosing a set of points.
            /// </summary>
            /// <returns/> True if any points were within the clipping rect.
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_EnclosePoints (
                [In (), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
                SDL_Point[] points,
                int count,
                ref SDL_Rect clip,
                out SDL_Rect result);

            /// <summary>
            /// Calculate a minimal rectangle enclosing a set of points.
            /// </summary>
            /// <returns/> True if any points were within the clipping rect.
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_EnclosePoints (
                [In (), MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 1)]
                SDL_Point[] points,
                int count,
                IntPtr clip,
                out SDL_Rect result);

            /// <summary>
            /// Calculate the intersection of a rectangle and line segment.
            /// </summary>
            /// <returns> True if there is an intersection, false otherwise. </returns>
            [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
            public static extern bool SDL_IntersectRectAndLine (ref SDL_Rect rectangle, ref int x1, ref int y1, ref int x2, ref int y2);
        }
    }
}