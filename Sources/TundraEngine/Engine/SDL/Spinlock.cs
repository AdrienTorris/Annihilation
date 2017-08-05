using System;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct Spinlock
    {
        internal IntPtr NativeHandle;

        public bool TryLock() => Native.SDL_AtomicTryLock(this);

        public void Lock() => Native.SDL_AtomicLock(this);

        public void Unlock() => Native.SDL_AtomicUnlock(this);
    }
}