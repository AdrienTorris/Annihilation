using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public static unsafe class Extensions
    {
        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this bool value)
        {
            Trace.Assert(value, Utf8.ToString(SDL.GetError()));
        }

        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this int value)
        {
            Trace.Assert(value == 0, Utf8.ToString(SDL.GetError()));
        }

        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this IntPtr value)
        {
            Trace.Assert(value != IntPtr.Zero, Utf8.ToString(SDL.GetError()));
        }

        [Conditional("DEBUG"), MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this SDL.Window value)
        {
            Trace.Assert(value != IntPtr.Zero, Utf8.ToString(SDL.GetError()));
        }
    }
}