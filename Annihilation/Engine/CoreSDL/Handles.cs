using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public static partial class SDL
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
        // SDL_pixels.h
        //


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

            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public Window CheckErrorAndReturn()
            {
                if (Handle == IntPtr.Zero) LogError(LogCategory.Application, SDL.GetError());
                return this;
            }
        }

        public struct WindowID
        {
            public uint Handle;
        }
    }
}