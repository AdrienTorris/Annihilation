using System;

using static SDL2.SDL;

namespace SDL2
{
    public struct Mutex
    {
        internal IntPtr NativeHandle;

        public static Mutex Create() => Native.SDL_CreateMutex();

        public void Lock() => Native.SDL_LockMutex(this).CheckError("Could not lock mutex");

        public int TryLock() => Native.SDL_TryLockMutex(this).CheckErrorAndReturn("Could not lock mutex");

        public void Unlock() => Native.SDL_UnlockMutex(this).CheckError("Could not unlock mutex");

        public void Destroy() => Native.SDL_DestroyMutex(this);
    }
}