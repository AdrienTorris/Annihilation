using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

using static SDL2.SDL;

namespace SDL2
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

    public struct Window : IEquatable<Window>
    {
        internal IntPtr NativeHandle;

        public static readonly Window Null = new Window();
        
        public const uint PositionUndefinedMask = 0x1FFF0000;
        public const int PositionUndefined = (int)PositionUndefinedMask;
        public const uint PositionCenteredMask = 0x2FFF0000;
        public const int PositionCentered = (int)PositionCenteredMask;
        
        //
        // Constructors
        //
        public unsafe Window(string title, int x, int y, int w, int h, WindowFlags flags)
        {
            this = Native.SDL_CreateWindow(title, x, y, w, h, flags);
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

        //
        // Methods
        //
        public void Destroy()
        {
            Native.SDL_DestroyWindow(this);
            NativeHandle = IntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDisplayIndex() => Native.SDL_GetWindowDisplayIndex(this).CheckErrorAndReturn("Could not get display index on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDisplayMode(ref DisplayMode mode) => Native.SDL_SetWindowDisplayMode(this, ref mode).CheckError("Could not set display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDefaultDisplayMode() => Native.SDL_SetWindowDisplayMode(this, IntPtr.Zero).CheckError("Could not set display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetDisplayMode(out DisplayMode mode) => Native.SDL_GetWindowDisplayMode(this, out mode).CheckError("Could not get display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetPixelFormat() => Native.SDL_GetWindowPixelFormat(this).CheckErrorAndReturn("Could not get pixel format on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowID GetID() => Native.SDL_GetWindowID(this).CheckErrorAndReturn("Could not get ID for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowFlags GetFlags() => Native.SDL_GetWindowFlags(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe string GetTitle() => Native.SDL_GetWindowTitle(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetTitle(string title) => Native.SDL_SetWindowTitle(this, title);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIcon(Surface icon) => Native.SDL_SetWindowIcon(this, icon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void* SetData(string name, void* data) => Native.SDL_SetWindowData(this, name, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void* GetData(string name) => Native.SDL_GetWindowData(this, name);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(int x, int y) => Native.SDL_SetWindowPosition(this, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPosition(out int x, out int y) => Native.SDL_GetWindowPosition(this, out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPositionX(out int x) => Native.SDL_GetWindowPosition(this, out x, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPositionY(out int y) => Native.SDL_GetWindowPosition(this, IntPtr.Zero, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetSize(int w, int h) => Native.SDL_SetWindowSize(this, w, h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetSize(out int w, out int h) => Native.SDL_GetWindowSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetWidth(out int w) => Native.SDL_GetWindowSize(this, out w, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetHeight(out int h) => Native.SDL_GetWindowSize(this, IntPtr.Zero, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetBorderSize(out int top, out int left, out int bottom, out int right) => Native.SDL_GetWindowBordersSize(this, out top, out left, out bottom, out right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinimumSize(int minW, int minH) => Native.SDL_SetWindowMinimumSize(this, minW, minH);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMinimumSize(out int w, out int h) => Native.SDL_GetWindowMinimumSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMaximumSize(int maxW, int maxH) => Native.SDL_SetWindowMaximumSize(this, maxW, maxH);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMaximumSize(out int w, out int h) => Native.SDL_GetWindowMaximumSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBordered(bool bordered) => Native.SDL_SetWindowBordered(this, bordered);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResizable(bool resizable) => Native.SDL_SetWindowResizable(this, resizable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Show() => Native.SDL_ShowWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Hide() => Native.SDL_HideWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Raise() => Native.SDL_RaiseWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Maximize() => Native.SDL_MaximizeWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Minimize() => Native.SDL_MinimizeWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Restore() => Native.SDL_RestoreWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScreenKeyboardShown() => Native.SDL_IsScreenKeyboardShown(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFullscreen(WindowFlags flags) => Native.SDL_SetWindowFullscreen(this, flags).CheckError("Could not set fullscreen mode for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Surface* GetSurface()
        {
            Surface* surface = Native.SDL_GetWindowSurface(this);
            Assert.IsTrue(surface != null, "Could not get surface for SDL window: " + Native.SDL_GetError());
            return surface;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateSurface() => Native.SDL_UpdateWindowSurface(this).CheckError("Could not update surface for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void UpdateSurfaceRects(Rect[] rects, int numRects)
        {
            fixed (Rect* ptr = &rects[0])
            {
                Native.SDL_UpdateWindowSurfaceRects(this, ptr, numRects).CheckError("Could not update surface rects for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetGrab() => Native.SDL_GetWindowGrab(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetGrab(bool value) => Native.SDL_SetWindowGrab(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Window GetGrabbedWindow() => Native.SDL_GetGrabbedWindow();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetBrightness() => Native.SDL_GetWindowBrightness(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBrightness(float value) => Native.SDL_SetWindowBrightness(this, value).CheckError("Could not set brightness for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetOpacity(out float opacity) => Native.SDL_GetWindowOpacity(this, out opacity).CheckError("Could not get opacity for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetOpacity(float value) => Native.SDL_SetWindowOpacity(this, value).CheckError("Could not set opacity for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetModalFor(Window parentWindow) => Native.SDL_SetWindowModalFor(this, parentWindow).CheckError("Could not set SDL window modal");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInputFocus() => Native.SDL_SetWindowInputFocus(this).CheckError("Could not set input focus for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                Native.SDL_SetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not set gamma ramp for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void GetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                Native.SDL_GetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not get gamma ramp for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetHitTest(HitTest callback, void* callbackData) => Native.SDL_SetWindowHitTest(this, callback, callbackData).CheckError("Could not set hit test callback for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetWMInfo(ref SysWMInfo sysWMInfo) => Native.SDL_GetWindowWMInfo(this, ref sysWMInfo).CheckError("Could not get sys wm info");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WarpMouseInWindow(int x, int y) => Native.SDL_WarpMouseInWindow(this, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetShape(Surface shape, WindowShape shapeMode) => Native.SDL_SetWindowShape(this, &shape, &shapeMode).CheckError("Could not set window shape");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void GetShape(out WindowShape shape) => Native.SDL_GetShapedWindowMode(this, out shape).CheckError("Could not get window shape");

        //
        // Utilities
        //
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint PositionUndefinedDisplay(uint x) => PositionUndefinedMask | x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositionUndefined(int x) => (x & 0xFFFF0000) == PositionUndefinedMask;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static uint PositionCenteredDisplay(uint x) => PositionCenteredMask | x;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPositionCentered(int x) => (x & 0xFFFF0000) == PositionCenteredMask;

        //
        // IEquatable
        //
        public bool Equals(Window other)
        {
            return NativeHandle == other.NativeHandle;
        }

        public override bool Equals(object obj)
        {
            return obj is Window ? Equals((Window)obj) : false;
        }

        public static bool operator ==(Window a, Window b)
        {
            return a.NativeHandle == b.NativeHandle;
        }

        public static bool operator !=(Window a, Window b)
        {
            return a.NativeHandle != b.NativeHandle;
        }

        public override int GetHashCode()
        {
            return NativeHandle.GetHashCode();
        }
    }
}