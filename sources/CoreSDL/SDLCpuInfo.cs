using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        /// <summary>
        /// This function returns the number of CPU cores available.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_GetCPUCount ();

        /// <summary>
        /// This function returns the L1 cache line size of the CPU
        /// <para /> This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_GetCPUCacheLineSize ();

        /// <summary>
        /// This function returns true if the CPU has the RDTSC instruction.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasRDTSC ();

        /// <summary>
        /// This function returns true if the CPU has AltiVec features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasAltiVec ();

        /// <summary>
        /// This function returns true if the CPU has MMX features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasMMX ();

        /// <summary>
        /// This function returns true if the CPU has 3DNow! features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_Has3DNow ();

        /// <summary>
        /// This function returns true if the CPU has SSE features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasSSE ();

        /// <summary>
        /// This function returns true if the CPU has SSE2 features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasSSE2 ();

        /// <summary>
        /// This function returns true if the CPU has SSE3 features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasSSE3 ();

        /// <summary>
        /// This function returns true if the CPU has SSE4.1 features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasSSE41 ();

        /// <summary>
        /// This function returns true if the CPU has SSE4.2 features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasSSE42 ();

        /// <summary>
        /// This function returns true if the CPU has AVX features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasAVX ();

        /// <summary>
        /// This function returns true if the CPU has AVX2 features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasAVX2 ();

        /// <summary>
        /// This function returns true if the CPU has NEON (ARM SIMD) features.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasNEON ();

        /// <summary>
        /// This function returns the amount of RAM configured in the system, in MB.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int SDL_GetSystemRAM ();
    }
}
