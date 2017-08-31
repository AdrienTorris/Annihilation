using System.Runtime.CompilerServices;

using SDL2;

namespace Engine
{
    public static class Mouse
    {
        public static bool RelativeMode
        {
            get => SDL.GetRelativeMouseMode();
            set => SDL.SetRelativeMouseMode(value).CheckError("Could not set relative mode");
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowHandle GetFocusedWindow() => SDL.GetMouseFocus();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetState() => SDL.GetMouseState(null, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateX(out int x) => SDL.GetMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateY(out int y) => SDL.GetMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetStateXY(out int x, out int y) => SDL.GetMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopState() => SDL.GetGlobalMouseState(null, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateX(out int x) => SDL.GetGlobalMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateY(out int y) => SDL.GetGlobalMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDesktopStateXY(out int x, out int y) => SDL.GetGlobalMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateX(out int x) => SDL.GetRelativeMouseState(out x, null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateY(out int y) => SDL.GetRelativeMouseState(null, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe MouseButtonState GetDeltaStateXY(out int x, out int y) => SDL.GetRelativeMouseState(out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarpInWindow(int x, int y) => SDL.WarpMouseInWindow(Window.SdlHandle, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void WarpInDesktop(int x, int y) => SDL.WarpMouseGlobal(x, y).CheckError("Could not warp mouse on desktop");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Capture(bool enabled) => SDL.CaptureMouse(enabled).CheckError("Could not set mouse capture");
    }
}