using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public enum HitTestResult
    {
        Normal,
        Draggable,
        ResizeTopLeft,
        ResizeTop,
        ResizeTopRight,
        ResizeRight,
        ResizeBottomRight,
        ResizeBottom,
        ResizeBottomLeft,
        ResizeLeft
    }

    [Flags]
    public enum WindowFlags : uint
    {
        Fullscreen = 1 << 0,
        OpenGL = 1 << 1,
        Shown = 1 << 2,
        Hidden = 1 << 3,
        Borderless = 1 << 4,
        Resizable = 1 << 5,
        Minimized = 1 << 6,
        Maximized = 1 << 7,
        InputGrabbed = 1 << 8,
        InputFocus = 1 << 9,
        MouseFocus = 1 << 10,
        FullscreenDeskTop = (Fullscreen | 1 << 12),
        Foreign = 1 << 11,
        AllowHighDPI = 1 << 13,
        MouseCapture = 1 << 14,
        AlwaysOnTop = 1 << 15,
        SkipTaskbar = 1 << 16,
        Utility = 1 << 17,
        Tooltip = 1 << 18,
        PopupMenu = 1 << 19,
        Vulkan = 1 << 20,
    }
    
    public enum WindowShapeMode
    {
        Default,
        BinarizeAlpha,
        ReverseBinarizeAlpha,
        ColorKey
    }

    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct WindowShapeParams
    {
        [FieldOffset(0)] public byte BinarizationCutoff;
        [FieldOffset(0)] public Color ColorKey;
    }

    public struct WindowShape
    {
        public WindowShapeMode Mode;
        public WindowShapeParams Parameters;
    }
    
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate HitTestResult HitTest(Window window, Point* area, void* data);

    public struct Window
    {
        internal IntPtr NativeHandle;

        public static readonly Window Null = new Window();
        
        public const uint PositionUndefinedMask = 0x1FFF0000;
        public const int PositionUndefined = (int)PositionUndefinedMask;
        public const uint PositionCenteredMask = 0x2FFF0000;
        public const int PositionCentered = (int)PositionCenteredMask;

        public bool IsNull => NativeHandle == IntPtr.Zero;

        public unsafe Window(string title, int x, int y, int w, int h, WindowFlags flags)
        {
            this = Native.SDL_CreateWindow(title.ToAddress(), x, y, w, h, flags);
            this.CheckError("Could not create SDL window");
        }
        
        public unsafe Window(void* data)
        {
            this = Native.SDL_CreateWindowFrom(data);
            this.CheckError("Could not create SDL window");
        }
        
        public Window(WindowID id)
        {
            this = Native.SDL_GetWindowFromID(id);
            this.CheckError("Could not get SDL window");
        }

        public void Destroy()
        {
            Native.SDL_DestroyWindow(this);
            NativeHandle = IntPtr.Zero;
        }

        public static uint PositionUndefinedDisplay(uint x) => PositionUndefinedMask | x;

        public static bool IsPositionUndefined(int x) => (x & 0xFFFF0000) == PositionUndefinedMask;

        public static uint PositionCenteredDisplay(uint x) => PositionCenteredMask | x;

        public static bool IsPositionCentered(int x) => (x & 0xFFFF0000) == PositionCenteredMask;

        public int DisplayIndex => Native.SDL_GetWindowDisplayIndex(this).CheckErrorAndReturn("Could not get display index on SDL window");

        public void SetDisplayMode(ref DisplayMode mode) => Native.SDL_SetWindowDisplayMode(this, ref mode).CheckError("Could not set display mode on SDL window");

        public void SetDefaultDisplayMode() => Native.SDL_SetWindowDisplayMode(this, IntPtr.Zero).CheckError("Could not set display mode on SDL window: " + GetError());

        public void GetDisplayMode(out DisplayMode mode) => Native.SDL_GetWindowDisplayMode(this, out mode).CheckError("Could not get display mode on SDL window: " + GetError());

        public uint PixelFormat => Native.SDL_GetWindowPixelFormat(this).CheckErrorAndReturn("Could not get pixel format on SDL window");

        public WindowID ID => Native.SDL_GetWindowID(this).CheckErrorAndReturn("Could not get ID for SDL window");

        public WindowFlags Flags => Native.SDL_GetWindowFlags(this);
        
        public unsafe string Title
        {
            get
            {
                return GetString(Native.SDL_GetWindowTitle(this));
            }
            set
            {
                Native.SDL_SetWindowTitle(this, value.ToAddress());
            }
        }
        
        public void SetIcon(Surface icon) => Native.SDL_SetWindowIcon(this, icon);
        
        public unsafe void* SetData(string name, void* data) => Native.SDL_SetWindowData(this, name.ToAddress(), data);
        
        public unsafe void* GetData(string name) => Native.SDL_GetWindowData(this, name.ToAddress());
        
        public void SetPosition(int x, int y) => Native.SDL_SetWindowPosition(this, x, y);
        
        public void GetPosition(out int x, out int y) => Native.SDL_GetWindowPosition(this, out x, out y);
        
        public void GetPositionX(out int x) => Native.SDL_GetWindowPosition(this, out x, IntPtr.Zero);
        
        public void GetPositionY(out int y) => Native.SDL_GetWindowPosition(this, IntPtr.Zero, out y);

        public void SetSize(int w, int h) => Native.SDL_SetWindowSize(this, w, h);

        public void GetSize(out int w, out int h) => Native.SDL_GetWindowSize(this, out w, out h);

        public void GetWidth(out int w) => Native.SDL_GetWindowSize(this, out w, IntPtr.Zero);

        public void GetHeight(out int h) => Native.SDL_GetWindowSize(this, IntPtr.Zero, out h);

        public void GetBorderSize(out int top, out int left, out int bottom, out int right) => Native.SDL_GetWindowBordersSize(this, out top, out left, out bottom, out right);

        public void SetMinimumSize(int minW, int minH) => Native.SDL_SetWindowMinimumSize(this, minW, minH);

        public void GetMinimumSize(out int w, out int h) => Native.SDL_GetWindowMinimumSize(this, out w, out h);

        public void SetMaximumSize(int maxW, int maxH) => Native.SDL_SetWindowMaximumSize(this, maxW, maxH);

        public void GetMaximumSize(out int w, out int h) => Native.SDL_GetWindowMaximumSize(this, out w, out h);
        
        public void SetBordered(bool bordered) => Native.SDL_SetWindowBordered(this, bordered);

        public void SetResizable(bool resizable) => Native.SDL_SetWindowResizable(this, resizable);

        public void Show() => Native.SDL_ShowWindow(this);

        public void Hide() => Native.SDL_HideWindow(this);

        public void Raise() => Native.SDL_RaiseWindow(this);

        public void Maximize() => Native.SDL_MaximizeWindow(this);

        public void Minimize() => Native.SDL_MinimizeWindow(this);

        public void Restore() => Native.SDL_RestoreWindow(this);

        public bool IsScreenKeyboardShown { get { return Native.SDL_IsScreenKeyboardShown(this); } }

        public void SetFullscreen(WindowFlags flags)
        {
            Native.SDL_SetWindowFullscreen(this, flags).CheckError("Could not set fullscreen mode for SDL window: " + GetError());
        }

        public unsafe Surface* GetSurface()
        {
            Surface* surface = Native.SDL_GetWindowSurface(this);
            Assert.IsTrue(surface != null, "Could not get surface for SDL window: " + GetError());
            return surface;
        }

        public void UpdateSurface()
        {
            Native.SDL_UpdateWindowSurface(this).CheckError("Could not update surface for SDL window: " + GetError());
        }

        public unsafe void UpdateSurfaceRects(Rect[] rects, int numRects)
        {
            fixed (Rect* ptr = &rects[0])
            {
                Native.SDL_UpdateWindowSurfaceRects(this, ptr, numRects).CheckError("Could not update surface rects for SDL window: " + GetError());
            }
        }

        public bool Grab
        {
            get { return Native.SDL_GetWindowGrab(this); }
            set { Native.SDL_SetWindowGrab(this, value); }
        }

        public static Window GetGrabbedWindow()
        {
            return Native.SDL_GetGrabbedWindow();
        }

        public float Brightness
        {
            get
            {
                return Native.SDL_GetWindowBrightness(this);
            }
            set
            {
                Native.SDL_SetWindowBrightness(this, value).CheckError("Could not set brightness for SDL window: " + GetError());
            }
        }

        public float Opacity
        {
            get
            {
                Native.SDL_GetWindowOpacity(this, out float opacity).CheckError("Could not get opacity for SDL window: " + GetError());
                return opacity;
            }
            set
            {
                Native.SDL_SetWindowOpacity(this, value).CheckError("Could not set opacity for SDL window: " + GetError());
            }
        }

        public void SetModalFor(Window parentWindow)
        {
            Native.SDL_SetWindowModalFor(this, parentWindow).CheckError("Could not set SDL window modal: " + GetError());
        }

        public void SetInputFocus()
        {
            Native.SDL_SetWindowInputFocus(this).CheckError("Could not set input focus for SDL window: " + GetError());
        }

        public unsafe void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                Native.SDL_SetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not set gamma ramp for SDL window: " + GetError());
            }
        }

        public unsafe void GetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                Native.SDL_GetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not get gamma ramp for SDL window");
            }
        }

        public unsafe void SetHitTest(HitTest callback, void* callbackData) => Native.SDL_SetWindowHitTest(this, callback, callbackData).CheckError("Could not set hit test callback for SDL window");

        public void GetWMInfo(ref SysWMInfo sysWMInfo) => Native.SDL_GetWindowWMInfo(this, ref sysWMInfo).CheckError("Could not get sys wm info");

        public void WarpMouseInWindow(int x, int y) => Native.SDL_WarpMouseInWindow(this, x, y);

        public Renderer CreateRenderer(int index, RendererFlags flags)
        {
            Renderer renderer = Native.SDL_CreateRenderer(this, index, flags);
            renderer.CheckError("Could not create renderer");
            return renderer;
        }

        public Renderer GetRenderer() => Native.SDL_GetRenderer(this);

        public unsafe void SetShape(Surface shape, WindowShape shapeMode) => Native.SDL_SetWindowShape(this, &shape, &shapeMode).CheckError("Could not set window shape");

        public unsafe void GetShape(out WindowShape shape) => Native.SDL_GetShapedWindowMode(this, out shape).CheckError("Could not get window shape");
    }

    internal static class WindowFlagsExtensions
    {
        public static bool Has(this WindowFlags variable, WindowFlags flag)
        {
            return (variable & flag) != 0;
        }

        public static bool HasNot(this WindowFlags variable, WindowFlags flag)
        {
            return (variable & flag) == 0;
        }
    }
}