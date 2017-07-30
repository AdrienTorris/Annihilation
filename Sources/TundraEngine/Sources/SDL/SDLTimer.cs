using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        /// <summary>
        /// Definition of the timer ID type.
        /// </summary>
        [StructLayout (LayoutKind.Sequential)]
        public struct SDL_TimerID
        {
            public readonly int ID;
        }

        /// <summary>
        /// Function prototype for the timer callback function.
        /// <para/>
        ///  The callback function is passed the current timer interval and returns
        ///  the next timer interval.  If the returned value is the same as the one
        ///  passed in, the periodic alarm continues, otherwise a new alarm is
        ///  scheduled.  If the callback returns 0, the periodic alarm is cancelled.
        /// </summary>
        public delegate SDL_TimerID SDL_TimerCallback (uint interval, IntPtr param);

        /// <summary>
        /// Get the number of milliseconds since the SDL library initialization.
        /// </summary>
        /// <remarks> This value wraps if the program runs for more than ~49 days. </remarks>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static uint SDL_GetTicks ();
        
        /// <summary>
        /// Compare SDL ticks values, and return true if A has passed B
        /// <para/>
        /// e.g. if you want to wait 100 ms, you could do this:
        /// Uint32 timeout = SDL_GetTicks () + 100;
        /// while (!SDL_TICKS_PASSED (SDL_GetTicks(), timeout)) {
        ///     ... do work until timeout has elapsed
        ///   }
        /// </summary>
        public static bool SDL_TicksPassed (uint a, uint b)
        {
            return ((int)(b - a) <= 0);
        }

        /// <summary>
        /// Get the current value of the high resolution counter
        /// </summary>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static ulong SDL_GetPerformanceCounter ();
        
        /// <summary>
        /// Get the count per second of the high resolution counter
        /// </summary>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static ulong SDL_GetPerformanceFrequency ();

        /// <summary>
        /// Wait a specified number of milliseconds before returning.
        /// </summary>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_Delay (uint ms);
        
        /// <summary>
        /// Add a new timer to the pool of timers already running.
        /// </summary>
        /// <returns> A timer ID, or 0 when an error occurs. </returns>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_TimerID SDL_AddTimer (uint interval,
                                                       SDL_TimerCallback callback,
                                                       IntPtr param);

        /// <summary>
        /// Remove a timer knowing its ID.
        /// </summary>
        /// <remarks> It is not safe to remove a timer multiple times. </remarks>
        /// <returns> A boolean value indicating success or failure. </returns>
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_RemoveTimer (SDL_TimerID id);
    }
}