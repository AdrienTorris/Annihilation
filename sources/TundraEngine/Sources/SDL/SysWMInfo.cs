using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    /// <summary>
    /// These are the various supported windowing subsystems.
    /// </summary>
    public enum SysWMType
    {
        Unknown,
        Windows,
        X11,
        DirectFB,
        Cocoa,
        UIKit,
        Wayland,
        Mir,
        WinRT,
        Android,
        Vivante
    }

    /// <summary>
    /// The custom window manager information structure.
    /// <para /> When this structure is returned, it holds information about which low level system it is using, and will be one of <see cref="SysWMType"/>.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SysWMInfo
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct InfoUnion
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct WindowsInfo
            {
                /// <summary>
                /// The window handle
                /// </summary>
                public readonly IntPtr Window;
                /// <summary>
                /// The window device context
                /// </summary>
                public readonly IntPtr Hdc;
                /// <summary>
                /// The instance handle
                /// </summary>
                public readonly IntPtr HInstance;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WinRTInfo
            {
                /// <summary>
                /// The WinRT CoreWindow
                /// </summary>
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct X11Info
            {
                /// <summary>
                /// The X11 display
                /// </summary>
                public readonly IntPtr Display;
                /// <summary>
                /// The X11 window
                /// </summary>
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct DirectFBInfo
            {
                /// <summary>
                /// The direcrfb main interface
                /// </summary>
                public readonly IntPtr DirectFB;
                /// <summary>
                /// The directfb window handle
                /// </summary>
                public readonly IntPtr Window;
                /// <summary>
                /// The directfb client surface
                /// </summary>
                public readonly IntPtr Surface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct CocoaInfo
            {
                /// <summary>
                /// The Cocoa window
                /// </summary>
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct UIKitInfo
            {
                /// <summary>
                /// The UIKit window
                /// </summary>
                public readonly IntPtr Window;
                /// <summary>
                /// The GL view's Framebuffer Object. It must be bound when rendering to the screen using GL.
                /// </summary>
                public readonly uint FrameBuffer;
                /// <summary>
                /// The GL view's color Renderbuffer Object. It must be bound when SDL_GL_SwapWindow is called.
                /// </summary>
                public readonly uint ColorBuffer;
                /// <summary>
                /// The Framebuffer Object which holds the resolve color Renderbuffer, when MSAA is used.
                /// </summary>
                public readonly uint ResolveFrameBuffer;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WaylandInfo
            {
                /// <summary>
                /// Wayland display
                /// </summary>
                public readonly IntPtr Display;
                /// <summary>
                /// Wayland surface
                /// </summary>
                public readonly IntPtr Surface;
                /// <summary>
                /// Wayland shell_surface (window manager handle)
                /// </summary>
                public readonly IntPtr ShellSurface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MirInfo
            {
                /// <summary>
                /// Mir display server connection
                /// </summary>
                public readonly IntPtr Connection;
                /// <summary>
                /// Mir surface
                /// </summary>
                public readonly IntPtr Surface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct AndroidInfo
            {
                public readonly IntPtr Window;
                public readonly IntPtr Surface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct VivanteInfo
            {
                public readonly IntPtr Display;
                public readonly IntPtr Window;
            }

            [FieldOffset(0)] public readonly WindowsInfo Windows;
            [FieldOffset(0)] public readonly WinRTInfo WinRT;
            [FieldOffset(0)] public readonly X11Info X11;
            [FieldOffset(0)] public readonly DirectFBInfo DirectFB;
            [FieldOffset(0)] public readonly CocoaInfo Cocoa;
            [FieldOffset(0)] public readonly UIKitInfo UIKit;
            [FieldOffset(0)] public readonly WaylandInfo Wayland;
            [FieldOffset(0)] public readonly MirInfo Mir;
            [FieldOffset(0)] public readonly AndroidInfo Android;
            [FieldOffset(0)] public readonly VivanteInfo Vivante;
        }

        public Version Version;
        public readonly SysWMType SubSystem;
        public readonly InfoUnion Info;
    }
}