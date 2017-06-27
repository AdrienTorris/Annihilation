using System;
using System.Runtime.InteropServices;

namespace TundraEngine.Windowing
{
    public enum WindowManagerType : byte
    {
        None,
        Windows,
        X11,
        Wayland
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct WindowManagerInfo
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct WindowsInfo
        {
            public IntPtr HWindow;
            public IntPtr HInstance;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct X11Info
        {
            public IntPtr Window;
            public IntPtr Connection;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WaylandInfo
        {
            public IntPtr Surface;
            public IntPtr Display;
        }

        [FieldOffset(0)] public WindowManagerType Type;
        [FieldOffset(1)] public WindowsInfo Windows;
        [FieldOffset(1)] public X11Info X11;
        [FieldOffset(1)] public WaylandInfo Wayland;
    }
}