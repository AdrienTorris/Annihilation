using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public struct Size
    {
        public ulong Handle;
    }
    
    //
    // SDL_joystick.h
    //
    public struct JoystickHandle
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
    public struct CursorHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public CursorHandle CheckErrorAndReturn()
        {
            if (Handle == IntPtr.Zero) SDL.LogError(LogCategory.Application, SDL.GetError());
            return this;
        }
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
    public struct WindowHandle
    {
        public IntPtr Handle;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowHandle CheckErrorAndReturn()
        {
            if (Handle == IntPtr.Zero) SDL.LogError(LogCategory.Application, SDL.GetError());
            return this;
        }
    }
    
    public struct WindowID
    {
        public uint Handle;
    }
}