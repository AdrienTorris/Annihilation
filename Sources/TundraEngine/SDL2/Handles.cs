using System;
using System.Runtime.CompilerServices;

using static SDL2.SDL;

namespace SDL2
{
    public struct WindowHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowHandle CheckErrorAndReturn()
        {
            Assert.IsTrue(Handle != IntPtr.Zero, SDL.GetError());
            return this;
        }
    }

    public struct CursorHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CursorHandle CheckErrorAndReturn()
        {
            Assert.IsTrue(Handle != IntPtr.Zero, SDL.GetError());
            return this;
        }
    }
}