using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        public enum SDL_WindowEventID
        {
            /// <summary>
            /// Never used
            /// </summary>
            None,
            /// <summary>
            /// Window has been shown
            /// </summary>
            Shown,
            /// <summary>
            /// Window has been hidden
            /// </summary>
            Hidden,
            /// <summary>
            /// Window has been exposed and should be redrawn
            /// </summary>
            Exposed,

            /// <summary>
            /// Window has been moved to data1, data2
            /// </summary>
            Moved,

            /// <summary>
            /// Window has been resized to data1 x data2
            /// </summary>
            Resized,
            /// <summary>
            /// The window size has changed, either as a result of an API call or through the system or user changing the window size
            /// </summary>
            SizeChanged,

            /// <summary>
            /// Window has been minimized
            /// </summary>
            Minimized,
            /// <summary>
            /// Window has been maximized
            /// </summary>
            Maximized,
            /// <summary>
            /// Window has been restored to normal size and position
            /// </summary>
            Restored,

            /// <summary>
            /// Window has gained mouse focus
            /// </summary>
            Enter,
            /// <summary>
            /// Window has lost mouse focus
            /// </summary>
            Leave,
            /// <summary>
            /// Window has gained keyboard focus
            /// </summary>
            FocusGained,
            /// <summary>
            /// Window has lost keyboard focus
            /// </summary>
            FocusLost,
            /// <summary>
            /// The window manager requests that the window be closed
            /// </summary>
            Close,
            /// <summary>
            /// Window is being offered a focus (should <see cref="SetWindowInputFocus"/> on itself or a subwindow, or ignore)
            /// </summary>
            TakeFocus,
            /// <summary>
            /// Window had a hit test that wasn't <see cref="HitTestNormal"/>
            /// </summary>
            HitTest
        }


        [Flags]
        public enum SDL_BlendMode
        {
            None = 0,
            Blend = 1 << 0,
            Add = 1 << 1,
            Mod = 1 << 2
        }

        /// <summary>
        /// Get the number of video drivers compiled into SDL
        /// </summary>
        /// <seealso cref="GetVideoDriver"/>
        [DllImport(LibraryName)]
        public extern static int SDL_GetNumVideoDrivers();

        [DllImport(LibraryName)]
        private extern static IntPtr SDL_GetVideoDriver(int index);

        /// <summary>
        /// Get the name of a built in video driver.
        /// </summary>
        /// <remarks> The video drivers are presented in the order in which they are normally checked during initialization. </remarks>
        /// <seealso cref="GetNumVideoDrivers"/>
        public static string GetVideoDriver(int index)
        {
            return GetString(SDL_GetVideoDriver(index));
        }

        /// <summary> Initialize the video subsystem, optionally specifying a video driver. </summary>
        /// <param name="driver_name"/> Initialize a specific driver by name, or <see cref="IntPtr.Zero"/> for the default video driver.
        /// <returns> 0 on success, -1 on error </returns>
        /// <remarks>
        ///  This function initializes the video subsystem; setting up a connection
        ///  to the window manager, etc, and determines the available display modes
        ///  and pixel formats, but does not initialize a window or graphics mode.
        /// </remarks>
        /// <seealso cref="VideoQuit"/>
        [DllImport(LibraryName)]
        public static extern int SDL_VideoInit(IntPtr driver_name);

        /// <summary> 
        /// Shuts down the video subsystem.
        /// <para/>
        /// This function closes all windows, and restores the original video mode.
        /// </summary>
        /// <seealso cref="VideoInit"/>
        [DllImport(LibraryName)]
        public static extern void SDL_VideoQuit();

        /// <summary> Returns the name of the currently initialized video driver. </summary>
        /// <returns> The name of the current video driver or <see cref="IntPtr.Zero"/> if no driver has been initialized. </returns>
        /// <seealso cref="GetNumVideoDrivers"/>
        /// <seealso cref="GetVideoDriver"/>
        [DllImport(LibraryName)]
        public static extern IntPtr SDL_GetCurrentVideoDriver();

        /// <summary> Returns the number of available video displays. </summary>
        /// <seealso cref="GetDisplayBounds"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetNumVideoDisplays();

        /// <summary> Get the name of a display in UTF-8 encodin.g </summary>
        /// <returns> The name of a display, or <see cref="IntPtr.Zero"/> for an invalid display index. </returns>
        /// <seealso cref="GetNumVideoDisplays"/>
        [DllImport(LibraryName)]
        public static extern IntPtr SDL_GetDisplayName(int displayIndex);

        /// <summary> Get the deskTop area represented by a display, with the primary display located at 0,0 </summary>
        /// <returns> 0 on success, or -1 if the index is out of range. </returns>
        /// <seealso cref="GetNumVideoDisplays"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetDisplayBounds(int displayIndex, out SDL_Rect rectangle);

        /// <summary> Get the dots/pixels-per-inch for a display </summary>
        /// <remarks>
        /// Diagonal, horizontal and vertical DPI can all be optionally returned if the parameter is non-<see cref="IntPtr.Zero"/>.
        /// </remarks>
        /// <returns> 0 on success, or -1 if no DPI information is available or the index is out of range. </returns>
        /// <seealso cref="GetNumVideoDisplays"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetDisplayDPI(int displayIndex, out float ddpi, out float hdpi, out float vdpi);

        /// <summary> 
        /// Get the usable deskTop area represented by a display, with the primary display located at 0,0
        /// <para/>
        ///  This is the same area as <see cref="GetDisplayBounds(int, out SDL_Rect)"/> reports, but with portions
        ///  reserved by the system removed. For example, on Mac OS X, this subtracts
        ///  the area occupied by the menu bar and dock.
        /// <para/>
        ///  Setting a window to be fullscreen generally bypasses these unusable areas,
        ///  so these are good guidelines for the maximum space available to a
        ///  non-fullscreen window.
        /// <returns> 0 on success, or -1 if the index is out of range. </returns>
        /// <seealso cref="GetDisplayBounds"/>
        /// <seealso cref="GetNumVideoDisplays"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetDisplayUsableBounds(int displayIndex, out SDL_Rect rectangle);

        /// <summary> Returns the number of available display modes. </summary>
        /// <seealso cref="GetDisplayMode"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetNumDisplayModes(int displayIndex);

        /// <summary> Fill in information about a specific display mode. </summary>
        /// <remarks>
        /// The display modes are sorted in this priority:
        /// <list type="bullet">
        /// <item> <description> bits per pixel -> more colors to fewer colors </description> </item>
        /// <item> <description> width -> largest to smallest </description> </item>
        /// <item> <description> height -> largest to smallest </description> </item>
        /// <item> <description> refresh rate -> highest to lowest </description> </item>
        /// </list>
        /// </remarks>
        /// <seealso cref="GetNumDisplayModes"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetDisplayMode(int displayIndex, int modeIndex, out DisplayMode mode);

        /// <summary> Fill in information about the deskTop display mode. </summary>
        [DllImport(LibraryName)]
        public static extern int SDL_GetDeskTopDisplayMode(int displayIndex, out DisplayMode mode);

        /// <summary> Fill in information about the current display mode. </summary>
        [DllImport(LibraryName)]
        public static extern int SDL_GetCurrentDisplayMode(int displayIndex, out DisplayMode mode);

        /// <summary> Get the closest match to the requested display mode. </summary>
        /// <param name="displayIndex"> The index of display from which mode should be queried. </param>
        /// <param name="mode"> The desired display mode. </param>
        /// <param name="closest"> A pointer to a display mode to be filled in with the closest match of the available display modes. </param>
        /// <returns> The passed in value <paramref name="closest"/>, or <see cref="IntPtr.Zero"/> if no matching video mode was available. </returns>
        /// <remarks>
        ///  The available display modes are scanned, and \c closest is filled in with the
        ///  closest mode matching the requested mode and returned.  The mode format and
        ///  refresh_rate default to the deskTop mode if they are 0.  The modes are
        ///  scanned with size being first priority, format being second priority, and
        ///  finally checking the refresh_rate.  If all the available modes are too
        ///  small, then <see cref="IntPtr.Zero"/> is returned.
        /// </remarks>
        /// <seealso cref="GetNumDisplayModes"/>
        /// <seealso cref="GetDisplayMode"/>
        [DllImport(LibraryName)]
        [return: MarshalAs(UnmanagedType.LPStruct)]
        public static extern DisplayMode SDL_GetClosestDisplayMode(int displayIndex, ref DisplayMode mode, out DisplayMode closest);

        [DllImport(LibraryName)]
        public static extern int SDL_GetWindowDisplayIndex(Window window);

        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowDisplayMode(Window window, ref DisplayMode mode);

        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowDisplayMode(Window window, IntPtr mode);

        [DllImport(LibraryName)]
        public static extern int SDL_GetWindowDisplayMode(Window window, out DisplayMode mode);

        [DllImport(LibraryName)]
        public static extern uint SDL_GetWindowPixelFormat(Window window);

        [DllImport(LibraryName)]
        public static unsafe extern Window SDL_CreateWindow(byte* title, int x, int y, int width, int height, WindowFlags flags);

        [DllImport(LibraryName)]
        public static unsafe extern Window SDL_CreateWindowFrom(void* data);

        [DllImport(LibraryName)]
        public static extern uint SDL_GetWindowID(Window window);

        [DllImport(LibraryName)]
        public static extern Window SDL_GetWindowFromID(uint id);

        [DllImport(LibraryName)]
        public static extern WindowFlags SDL_GetWindowFlags(Window window);

        [DllImport(LibraryName)]
        public static unsafe extern void SDL_SetWindowTitle(Window window, byte* title);

        [DllImport(LibraryName)]
        public static unsafe extern byte* SDL_GetWindowTitle(Window window);
        
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowIcon(Window window, Surface icon);

        [DllImport(LibraryName)]
        public static unsafe extern void* SDL_SetWindowData(Window window, byte* name, void* userData);

        [DllImport(LibraryName)]
        public static unsafe extern void* SDL_GetWindowData(Window window, byte* name);

        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowPosition(Window window, int x, int y);

        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowPosition(Window window, out int x, out int y);

        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowPosition(Window window, out int x, IntPtr y);

        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowPosition(Window window, IntPtr x, out int y);

        /// <summary> Set the size of a window's client area. </summary>
        /// <param name="window"> The window to resize. </param>
        /// <param name="width"> The width of the window, in screen coordinates. Must be >0. </param>
        /// <param name="height"> The height of the window, in screen coordinates. Must be >0. </param>
        /// <remarks> 
        /// You can't change the size of a fullscreen window, it automatically matches the size of the display mode.
        /// <para/>
        ///  The window size in screen coordinates may differ from the size in pixels, if
        ///  the window was created with WINDOW_ALLOW_HIGHDPI on a platform with
        ///  high-dpi support (e.g. iOS or OS X). Use GL_GetDrawableSize"/> or
        ///  GetRendererOutputSize"/> to get the real client area size in pixels.
        /// </remarks>
        /// <seealso cref="GetWindowSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowSize(Window window, int width, int height);

        /// <summary> Get the size of a window's client area. </summary>
        /// <param name="window"> The window to query. </param>
        /// <param name="width"> Pointer to variable for storing the width, in screen coordinates. May be <see cref="IntPtr.Zero"/>. </param>
        /// <param name="height"> Pointer to variable for storing the height, in screen coordinates. May be <see cref="IntPtr.Zero"/>. </param>
        /// <remarks>
        ///  The window size in screen coordinates may differ from the size in pixels, if
        ///  the window was created with WINDOW_ALLOW_HIGHDPI on a platform with
        ///  high-dpi support (e.g. iOS or OS X). Use GL_GetDrawableSize"/> or
        ///  GetRendererOutputSize"/> to get the real client area size in pixels.
        /// </remarks>
        /// <seealso cref="SetWindowSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowSize(Window window, out int width, out int height);

        /// <summary> Get the size of a window's borders (decorations) around the client area. </summary>
        /// <param name="window"> The window to query. </param>
        /// <param name="top"> Pointer to variable for storing the size of the Top border. <see cref="IntPtr.Zero"/> is permitted. </param>
        /// <param name="left"> Pointer to variable for storing the size of the Left border. <see cref="IntPtr.Zero"/> is permitted. </param>
        /// <param name="bottom"> Pointer to variable for storing the size of the Bottom border. <see cref="IntPtr.Zero"/> is permitted. </param>
        /// <param name="right"> Pointer to variable for storing the size of the Right border. <see cref="IntPtr.Zero"/> is permitted. </param>
        /// <returns> 0 on success, or -1 if getting this information is not supported. </returns>
        /// <remarks>
        /// If this function fails (returns -1), the size values will be initialized to 0, 0, 0, 0 (if a non-<see cref="IntPtr.Zero"/> pointer is provided), as if the window in question was borderless.
        /// </remarks>
        [DllImport(LibraryName)]
        public static extern int SDL_GetWindowBordersSize(Window window, out int top, out int left, out int bottom, out int right);

        /// <summary> Set the minimum size of a window's client area. </summary>
        /// <param name="window"> The window to set a new minimum size. </param>
        /// <param name="minwidth"> The minimum width of the window, must be >0 </param>
        /// <param name="minHeight"> The minimum height of the window, must be >0 </param>
        /// <remarks> You can't change the minimum size of a fullscreen window, it automatically matches the size of the display mode. </remarks>
        /// <seealso cref="GetWindowMinimumSize"/>
        /// <seealso cref="SetWindowMaximumSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowMinimumSize(Window window, int minwidth, int minHeight);

        /// <summary> Get the minimum size of a window's client area. </summary>
        /// <param name="window"/> The window to query.
        /// <param name="width"/> Pointer to variable for storing the minimum width, may be <see cref="IntPtr.Zero"/>
        /// <param name="height"/> Pointer to variable for storing the minimum height, may be <see cref="IntPtr.Zero"/>
        /// <seealso cref="GetWindowMaximumSize"/>
        /// <seealso cref="SetWindowMinimumSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowMinimumSize(Window window, out int width, out int height);

        /// <summary> Set the maximum size of a window's client area. </summary>
        /// <param name="window"/> The window to set a new maximum size.
        /// <param name="maxWidth"/> The maximum width of the window, must be >0
        /// <param name="maxHeight"/> The maximum height of the window, must be >0
        /// <remarks/> You can't change the maximum size of a fullscreen window, it automatically matches the size of the display mode.
        /// <seealso cref="GetWindowMaximumSize"/>
        /// <seealso cref="SetWindowMinimumSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowMaximumSize(Window window, int maxWidth, int maxHeight);

        /// <summary> Get the maximum size of a window's client area. </summary>
        /// <param name="window"/> The window to query.
        /// <param name="width"/> Pointer to variable for storing the maximum width, may be <see cref="IntPtr.Zero"/>
        /// <param name="height"/> Pointer to variable for storing the maximum height, may be <see cref="IntPtr.Zero"/>
        /// <seealso cref="GetWindowMinimumSize"/>
        /// <seealso cref="SetWindowMaximumSize"/>
        [DllImport(LibraryName)]
        public static extern void SDL_GetWindowMaximumSize(Window window, out int width, out int height);

        /// <summary> 
        /// Set the border state of a window.
        /// <para/>
        ///  This will add or remove the window's WINDOW_BORDERLESS flag and
        ///  add or remove the border from the actual window. This is a no-op if the
        ///  window's border already matches the requested state.
        /// </summary>
        /// <param name="window"/> The window of which to change the border state.
        /// <param name="bordered"/> FALSE to remove border, TRUE to add border.
        /// <remarks/> You can't change the border state of a fullscreen window.
        /// <seealso cref="GetWindowFlags"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowBordered(Window window, bool bordered);

        /// <summary>
        /// Set the user-resizable state of a window.
        /// <para/>
        ///  This will add or remove the window's WINDOW_RESIZABLE flag and
        ///  allow/disallow user resizing of the window. This is a no-op if the
        ///  window's resizable state already matches the requested state.
        /// </summary>
        /// <param name="window"/> The window of which to change the resizable state.
        /// <param name="resizable"/> TRUE to allow resizing, FALSE to disallow.
        /// <remarks> You can't change the resizable state of a fullscreen window. </remarks>
        /// <seealso cref="GetWindowFlags"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowResizable(Window window, bool resizable);

        /// <summary> Show a window. </summary>
        /// <seealso cref="HideWindow"/>
        [DllImport(LibraryName)]
        public static extern void SDL_ShowWindow(Window window);

        /// <summary> Hide a window. </summary>
        /// <seealso cref="ShowWindow"/>
        [DllImport(LibraryName)]
        public static extern void SDL_HideWindow(Window window);

        /// <summary> Raise a window above other windows and set the input focus. </summary>
        [DllImport(LibraryName)]
        public static extern void SDL_RaiseWindow(Window window);

        /// <summary> Make a window as large as possible. </summary>
        /// <seealso cref="RestoreWindow"/>
        [DllImport(LibraryName)]
        public static extern void SDL_MaximizeWindow(Window window);

        /// <summary> Minimize a window to an iconic representation. </summary>
        /// <seealso cref="RestoreWindow"/>
        [DllImport(LibraryName)]
        public static extern void SDL_MinimizeWindow(Window window);

        /// <summary> Restore the size and position of a minimized or maximized window. </summary>
        /// <seealso cref="MaximizeWindow"/>
        /// <seealso cref="MinimizeWindow"/>
        [DllImport(LibraryName)]
        public static extern void SDL_RestoreWindow(Window window);

        /// <summary> Set a window's fullscreen state. </summary>
        /// <returns> 0 on success, or -1 if setting the display mode failed. </returns>
        /// <seealso cref="SetWindowDisplayMode"/>
        /// <seealso cref="GetWindowDisplayMode"/>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowFullscreen(Window window, WindowFlags flags);

        /// <summary> Get the SDL surface associated with the window. </summary>
        /// <returns> The window's framebuffer surface, or <see cref="IntPtr.Zero"/> on error. </returns>
        /// <remarks>
        ///  A new surface will be created with the optimal format for the window,
        ///  if necessary. This surface will be freed when the window is destroyed.
        /// <para/>
        ///  You may not combine this with 3D or the rendering API on this window.
        /// </remarks>
        /// <seealso cref="UpdateWindowSurface"/>
        /// <seealso cref="UpdateWindowSurfaceRects"/>
        [DllImport(LibraryName)]
        public static extern IntPtr SDL_GetWindowSurface(Window window);

        /// <summary> Copy the window surface to the screen. </summary>
        /// <returns> 0 on success, or -1 on error. </returns>
        /// <seealso cref="GetWindowSurface"/>
        /// <seealso cref="UpdateWindowSurfaceRects"/>
        [DllImport(LibraryName)]
        public static extern int SDL_UpdateWindowSurface(Window window);

        /// <summary> Copy a number of rectangles on the window surface to the screen. </summary>
        /// <returns> 0 on success, or -1 on error. </returns>
        /// <seealso cref="GetWindowSurface"/>
        /// <seealso cref="UpdateWindowSurface"/>
        [DllImport(LibraryName)]
        public static extern int SDL_UpdateWindowSurfaceRects(
            Window window,
            [In(), MarshalAs (UnmanagedType.LPArray, SizeParamIndex = 2)]
            SDL_Rect[] rectangles,
            int numRectangles);

        /// <summary> Set a window's input grab mode. </summary>
        /// <param name="window"/> The window for which the input grab mode should be set.
        /// <param name="grabbed"/> This is TRUE to grab input, and FALSE to release input.
        /// <remarks>
        ///  If the caller enables a grab while another window is currently grabbed,
        ///  the other window loses its grab in favor of the caller's window.
        /// </remarks>
        /// <seealso cref="GetWindowGrab"/>
        [DllImport(LibraryName)]
        public static extern void SDL_SetWindowGrab(Window window, bool grabbed);

        /// <summary> Get a window's input grab mode. </summary>
        /// <returns> This returns TRUE if input is grabbed, and FALSE otherwise. </returns>
        /// <seealso cref="SetWindowGrab"/>
        [DllImport(LibraryName)]
        public static extern bool SDL_GetWindowGrab(Window window);

        /// <summary> Get the window that currently has an input grab enabled. </summary>
        /// <returns> This returns the window if input is grabbed, and <see cref="IntPtr.Zero"/> otherwise. </returns>
        /// <seealso cref="SetWindowGrab"/>
        [DllImport(LibraryName)]
        public static extern IntPtr SDL_GetGrabbedWindow();

        /// <summary> Set the bRightness (gamma correction) for a window. </summary>
        /// <returns> 0 on success, or -1 if setting the bRightness isn't supported. </returns>
        /// <seealso cref="GetWindowBRightness"/>
        /// <seealso cref="SetWindowGammaRamp"/>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowBRightness(Window window, float brightness);

        /// <summary> Get the bRightness (gamma correction) for a window. </summary>
        /// <returns> The last bRightness value passed to SetWindowBRightness"/> </returns>
        /// <seealso cref="SetWindowBRightness"/>
        [DllImport(LibraryName)]
        public static extern float SDL_GetWindowBRightness(Window window);

        /// <summary> Set the opacity for a window </summary>
        /// <param name="window"/> The window which will be made transparent or opaque
        /// <param name="opacity"/> Opacity (0.0f - transparent, 1.0f - opaque) This will be clamped Natively between 0.0f and 1.0f.
        /// <returns> 0 on success, or -1 if setting the opacity isn't supported. </returns>
        /// <seealso cref="GetWindowOpacity"/>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowOpacity(Window window, float opacity);

        /// <summary> 
        /// Get the opacity of a window.
        /// <para/>
        ///  If transparency isn't supported on this platform, opacity will be reported
        ///  as 1.0f without error.
        /// </summary>
        /// <param name="window"/> The window in question.
        /// <param name="outOpacity"/> Opacity (0.0f - transparent, 1.0f - opaque)
        /// <returns> 0 on success, or -1 on error (invalid window, etc). </returns>
        /// <seealso cref="SetWindowOpacity"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetWindowOpacity(Window window, out float outOpacity);

        /// <summary> Sets the window as a modal for another window (TODO: reconsider this function and/or its name) </summary>
        /// <param name="modalWindow"> The window that should be modal </param>
        /// <param name="parentWindow"> The parent window </param>
        /// <returns> 0 on success, or -1 otherwise. </returns>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowModalFor(IntPtr modalWindow, IntPtr parentWindow);

        /// <summary> 
        /// Explicitly sets input focus to the window.
        /// <para/>
        ///  You almost certainly want <see cref="RaiseWindow(IntPtr)"/> instead of this function. Use
        ///  this with caution, as you might give focus to a window that's completely
        ///  obscured by other windows.
        /// </summary>
        /// <param name="window"> The window that should get the input focus. </param>
        /// <returns> 0 on success, or -1 otherwise. </returns>
        /// <seealso cref="RaiseWindow"/>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowInputFocus(Window window);


        /// <summary> Set the gamma ramp for a window. </summary>
        /// <param name="window"/> The window for which the gamma ramp should be set.
        /// <param name="red"/> The translation table for the red channel, or <see cref="IntPtr.Zero"/>.
        /// <param name="green"/> The translation table for the green channel, or <see cref="IntPtr.Zero"/>.
        /// <param name="blue"/> The translation table for the blue channel, or <see cref="IntPtr.Zero"/>.
        /// <returns> 0 on success, or -1 if gamma ramps are unsupported. </returns>
        /// <remarks>
        ///  Set the gamma translation table for the red, green, and blue channels
        ///  of the video hardware.  Each table is an array of 256 16-bit quantities,
        ///  representing a mapping between the input and output for that channel.
        ///  The input is the index into the array, and the output is the 16-bit
        ///  gamma value at that index, scaled to the output color precision.
        /// </remarks>
        /// <seealso cref="GetWindowGammaRamp"/>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowGammaRamp(
            Window window,
            [In(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] red,
            [In(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] green,
            [In(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] blue);


        /// <summary> Get the gamma ramp for a window. </summary>
        /// <param name="window"/> The window from which the gamma ramp should be queried.
        /// <param name="red"/> A pointer to a 256 element array of 16-bit quantities to hold the translation table for the red channel, or <see cref="IntPtr.Zero"/>.
        /// <param name="green"/> A pointer to a 256 element array of 16-bit quantities to hold the translation table for the green channel, or <see cref="IntPtr.Zero"/>.
        /// <param name="blue"/> A pointer to a 256 element array of 16-bit quantities to hold the translation table for the blue channel, or <see cref="IntPtr.Zero"/>.
        /// <returns> 0 on success, or -1 if gamma ramps are unsupported. </returns>
        /// <seealso cref="SetWindowGammaRamp"/>
        [DllImport(LibraryName)]
        public static extern int SDL_GetWindowGammaRamp(
            Window window,
            [Out(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] red,
            [Out(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] green,
            [Out(), MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.U2, SizeConst = 256)]
            ushort[] blue);

        /// <summary> 
        /// Provide a callback that decides if a window region has special properties.
        /// <para/>
        ///  Normally windows are dragged and resized by decorations provided by the
        ///  system window manager (a title bar, borders, etc), but for some apps, it
        ///  makes sense to drag them from somewhere else inside the window itself; for
        ///  example, one might have a borderless window that wants to be draggable
        ///  from any part, or simulate its own title bar, etc.
        /// <para/>
        ///  This function lets the app provide a callback that designates pieces of
        ///  a given window as special. This callback is run during event processing
        ///  if we need to tell the OS to treat a region of the window specially; the
        ///  use of this callback is known as "hit testing."
        /// <para/>
        ///  Mouse input may not be delivered to your application if it is within
        ///  a special area; the OS will often apply that input to moving the window or
        ///  resizing the window and not deliver it to the application.
        /// <para/>
        ///  Specifying <see cref="IntPtr.Zero"/> for a callback disables hit-testing. Hit-testing is
        ///  disabled by default.
        /// <para/>
        ///  Platforms that don't support this functionality will return -1
        ///  unconditionally, even if you're attempting to disable hit-testing.
        /// <para/>
        ///  Your callback may fire at any time, and its firing does not indicate any
        ///  specific behavior (for example, on Windows, this certainly might fire
        ///  when the OS is deciding whether to drag your window, but it fires for lots
        ///  of other reasons, too, some unrelated to anything you probably care about
        ///  _and when the mouse isn't actually at the location it is testing_).
        ///  Since this can fire at any time, you should try to keep your callback
        ///  efficient, devoid of allocations, etc.
        /// </summary>
        /// <paramref name="window"/> The window to set hit-testing on.
        /// <paramref name="callback"/> The callback to call when doing a hit-test.
        /// <paramref name="callbackData"/> An app-defined void pointer passed to the callback.
        /// <returns> 0 on success, -1 on error (including unsupported). </returns>
        [DllImport(LibraryName)]
        public static extern int SDL_SetWindowHitTest(Window window, HitTest callback, IntPtr callbackData);

        /// <summary> Destroy a window. </summary>
        [DllImport(LibraryName)]
        public static extern void SDL_DestroyWindow(Window window);

        /// <summary> Returns whether the screensaver is currently enabled (default off). </summary>
        /// <seealso cref="EnableScreenSaver"/>
        /// <seealso cref="DisableScreenSaver"/>
        [DllImport(LibraryName)]
        public static extern bool SDL_IsScreenSaverEnabled();

        /// <summary> Allow the screen to be blanked by a screensaver. </summary>
        /// <seealso cref="IsScreenSaverEnabled"/>
        /// <seealso cref="DisableScreenSaver"/>
        [DllImport(LibraryName)]
        public static extern void SDL_EnableScreenSaver();

        /// <summary> Prevent the screen from being blanked by a screensaver. </summary>
        /// <seealso cref="IsScreenSaverEnabled"/>
        /// <seealso cref="EnableScreenSaver"/>
        [DllImport(LibraryName)]
        public static extern void SDL_DisableScreenSaver();
    }
}