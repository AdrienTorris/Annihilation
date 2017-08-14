using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace SDL2
{
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
            this = SDL.CreateWindow(title, x, y, w, h, flags);
            this.CheckError("Could not create SDL window");
        }
        
        public unsafe Window(void* data)
        {
            this = SDL.CreateWindowFrom(data);
            this.CheckError("Could not create SDL window");
        }
        
        public Window(WindowID id)
        {
            this = SDL.GetWindowFromID(id);
            this.CheckError("Could not get SDL window");
        }

        //
        // Methods
        //
        public void Destroy()
        {
            SDL.DestroyWindow(this);
            NativeHandle = IntPtr.Zero;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetDisplayIndex() => SDL.GetWindowDisplayIndex(this).CheckErrorAndReturn("Could not get display index on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDisplayMode(ref DisplayMode mode) => SDL.SetWindowDisplayMode(this, ref mode).CheckError("Could not set display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetDefaultDisplayMode() => SDL.SetWindowDisplayMode(this, IntPtr.Zero).CheckError("Could not set display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetDisplayMode(out DisplayMode mode) => SDL.GetWindowDisplayMode(this, out mode).CheckError("Could not get display mode on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public uint GetPixelFormat() => SDL.GetWindowPixelFormat(this).CheckErrorAndReturn("Could not get pixel format on SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowID GetID() => SDL.GetWindowID(this).CheckErrorAndReturn("Could not get ID for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public WindowFlags GetFlags() => SDL.GetWindowFlags(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe string GetTitle() => SDL.GetWindowTitle(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetTitle(string title) => SDL.SetWindowTitle(this, title);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetIcon(Surface icon) => SDL.SetWindowIcon(this, icon);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void* SetData(string name, void* data) => SDL.SetWindowData(this, name, data);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void* GetData(string name) => SDL.GetWindowData(this, name);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetPosition(int x, int y) => SDL.SetWindowPosition(this, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPosition(out int x, out int y) => SDL.GetWindowPosition(this, out x, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPositionX(out int x) => SDL.GetWindowPosition(this, out x, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetPositionY(out int y) => SDL.GetWindowPosition(this, IntPtr.Zero, out y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetSize(int w, int h) => SDL.SetWindowSize(this, w, h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetSize(out int w, out int h) => SDL.GetWindowSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetWidth(out int w) => SDL.GetWindowSize(this, out w, IntPtr.Zero);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetHeight(out int h) => SDL.GetWindowSize(this, IntPtr.Zero, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetBorderSize(out int top, out int left, out int bottom, out int right) => SDL.GetWindowBordersSize(this, out top, out left, out bottom, out right);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMinimumSize(int minW, int minH) => SDL.SetWindowMinimumSize(this, minW, minH);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMinimumSize(out int w, out int h) => SDL.GetWindowMinimumSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetMaximumSize(int maxW, int maxH) => SDL.SetWindowMaximumSize(this, maxW, maxH);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetMaximumSize(out int w, out int h) => SDL.GetWindowMaximumSize(this, out w, out h);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBordered(bool bordered) => SDL.SetWindowBordered(this, bordered);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResizable(bool resizable) => SDL.SetWindowResizable(this, resizable);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Show() => SDL.ShowWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Hide() => SDL.HideWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Raise() => SDL.RaiseWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Maximize() => SDL.MaximizeWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Minimize() => SDL.MinimizeWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Restore() => SDL.RestoreWindow(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool IsScreenKeyboardShown() => SDL.IsScreenKeyboardShown(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetFullscreen(WindowFlags flags) => SDL.SetWindowFullscreen(this, flags).CheckError("Could not set fullscreen mode for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe Surface* GetSurface()
        {
            Surface* surface = SDL.GetWindowSurface(this);
            Assert.IsTrue(surface != null, "Could not get surface for SDL window: " + SDL.GetError());
            return surface;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UpdateSurface() => SDL.UpdateWindowSurface(this).CheckError("Could not update surface for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void UpdateSurfaceRects(Rect[] rects, int numRects)
        {
            fixed (Rect* ptr = &rects[0])
            {
                SDL.UpdateWindowSurfaceRects(this, ptr, numRects).CheckError("Could not update surface rects for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool GetGrab() => SDL.GetWindowGrab(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetGrab(bool value) => SDL.SetWindowGrab(this, value);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Window GetGrabbedWindow() => SDL.GetGrabbedWindow();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public float GetBrightness() => SDL.GetWindowBrightness(this);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetBrightness(float value) => SDL.SetWindowBrightness(this, value).CheckError("Could not set brightness for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetOpacity(out float opacity) => SDL.GetWindowOpacity(this, out opacity).CheckError("Could not get opacity for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetOpacity(float value) => SDL.SetWindowOpacity(this, value).CheckError("Could not set opacity for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetModalFor(Window parentWindow) => SDL.SetWindowModalFor(this, parentWindow).CheckError("Could not set SDL window modal");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetInputFocus() => SDL.SetWindowInputFocus(this).CheckError("Could not set input focus for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                SDL.SetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not set gamma ramp for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void GetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                SDL.GetWindowGammaRamp(this, redPtr, greenPtr, bluePtr).CheckError("Could not get gamma ramp for SDL window");
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetHitTest(HitTest callback, void* callbackData) => SDL.SetWindowHitTest(this, callback, callbackData).CheckError("Could not set hit test callback for SDL window");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void GetWMInfo(ref SysWMInfo sysWMInfo) => SDL.GetWindowWMInfo(this, ref sysWMInfo).CheckError("Could not get sys wm info");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void WarpMouseInWindow(int x, int y) => SDL.WarpMouseInWindow(this, x, y);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void SetShape(Surface shape, WindowShape shapeMode) => SDL.SetWindowShape(this, &shape, &shapeMode).CheckError("Could not set window shape");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe void GetShape(out WindowShape shape) => SDL.GetShapedWindowMode(this, out shape).CheckError("Could not get window shape");

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