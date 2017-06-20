using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SDL_RWFromFile(string file, string mode);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_RWclose(IntPtr context);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

        [DllImport(LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static long SDL_RWsize(IntPtr context);
    }
}