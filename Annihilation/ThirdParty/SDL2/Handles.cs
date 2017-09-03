using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public static unsafe partial class SDL
    {
        public struct Size
        {
            public ulong Handle;
        }

        //
        // SDL_events.h
        //
        public struct SysWMmsg
        {
            public IntPtr Handle;
        }

        //
        // SDL_gamecontroller.h
        //
        public struct GameController
        {
            public IntPtr Handle;
        }

        //
        // SDL_haptic.h
        //
        public struct Haptic
        {
            public IntPtr Handle;
        }

        //
        // SDL_joystick.h
        //
        public struct Joystick
        {
            public IntPtr Handle;
        }

        public struct JoystickID
        {
            public int Handle;
        }

        //
        // SDL_mouse.h
        //
        public struct Cursor
        {
            public IntPtr Handle;

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Cursor CheckErrorAndReturn()
            {
                if (Handle == IntPtr.Zero) LogError(LogCategory.Application, GetError());
                return this;
            }
        }

        //
        // SDL_mutex.h
        //
        public struct Mutex
        {
            public IntPtr Handle;
        }

        public struct Semaphore
        {
            public IntPtr Handle;
        }

        public struct Condition
        {
            public IntPtr Handle;
        }

        //
        // SDL_render.h
        //
        public struct Renderer
        {
            public IntPtr Handle;
        }

        public struct Texture
        {
            public IntPtr Handle;
        }
        
        //
        // SDL_video.h
        //
        public struct Window
        {
            public IntPtr Handle;

            public Window(IntPtr handle)
            {
                Handle = handle;
            }

            /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
            public void CheckError()
            {
                if (Handle == IntPtr.Zero) LogError(LogCategory.Application, GetError());
            }

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Window CheckErrorAndReturn()
            {
                if (Handle == IntPtr.Zero) LogError(LogCategory.Application, GetError());
                return this;
            }*/

            public static implicit operator IntPtr(Window window) => window.Handle;
            public static implicit operator Window(IntPtr handle) => new Window(handle);
        }

        public struct WindowID
        {
            public uint Handle;
        }
    }
}