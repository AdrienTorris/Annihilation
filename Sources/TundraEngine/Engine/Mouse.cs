using System.Runtime.CompilerServices;

using Engine.SDL;
using static Engine.SDL.SDL;

namespace Engine
{
    public static class Mouse
    {
        public static bool RelativeMode
        {
            get { return Native.SDL_GetRelativeMouseMode(); }
            set { Native.SDL_SetRelativeMouseMode(value).CheckError("Could not set relative mode"); }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static SDL.Window GetFocusedWindow() => Native.SDL_GetMouseFocus();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetState() => Native.SDL_GetMouseState(null, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateX(out int x) => Native.SDL_GetMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateY(out int y) => Native.SDL_GetMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateXY(out int x, out int y) => Native.SDL_GetMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopState() => Native.SDL_GetGlobalMouseState(null, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateX(out int x) => Native.SDL_GetGlobalMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateY(out int y) => Native.SDL_GetGlobalMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateXY(out int x, out int y) => Native.SDL_GetGlobalMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateX(out int x) => Native.SDL_GetRelativeMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateY(out int y) => Native.SDL_GetRelativeMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateXY(out int x, out int y) => Native.SDL_GetRelativeMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarpInWindow(int x, int y) => Native.SDL_WarpMouseInWindow(Window.SDLWindow, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarpInDesktop(int x, int y) => Native.SDL_WarpMouseGlobal(x, y).CheckError("Could not warp mouse on desktop");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Capture(bool enabled) => Native.SDL_CaptureMouse(enabled).CheckError("Could not set mouse capture");
    }
}