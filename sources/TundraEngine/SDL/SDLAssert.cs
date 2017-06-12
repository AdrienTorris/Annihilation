using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    // TODO: Skipping Assert for now, see if useful
    unsafe public static partial class SDL
    {
        public enum AssertState
        {
            /// <summary>
            /// Retry the assert immediately.
            /// </summary>
            Retry,
            /// <summary>
            /// Make the debugger trigger a breakpoint.
            /// </summary>
            Break,
            /// <summary>
            /// Terminate the program.
            /// </summary>
            Abort,
            /// <summary>
            /// Ignore the assert.
            /// </summary>
            Ignore,
            /// <summary>
            /// Ignore the assert from now on.
            /// </summary>
            AlwaysIgnore
        }

        [StructLayout (LayoutKind.Sequential)]
        public struct AssertData
        {
            public readonly int AlwaysIgnore;
            public readonly uint TriggerCount;
            [MarshalAs(UnmanagedType.LPStr)]
            public readonly string Condition;
            [MarshalAs (UnmanagedType.LPStr)]
            public readonly string Filename;
            public readonly int LineNum;
            [MarshalAs (UnmanagedType.LPStr)]
            public readonly string Function;
            public readonly IntPtr* Next;
        }
    }
}