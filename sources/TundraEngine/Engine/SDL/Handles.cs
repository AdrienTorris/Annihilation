using System;
using System.Runtime.CompilerServices;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct WindowHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowHandle CheckErrorAndReturn()
        {
            Assert.IsTrue(Handle != IntPtr.Zero, Native.SDL_GetError());
            return this;
        }
    }

    public struct CursorHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CursorHandle CheckErrorAndReturn()
        {
            Assert.IsTrue(Handle != IntPtr.Zero, Native.SDL_GetError());
            return this;
        }
    }
}