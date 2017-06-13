using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        /// <summary>
        /// This function returns the number of CPU cores available.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetCPUCount", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetCPUCount ();

        /// <summary>
        /// This function returns the L1 cache line size of the CPU
        /// <para /> This is useful for determining multi-threaded structure padding or SIMD prefetch sizes.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetCPUCacheLineSize", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetCPUCacheLineSize ();

        /// <summary>
        /// This function returns true if the CPU has the RDTSC instruction.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasRDTSC", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasRDTSC ();

        /// <summary>
        /// This function returns true if the CPU has AltiVec features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasAltiVec", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasAltiVec ();

        /// <summary>
        /// This function returns true if the CPU has MMX features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasMMX", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasMMX ();

        /// <summary>
        /// This function returns true if the CPU has 3DNow! features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_Has3DNow", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool Has3DNow ();

        /// <summary>
        /// This function returns true if the CPU has SSE features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasSSE", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasSSE ();

        /// <summary>
        /// This function returns true if the CPU has SSE2 features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasSSE2", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasSSE2 ();

        /// <summary>
        /// This function returns true if the CPU has SSE3 features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasSSE3", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasSSE3 ();

        /// <summary>
        /// This function returns true if the CPU has SSE4.1 features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasSSE41", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasSSE41 ();

        /// <summary>
        /// This function returns true if the CPU has SSE4.2 features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasSSE42", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasSSE42 ();

        /// <summary>
        /// This function returns true if the CPU has AVX features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasAVX", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasAVX ();

        /// <summary>
        /// This function returns true if the CPU has AVX2 features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasAVX2", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasAVX2 ();

        /// <summary>
        /// This function returns true if the CPU has NEON (ARM SIMD) features.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasNEON", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasNEON ();

        /// <summary>
        /// This function returns the amount of RAM configured in the system, in MB.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetSystemRAM", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetSystemRAM ();
    }
}
