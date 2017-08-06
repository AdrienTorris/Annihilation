using System;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct Semaphore
    {
        internal IntPtr NativeHandle;

        public uint Value => Native.SDL_SemValue(this);

        public Semaphore(uint initialValue)
        {
            this = Native.SDL_CreateSemaphore(initialValue);
        }

        public void Destroy() => Native.SDL_DestroySemaphore(this);

        public void Wait() => Native.SDL_SemWait(this).CheckError("Could not wait on semaphore");

        public int TryWait() => Native.SDL_SemTryWait(this).CheckErrorAndReturn("Could not wait on semaphore");

        public int WaitTimeout(uint ms) => Native.SDL_SemWaitTimeout(this, ms).CheckErrorAndReturn("Could not wait on semaphore");

        public void Post() => Native.SDL_SemPost(this).CheckError("Could not increase semaphore count");
    }
}