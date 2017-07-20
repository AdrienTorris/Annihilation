using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        public enum SDL_MouseButton : byte
        {
            Left = 1,
            Middle = 2,
            Right = 3,
            X1 = 4,
            X2 = 5,
        }

        [Flags]
        public enum SDL_MouseButtonState : uint
        {
            Left = 1 << 0,
            Middle = 1 << 1,
            Right = 1 << 2,
            X1 = 1 << 3,
            X2 = 1 << 4
        }

        public enum SDL_SystemCursor
        {
            Arrow,
            IBeam,
            Wait,
            Crosshair,
            WaitArrow,
            SizeNWSE,
            SizeNESW,
            SizeWE,
            SizeNS,
            SizeAll,
            No,
            Hand,
            Count
        }

        public enum SDL_MouseWheelDirection
        {
            Normal,
            Flipped
        }

        [DllImport(LibName)]
        public extern static IntPtr SDL_GetMouseFocus();

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetMouseState(out int x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetMouseState(out int x, IntPtr y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetMouseState(IntPtr x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetMouseState(IntPtr x, IntPtr y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetGlobalMouseState(out int x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetGlobalMouseState(out int x, IntPtr y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetGlobalMouseState(IntPtr x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetGlobalMouseState(IntPtr x, IntPtr y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetRelativeMouseState(out int x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetRelativeMouseState(out int x, IntPtr y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetRelativeMouseState(IntPtr x, out int y);

        [DllImport(LibName)]
        public extern static SDL_MouseButtonState SDL_GetRelativeMouseState(IntPtr x, IntPtr y);

        [DllImport(LibName)]
        public extern static void SDL_WarpMouseInWindow(IntPtr window, int x, int y);

        [DllImport(LibName)]
        public extern static int SDL_WarpMouseGlobal(int x, int y);

        [DllImport(LibName)]
        public extern static int SDL_SetRelativeMouseMode(bool enabled);

        [DllImport(LibName)]
        public extern static int SDL_CaptureMouse(bool enabled);

        [DllImport(LibName)]
        public extern static bool SDL_GetRelativeMouseMode();

        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateCursor(byte[] data, byte[] mask, int w, int h, int hot_x, int hot_y);

        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateColorCursor(IntPtr surface, int hot_x, int hot_y);

        [DllImport(LibName)]
        public extern static IntPtr SDL_CreateSystemCursor(SDL_SystemCursor id);

        [DllImport(LibName)]
        public extern static void SDL_SetCursor(IntPtr cursor);

        [DllImport(LibName)]
        public extern static IntPtr SDL_GetCursor();

        [DllImport(LibName)]
        public extern static IntPtr SDL_GetDefaultCursor();

        [DllImport(LibName)]
        public extern static void SDL_FreeCursor(IntPtr cursor);

        [DllImport(LibName)]
        public extern static int SDL_ShowCursor(int toggle);
    }
}