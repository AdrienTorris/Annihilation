using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SDL_RWFromFile(string file, string mode);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_RWclose(IntPtr context);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_RWread(IntPtr context, IntPtr ptr, int size, int maxNum);

        [DllImport(LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static long SDL_RWsize(IntPtr context);
    }
}