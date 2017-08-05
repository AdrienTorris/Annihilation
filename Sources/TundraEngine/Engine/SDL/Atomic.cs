using System;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct Atomic
    {
        internal IntPtr NativeHandle;

        public bool CAS(int oldValue, int newValue) => Native.SDL_AtomicCAS(this, oldValue, newValue);

        public int Set(int value) => Native.SDL_AtomicSet(this, value);

        public int Get() => Native.SDL_AtomicGet(this);

        public int Add(int value) => Native.SDL_AtomicAdd(this, value);
    }
}