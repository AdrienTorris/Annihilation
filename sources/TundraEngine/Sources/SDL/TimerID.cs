using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    /// <summary>
    /// Function prototype for the timer callback function.
    /// <para/>
    ///  The callback function is passed the current timer interval and returns
    ///  the next timer interval.  If the returned value is the same as the one
    ///  passed in, the periodic alarm continues, otherwise a new alarm is
    ///  scheduled.  If the callback returns 0, the periodic alarm is cancelled.
    /// </summary>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate TimerID TimerCallback(uint interval, IntPtr param);

    /// <summary>
    /// Definition of the timer ID type.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TimerID
    {
        public readonly int ID;
    }
}