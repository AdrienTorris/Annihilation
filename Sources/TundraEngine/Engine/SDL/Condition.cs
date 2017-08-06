using System;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public struct Condition
    {
        internal IntPtr NativeHandle;

        public static Condition Create() => Native.SDL_CreateCond();

        public void Destroy() => Native.SDL_DestroyCond(this);

        public void Signal() => Native.SDL_CondSignal(this).CheckError("Could not signal condition");

        public void Broadcast() => Native.SDL_CondBroadcast(this).CheckError("Could not broadcast condition");

        public void Wait(Mutex mutex) => Native.SDL_CondWait(this, mutex).CheckError("Could not wait on condition");

        public int WaitTimeout(Mutex mutex, uint ms) => Native.SDL_CondWaitTimeout(this, mutex, ms).CheckErrorAndReturn("Could not wait on condition");
    }
}