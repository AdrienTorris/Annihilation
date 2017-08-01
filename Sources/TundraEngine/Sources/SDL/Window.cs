using System;
using System.Text;
using System.Runtime.InteropServices;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.SDL
{
    /// <summary> Possible return values from the <see cref="HitTest"/> callback. </summary>
    /// <seealso cref="HitTest"/>
    public enum HitTestResult
    {
        /// <summary> Region is normal. No special properties. </summary>
        Normal,
        /// <summary> Region can drag entire window. </summary>
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
        /// <summary> Fullscreen window </summary>
        Fullscreen = 1 << 0,
        /// <summary> Window usable with OpenGL context </summary>
        OpenGL = 1 << 1,
        /// <summary> Window is visible </summary>
        Shown = 1 << 2,
        /// <summary> Window is not visible </summary>
        Hidden = 1 << 3,
        /// <summary> No window decoration </summary>
        Borderless = 1 << 4,
        /// <summary> Window can be resized </summary>
        Resizable = 1 << 5,
        /// <summary> Window is minimized </summary>
        Minimized = 1 << 6,
        /// <summary> Window is maximized </summary>
        Maximized = 1 << 7,
        /// <summary> Window has grabbed input focus </summary>
        InputGrabbed = 1 << 8,
        /// <summary> Window has input focus </summary>
        InputFocus = 1 << 9,
        /// <summary> Window has mouse focus </summary>
        MouseFocus = 1 << 10,
        FullscreenDeskTop = (Fullscreen | 1 << 12),
        /// <summary> Window not created by SDL </summary>
        Foreign = 1 << 11,
        /// <summary> Window should be created in high-DPI mode if supported </summary>
        AllowHighDPI = 1 << 13,
        /// <summary> Window has mouse captured (unrelated to <see cref="InputGrabbed"/>) </summary>
        MouseCapture = 1 << 14,
        /// <summary> Window should always be above others </summary>
        AlwaysOnTop = 1 << 15,
        /// <summary> Window should not be added to the taskbar </summary>
        SkipTaskbar = 1 << 16,
        /// <summary> Window should be treated as a utility window </summary>
        Utility = 1 << 17,
        /// <summary> Window should be treated as a tooltip </summary>
        Tooltip = 1 << 18,
        /// <summary> Window should be treated as a popup menu </summary>
        PopupMenu = 1 << 19,
        /// <summary> Window usable for Vulkan surface </summary>
        Vulkan = 1 << 20,
    }

    /// <summary> Callback used for hit-testing. </summary>
    /// <seealso cref="SetWindowHitTest(IntPtr, HitTest, IntPtr)"/>
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate HitTestResult HitTest(Window win, Point* area, void* data);

    public struct Window
    {
        internal IntPtr NativeHandle;

        public static readonly Window Null = new Window();

        /// <summary> Used to indicate that you don't care what the window position is. </summary>
        public const uint PositionUndefinedMask = 0x1FFF0000u;
        public const int PositionUndefined = (int)PositionUndefinedMask;

        /// <summary> Used to indicate that the window position should be centered. </summary>
        public const uint PositionCenteredMask = 0x2FFF0000u;
        public const int PositionCentered = (int)PositionCenteredMask;

        public bool IsNull { get { return NativeHandle == IntPtr.Zero; } }

        /// <summary> Create a window with the specified position, dimensions, and flags. </summary>
        /// <param name="title"> The title of the window, in UTF-8 encoding. </param>
        /// <param name="x"> The x position of the window, <see cref="Native.SDL_WindowPositionCentered"/>, or <see cref="Native.SDL_WindowPositionUndefined"/>. </param>
        /// <param name="y"> The y position of the window, <see cref="Native.SDL_WindowPositionCentered"/>, or<see cref="Native.SDL_WindowPositionUndefined"/>. </param>
        /// <param name="width"> The width of the window, in screen coordinates. </param>
        /// <param name="height"> The height of the window, in screen coordinates. </param>
        /// <param name="flags"> The flags for the window </param>
        /// <remarks>
        ///  If the window is created with the WINDOW_ALLOW_HIGHDPI flag, its size
        ///  in pixels may differ from its size in screen coordinates on platforms with
        ///  high-DPI support (e.g. iOS and Mac OS X). Use GetWindowSize"/> to query
        ///  the client area's size in screen coordinates, and GL_GetDrawableSize"/>
        ///  or GetRendererOutputSize"/> to query the drawable size in pixels.
        /// </remarks>
        /// <seealso cref="DestroyWindow"/>
        public unsafe Window(string title, int x, int y, int w, int h, WindowFlags flags)
        {
            this = Native.SDL_CreateWindow(title.ToAddress(), x, y, w, h, flags);
            Assert.IsTrue(NativeHandle != IntPtr.Zero, "Could not create SDL window: " + GetError());
        }

        /// <summary> Create an SDL window from an existing native window. </summary>
        /// <param name="data"> A pointer to driver-dependent window creation data. </param>
        /// <seealso cref="DestroyWindow"/>
        public unsafe Window(void* data)
        {
            this = Native.SDL_CreateWindowFrom(data);
            Assert.IsTrue(NativeHandle != IntPtr.Zero, "Could not create SDL window: " + GetError());
        }

        /// <summary> Get a window from a stored ID, or <see cref="Null"/> if it doesn't exist. </summary>
        public Window(uint id)
        {
            this = Native.SDL_GetWindowFromID(id);
            NativeHandle = IntPtr.Zero;
        }

        public void Destroy()
        {
            Native.SDL_DestroyWindow(this);
            NativeHandle = IntPtr.Zero;
        }

        public static uint PositionUndefinedDisplay(uint x)
        {
            return PositionUndefinedMask | x;
        }

        public static bool IsPositionUndefined(int x)
        {
            return (x & 0xFFFF0000) == PositionUndefinedMask;
        }

        public static uint PositionCenteredDisplay(uint x)
        {
            return PositionCenteredMask | x;
        }

        public static bool IsPositionCentered(int x)
        {
            return (x & 0xFFFF0000) == PositionCenteredMask;
        }

        /// <summary> Get the display index associated with a window. </summary>
        /// <returns> The display index of the display containing the center of the window, or -1 on error. </returns>
        public int DisplayIndex
        {
            get
            {
                int result = Native.SDL_GetWindowDisplayIndex(this);
                Assert.IsTrue(result >= 0, "Could not get display index on SDL window: " + GetError());
                return result;
            }
        }

        /// <summary> Set the display mode used when a fullscreen window is visible. </summary>
        /// <param name="mode"/> The mode to use.
        public void SetDisplayMode(ref DisplayMode mode)
        {
            int result = Native.SDL_SetWindowDisplayMode(this, ref mode);
            Assert.IsTrue(result == 0, "Could not set display mode on SDL window: " + GetError());
        }

        /// <summary> Set the display mode used when a fullscreen window is visible.
        /// <para/>
        ///  By default the window's dimensions and the deskTop format and refresh rate
        ///  are used.
        /// </summary>
        public void SetDefaultDisplayMode()
        {
            int result = Native.SDL_SetWindowDisplayMode(this, IntPtr.Zero);
            Assert.IsTrue(result == 0, "Could not set display mode on SDL window: " + GetError());
        }

        /// <summary> Fill in information about the display mode used when a fullscreen window is visible. </summary>
        public void GetDisplayMode(out DisplayMode mode)
        {
            int result = Native.SDL_GetWindowDisplayMode(this, out mode);
            Assert.IsTrue(result == 0, "Could not get display mode on SDL window: " + GetError());
        }

        /// <summary> Get the pixel format associated with the window. </summary>
        public uint PixelFormat
        {
            get
            {
                uint result = Native.SDL_GetWindowPixelFormat(this);
                Assert.IsTrue(result != PixelFormatUnknown, "Could not get pixel format on SDL window: " + GetError());
                return result;
            }
        }

        /// <summary> Get the numeric ID of a window, for logging purposes. </summary>
        public uint ID
        {
            get
            {
                uint result = Native.SDL_GetWindowID(this);
                Assert.IsTrue(result != 0, "Could not get ID for SDL window: " + GetError());
                return result;
            }
        }

        /// <summary> Get the window flags. </summary>
        public WindowFlags Flags => Native.SDL_GetWindowFlags(this);

        /// <summary> The title of a window. </summary>
        public unsafe string Title
        {
            get
            {
                byte* ptr = Native.SDL_GetWindowTitle(this);
                return GetString(ptr);
            }
            set
            {
                Native.SDL_SetWindowTitle(this, value.ToAddress());
            }
        }

        /// <summary> Set the icon for a window. </summary>
        public void SetIcon(Surface icon) => Native.SDL_SetWindowIcon(this, icon);

        /// <summary> Associate an arbitrary named pointer with a window. </summary>
        /// <param name="name"> The name of the pointer. </param>
        /// <param name="userData"> The associated pointer. </param>
        /// <returns> The previous value associated with 'name' </returns>
        /// <remarks> The name is case-sensitive. </remarks>
        /// <seealso cref="GetData(string)"/>
        public unsafe void* SetData(string name, void* data) => Native.SDL_SetWindowData(this, name.ToAddress(), data);

        /// <summary> Retrieve the data pointer associated with a window. </summary>
        /// <param name="name"> The name of the pointer. </param>
        /// <returns> The value associated with 'name' </returns>
        /// <seealso cref="SetData(string, void*)"/>
        public unsafe void* GetData(string name) => Native.SDL_GetWindowData(this, name.ToAddress());

        /// <summary> Set the position of a window. </summary>
        /// <param name="x"> The x coordinate of the window in screen coordinates, or <see cref=PositionCentered"/> or <see cref="PositionUndefined"/>. </param>
        /// <param name="y"> The y coordinate of the window in screen coordinates, or <see cref="PositionCentered"/> or <see cref="PositionUndefined"/>. </param>
        /// <remarks> The window coordinate origin is the upper Left of the display. </remarks>
        /// <seealso cref="GetPosition(out int, out int)"/>
        public void SetPosition(int x, int y) => Native.SDL_SetWindowPosition(this, x, y);

        /// <summary> Get the position of a window. </summary>
        /// <param name="x"> Variable for storing the x position, in screen coordinates. </param>
        /// <param name="y"> Variable for storing the y position, in screen coordinates. </param>
        /// <seealso cref="SetPosition(int, int)"/>
        public void GetPosition(out int x, out int y) => Native.SDL_GetWindowPosition(this, out x, out y);

        /// <summary> Get the X position of a window. </summary>
        /// <param name="x"> Variable for storing the x position, in screen coordinates. </param>
        /// <seealso cref="SetPosition(int, int)"/>
        public void GetPositionX(out int x) => Native.SDL_GetWindowPosition(this, out x, IntPtr.Zero);

        /// <summary> Get the Y position of a window. </summary>
        /// <param name="y"> Variable for storing the y position, in screen coordinates. </param>
        /// <seealso cref="SetPosition(int, int)"/>
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

        public void SetFullscreen(WindowFlags flags)
        {
            int result = Native.SDL_SetWindowFullscreen(this, flags);
            Assert.IsTrue(result == 0, "Could not set fullscreen mode for SDL window: " + GetError());
        }

        public unsafe Surface* GetSurface()
        {
            Surface* surface = Native.SDL_GetWindowSurface(this);
            Assert.IsTrue(surface != null, "Could not get surface for SDL window: " + GetError());
            return surface;
        }

        public void UpdateSurface()
        {
            int result = Native.SDL_UpdateWindowSurface(this);
            Assert.IsTrue(result == 0, "Could not update surface for SDL window: " + GetError());
        }

        public unsafe void UpdateSurfaceRects(Rect[] rects, int numRects)
        {
            fixed (Rect* ptr = &rects[0])
            {
                int result = Native.SDL_UpdateWindowSurfaceRects(this, ptr, numRects);
                Assert.IsTrue(result == 0, "Could not update surface rects for SDL window: " + GetError());
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
                int result = Native.SDL_SetWindowBrightness(this, value);
                Assert.IsTrue(result == 0, "Could not set brightness for SDL window: " + GetError());
            }
        }

        public float Opacity
        {
            get
            {
                int result = Native.SDL_GetWindowOpacity(this, out float opacity);
                Assert.IsTrue(result == 0, "Could not get opacity for SDL window: " + GetError());
                return opacity;
            }
            set
            {
                int result = Native.SDL_SetWindowOpacity(this, value);
                Assert.IsTrue(result == 0, "Could not set opacity for SDL window: " + GetError());
            }
        }

        public void SetModalFor(Window parentWindow)
        {
            int result = Native.SDL_SetWindowModalFor(this, parentWindow);
            Assert.IsTrue(result == 0, "Could not set SDL window modal: " + GetError());
        }

        public void SetInputFocus()
        {
            int result = Native.SDL_SetWindowInputFocus(this);
            Assert.IsTrue(result == 0, "Could not set input focus for SDL window: " + GetError());
        }

        public unsafe void SetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                int result = Native.SDL_SetWindowGammaRamp(this, redPtr, greenPtr, bluePtr);
                Assert.IsTrue(result == 0, "Could not set gamma ramp for SDL window: " + GetError());
            }
        }

        public unsafe void GetGammaRamp(ushort[] red, ushort[] green, ushort[] blue)
        {
            fixed (ushort* redPtr = &red[0])
            fixed (ushort* greenPtr = &green[0])
            fixed (ushort* bluePtr = &blue[0])
            {
                int result = Native.SDL_GetWindowGammaRamp(this, redPtr, greenPtr, bluePtr);
                Assert.IsTrue(result == 0, "Could not get gamma ramp for SDL window: " + GetError());
            }
        }

        public unsafe void SetHitTest(HitTest callback, void* callbackData)
        {
            int result = Native.SDL_SetWindowHitTest(this, callback, callbackData);
            Assert.IsTrue(result == 0, "Could not set hit test callback for SDL window: " + GetError());
        }

        public void GetWMInfo(ref SysWMInfo sysWMInfo)
        {
            bool result = Native.SDL_GetWindowWMInfo(this, ref sysWMInfo);
            Assert.IsTrue(result, "Could not get sys WM info for SDL window: " + GetError());
        }
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