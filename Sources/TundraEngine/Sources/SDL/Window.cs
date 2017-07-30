using System;
using System.Security;
using System.Runtime.InteropServices;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.SDL
{
    /// <summary> Possible return values from the <see cref="HitTest"/> callback. </summary>
    /// <seealso cref="HitTest"/>
    public enum SDL_HitTestResult
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
    public delegate SDL_HitTestResult HitTest(IntPtr win, IntPtr area, IntPtr data);

    public struct Window
    {
        internal IntPtr NativeHandle;

        /// <summary> Used to indicate that you don't care what the window position is. </summary>
        public const uint PositionUndefinedMask = 0x1FFF0000u;
        public const int PositionUndefined = (int)PositionUndefinedMask;

        /// <summary> Used to indicate that the window position should be centered. </summary>
        public const uint PositionCenteredMask = 0x2FFF0000u;
        public const int PositionCentered = (int)PositionCenteredMask;

        public bool IsNull { get { return NativeHandle == IntPtr.Zero; } }

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
        public int GetDisplayIndex()
        {
            return SDL_GetWindowDisplayIndex(this);
        }

        public int SetDisplayMode()
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