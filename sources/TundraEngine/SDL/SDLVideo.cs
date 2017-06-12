using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        /// <summary>
        /// The flags on a window.
        /// </summary>
        /// <seealso cref="GetWindowFlags"/>
        [Flags]
        public enum WindowFlags
        {
            /// <summary>
            /// Fullscreen window
            /// </summary>
            Fullscreen = 1 << 0,
            /// <summary>
            /// Window usable with OpenGL context
            /// </summary>
            OpenGL = 1 << 1,
            /// <summary>
            /// Window is visible
            /// </summary>
            Shown = 1 << 2,
            /// <summary>
            /// Window is not visible
            /// </summary>
            Hidden = 1 << 3,
            /// <summary>
            /// No window decoration
            /// </summary>
            Borderless = 1 << 4,
            /// <summary>
            /// Window can be resized
            /// </summary>
            Resizable = 1 << 5,
            /// <summary>
            /// Window is minimized
            /// </summary>
            Minimized = 1 << 6,
            /// <summary>
            /// Window is maximized
            /// </summary>
            Maximized = 1 << 7,
            /// <summary>
            /// Window has grabbed input focus
            /// </summary>
            InputGrabbed = 1 << 8,
            /// <summary>
            /// Window has input focus
            /// </summary>
            InputFocus = 1 << 9,
            /// <summary>
            /// Window has mouse focus
            /// </summary>
            MouseFocus = 1 << 10,
            FullscreenDesktop = (Fullscreen | 1 << 12),
            /// <summary>
            /// Window not created by SDL
            /// </summary>
            Foreign = 1 << 11,
            /// <summary>
            /// Window should be created in high-DPI mode if supported
            /// </summary>
            AllowHighDPI = 1 << 13,
            /// <summary>
            /// Window has mouse captured (unrelated to <see cref="InputGrabbed"/>)
            /// </summary>
            MouseCapture = 1 << 14,
            /// <summary>
            /// Window should always be above others
            /// </summary>
            AlwaysOnTop = 1 << 15,
            /// <summary>
            /// Window should not be added to the taskbar
            /// </summary>
            SkipTaskbar = 1 << 16,
            /// <summary>
            /// Window should be treated as a utility window
            /// </summary>
            Utility = 1 << 17,
            /// <summary>
            /// Window should be treated as a tooltip
            /// </summary>
            Tooltip = 1 << 18,
            /// <summary>
            /// Window should be treated as a popup menu
            /// </summary>
            PopupMenu = 1 << 19,
            /// <summary>
            /// Window usable for Vulkan surface
            /// </summary>
            Vulkan = 1 << 20,
        }

        public enum WindowEventID
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

        /// <summary>
        /// Used to indicate that you don't care what the window position is.
        /// </summary>
        public const uint WindowPositionUndefinedMask = 0x1FFF0000u;
        public const uint WindowPositionUndefined = WindowPositionUndefinedMask;

        /// <summary>
        /// Used to indicate that the window position should be centered.
        /// </summary>
        public const uint WindowPositionCenteredMask = 0x2FFF0000u;
        public const uint WindowPositionCentered = WindowPositionCenteredMask;

        public static uint WindowPositionUndefinedDisplay (uint x)
        {
            return WindowPositionUndefinedMask | x;
        }

        public static bool IsWindowPositionUndefined (int x)
        {
            return (x & 0xFFFF0000) == WindowPositionUndefinedMask;
        }

        public static uint WindowPositionCenteredDisplay (uint x)
        {
            return WindowPositionCenteredMask | x;
        }

        public static bool IsWindowPositionCentered (int x)
        {
            return (x & 0xFFFF0000) == WindowPositionCenteredMask;
        }

        /// <summary>
        /// Get the number of video drivers compiled into SDL
        /// </summary>
        /// <seealso cref="GetVideoDriver"/>
        [DllImport (LibName, EntryPoint = "SDL_GetNumVideoDrivers", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetNumVideoDrivers ();

        [DllImport (LibName, EntryPoint = "SDL_GetVideoDriver", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetVideoDriverInternal (int index);

        /// <summary>
        /// Get the name of a built in video driver.
        /// </summary>
        /// <remarks> The video drivers are presented in the order in which they are normally checked during initialization. </remarks>
        /// <seealso cref="GetNumVideoDrivers"/>
        public static string GetVideoDriver (int index)
        {
            return GetVideoDriverInternal (index).ToStr ();
        }
    }
}