using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    /// <summary>
    /// General keyboard/mouse state definitions
    /// </summary>
    public enum ButtonState
    {
        Released = 0,
        Pressed = 1
    }

    /// <summary>
    /// The types of events that can be delivered.
    /// </summary>
    public enum EventType : uint
    {
        /// <summary>
        /// Unused (do not remove)
        /// </summary>
        FirstEvent = 0,

        /* Application events */
        /// <summary>
        /// User-requested quit
        /// </summary>
        Quit = 0x100,

        /* These application events have special meaning on iOS, see README-ios.md for details */
        /// <summary>
        /// The application is being terminated by the OS
        /// <para /> Called on iOS in applicationWillTerminate()
        /// <para /> Called on Android in onDestroy()
        /// </summary>
        AppTerminating,
        /// <summary>
        /// The application is low on memory, free memory if possible.
        /// <para /> Called on iOS in applicationDidReceiveMemoryWarning()
        /// <para /> Called on Android in onLowMemory()
        /// </summary>
        AppLowMemory,
        /// <summary>
        /// The application is about to enter the background
        /// <para /> Called on iOS in applicationWillResignActive()
        /// <para /> Called on Android in onPause()
        /// </summary>
        AppWillEnterBackground,
        /// <summary>
        /// The application did enter the background and may not get CPU for some time
        /// <para /> Called on iOS in applicationDidEnterBackground()
        /// <para /> Called on Android in onPause()
        /// </summary>
        AppDidEnterBackground,
        /// <summary>
        /// The application is about to enter the foreground
        /// <para /> Called on iOS in applicationWillEnterForeground()
        /// <para /> Called on Android in onResume()
        /// </summary>
        AppWillEnterForeground,
        /// <summary>
        /// The application is now interactive
        /// <para /> Called on iOS in applicationDidBecomeActive()
        /// <para /> Called on Android in onResume()
        /// </summary>
        AppDidEnterForeground,

        /* Window events */
        /// <summary>
        /// Window state change
        /// </summary>
        WindowEvent = 0x200,
        /// <summary>
        /// System specific event
        /// </summary>
        SysWMEvent,

        /* Keyboard events */
        /// <summary>
        /// Key pressed
        /// </summary>
        KeyDown = 0x300,
        /// <summary>
        /// Key released
        /// </summary>
        KeyUp,
        /// <summary>
        /// Keyboard text editing (composition)
        /// </summary>
        TextEditing,
        /// <summary>
        /// Keyboard text input
        /// </summary>
        TextInput,
        /// <summary>
        /// Keymap changed due to a system event such as an input language or keyboard layout change.
        /// </summary>
        KeyMapChanged,

        /* Mouse events */
        /// <summary>
        /// Mouse moved
        /// </summary>
        MouseMotion = 0x400,
        /// <summary>
        /// Mouse button pressed
        /// </summary>
        MouseButtonDown,
        /// <summary>
        /// Mouse button released
        /// </summary>
        MouseButtonUp,
        /// <summary>
        /// Mouse wheel motion
        /// </summary>
        MouseWheel,

        /* Joystick events */
        /// <summary>
        /// Joystick axis motion
        /// </summary>
        JoyAxisMotion = 0x600,
        /// <summary>
        /// Joystick trackball motion 
        /// </summary>
        JoyBallMotion,
        /// <summary>
        /// Joystick hat position change
        /// </summary>
        JoyHatMotion,
        /// <summary>
        /// Joystick button presse
        /// </summary>
        JoyButtonDown,
        /// <summary>
        /// Joystick button released
        /// </summary>
        JoyButtonUp,
        /// <summary>
        /// A new joystick has been inserted into the system
        /// </summary>
        JoyDeviceAdded,
        /// <summary>
        /// An opened joystick has been removed
        /// </summary>
        JoyDeviceRemoved,

        /* Game controller events */
        /// <summary>
        /// Game controller axis motion
        /// </summary>
        ControllerAxisMotion = 0x650,
        /// <summary>
        /// Game controller button pressed
        /// </summary>
        ControllerButtonDown,
        /// <summary>
        /// Game controller button released
        /// </summary>
        ControllerButtonUp,
        /// <summary>
        // A new Game controller has been inserted into the system
        /// </summary>
        ControllerDeviceAdded,
        /// <summary>
        /// An opened Game controller has been removed
        /// </summary>
        ControllerDeviceRemoved,
        /// <summary>
        /// The controller mapping was updated
        /// </summary>
        ControllerDeviceMapped,

        /* Touch events */
        FingerDown = 0x700,
        FingerUp,
        FingerMotion,

        /* Gesture events */
        DollarGesture = 0x800,
        DollarRecord,
        MultiGesture,

        /* Clipboard events */
        /// <summary>
        /// The clipboard changed
        /// </summary>
        ClipboardUpdate = 0x900,

        /* Drag and drop events */
        /// <summary>
        /// The system requests a file open
        /// </summary>
        DropFile = 0x1000,
        /// <summary>
        /// text/plain drag-and-drop event
        /// </summary>
        DropText,
        /// <summary>
        /// A new set of drops is beginning (NULL filename)
        /// </summary>
        DropBegin,
        /// <summary>
        /// Current set of drops is now complete (NULL filename)
        /// </summary>
        DropComplete,

        /* Audio hotplug events */
        /// <summary>
        /// A new audio device is available
        /// </summary>
        AudioDeviceAdded = 0x1100,
        /// <summary>
        /// An audio device has been removed.
        /// </summary>
        AudioDeviceRemoved,

        /* Render events */
        /// <summary>
        /// The render targets have been reset and their contents need to be updated
        /// </summary>
        RenderTargetsReset = 0x2000,
        /// <summary>
        /// The device has been reset and all textures need to be recreated
        /// </summary>
        RenderDeviceReset,

        /// <summary>
        /// Events ::USEREVENT through ::LASTEVENT are for your use, and should be allocated with RegisterEvents()
        /// </summary>
        UserEvent = 0x8000,

        /// <summary>
        /// This last event is only for bounding internal arrays
        /// </summary>
        LastEvent = 0xFFFF
    }

    /// <summary>
    /// Fields shared by every event
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct CommonEvent
    {
        public readonly EventType Type;
        public readonly uint TimeStamp;
    }

    /// <summary>
    /// Window state change event data (event.window.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct WindowEvent
    {
        /// <summary>
        /// WINDOWEVENT
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The associated window
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// WindowEventID
        /// </summary>
        public readonly byte Event;
        private readonly byte Padding1;
        private readonly byte Padding2;
        private readonly byte Padding3;
        /// <summary>
        /// Event dependent data
        /// </summary>
        public readonly int Data1;
        /// <summary>
        /// Event dependent data
        /// </summary>
        public readonly int Data2;
    }

    /// <summary>
    /// Keyboard button event structure (event.key.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        /// <summary>
        /// KEYDOWN or KEYUP
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public readonly byte State;
        /// <summary>
        /// Non-zero if this is a key repeat
        /// </summary>
        public readonly byte Repeat;
        private readonly byte Padding2;
        private readonly byte Padding3;
        /// <summary>
        /// The key that was pressed or released
        /// </summary>
//        public readonly KeySym KeySym;
    }

    /// <summary>
    /// Keyboard text editing event structure (event.edit.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    unsafe public struct TextEditingEvent
    {
        /// <summary>
        /// TEXTEDITING
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// The editing text
        /// </summary>
        public fixed char Text[SDL.TEXTEDITINGEVENT_TEXT_SIZE];
        /// <summary>
        /// The start cursor of selected editing text
        /// </summary>
        public readonly int Start;
        /// <summary>
        /// The length of selected editing text
        /// </summary>
        public readonly int Length;
    }

    /// <summary>
    /// Keyboard text input event structure (event.text.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    unsafe public struct TextInputEvent
    {
        /// <summary>
        /// TEXTINPUT
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// The input text
        /// </summary>
        public fixed char Text[SDL.TEXTINPUTEVENT_TEXT_SIZE];
    }

    /// <summary>
    /// Mouse motion event structure (event.motion.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        /// <summary>
        /// MOUSEMOTION
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public readonly uint Which;
        /// <summary>
        /// The current button state
        /// </summary>
        public readonly uint State;
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public readonly int Y;
        /// <summary>
        /// The relative motion in the X direction
        /// </summary>
        public readonly int Xrel;
        /// <summary>
        /// The relative motion in the Y direction
        /// </summary>
        public readonly int Yrel;
    }

    /// <summary>
    /// Mouse button event structure (event.button.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        /// <summary>
        /// MOUSEBUTTONDOWN or MOUSEBUTTONUP
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public readonly uint Which;
        /// <summary>
        /// The mouse button index
        /// </summary>
        public readonly byte Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public readonly byte State;
        /// <summary>
        /// 1 for single-click, 2 for double-click, etc.
        /// </summary>
        public readonly byte Clicks;
        private readonly byte Padding1;
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public readonly int X;
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public readonly int Y;
    }

    /// <summary>
    /// Mouse wheel event structure (event.wheel.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        /// <summary>
        /// MOUSEWHEEL
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public readonly uint Which;
        /// <summary>
        /// The amount scrolled horizontally, positive to the right and negative to the left
        /// </summary>
        public readonly int X;
        /// <summary>
        /// The amount scrolled vertically, positive away from the user and negative toward the user
        /// </summary>
        public readonly int Y;
        /// <summary>
        /// Set to one of the MOUSEWHEEL_* defines. When FLIPPED the values in X and Y will be opposite. Multiply by -1 to change them back
        /// </summary>
        public readonly uint Direction;
    }

    /// <summary>
    /// Joystick axis motion event structure (event.jaxis.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        /// <summary>
        /// JOYAXISMOTION
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The joystick axis index
        /// </summary>
        public readonly byte Axis;
        private readonly byte Padding1;
        private readonly byte Padding2;
        private readonly byte Padding3;
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public readonly short Value;
        public readonly ushort Padding4;
    }

    /// <summary>
    /// Joystick trackball motion event structure (event.jball.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        /// <summary>
        /// JOYBALLMOTION
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The joystick trackball index
        /// </summary>
        public readonly byte Ball;
        private readonly byte Padding1;
        private readonly byte Padding2;
        private readonly byte Padding3;
        /// <summary>
        /// The relative motion in the X direction
        /// </summary>
        public readonly short Xrel;
        /// <summary>
        /// The relative motion in the Y direction
        /// </summary>
        public readonly short Yrel;
    }

    /// <summary>
    /// Joystick hat position change event structure (event.jhat.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        /// <summary>
        /// JOYHATMOTION
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The joystick hat index
        /// </summary>
        public readonly byte Hat;
        /// <summary>
        /// The hat position value. 
        /// <para /> HAT_LEFTUP HAT_UP HAT_RIGHTUP
        /// <para /> HAT_LEFT HAT_CENTERED HAT_RIGHT
        /// <para /> HAT_LEFTDOWN HAT_DOWN HAT_RIGHTDOWN
        /// </summary>
        /// <remarks> Note that zero means the POV is centered. </remarks>
        public readonly byte Value;
        private readonly byte Padding1;
        private readonly byte Padding2;
    }

    /// <summary>
    /// Joystick button event structure (event.jbutton.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        /// <summary>
        /// JOYBUTTONDOWN or JOYBUTTONUP
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The joystick button index
        /// </summary>
        public readonly byte Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public readonly byte State;
        private readonly byte Padding1;
        private readonly byte Padding2;
    }

    /// <summary>
    /// Joystick device event structure (event.jdevice.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        /// <summary>
        /// JOYDEVICEADDED or JOYDEVICEREMOVED
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick device index for the ADDED event, instance id for the REMOVED event
        /// </summary>
        public readonly int Which;
    }

    /// <summary>
    /// Game controller axis motion event structure (event.caxis.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct ControllerAxisEvent
    {
        /// <summary>
        /// CONTROLLERAXISMOTION
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The controller axis (GameControllerAxis)
        /// </summary>
        public readonly byte Axis;
        private readonly byte Padding1;
        private readonly byte Padding2;
        private readonly byte Padding3;
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public readonly short Value;
        public readonly ushort Padding4;
    }

    /// <summary>
    /// Game controller button event structure (event.cbutton.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct ControllerButtonEvent
    {
        /// <summary>
        /// CONTROLLERBUTTONDOWN or CONTROLLERBUTTONUP
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public readonly int Which;
        /// <summary>
        /// The controller button (GameControllerButton)
        /// </summary>
        public readonly byte Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public readonly byte State;
        private readonly byte Padding1;
        private readonly byte Padding2;
    }

    /// <summary>
    /// Controller device event structure (event.cdevice.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct ControllerDeviceEvent
    {
        /// <summary>
        /// CONTROLLERDEVICEADDED, CONTROLLERDEVICEREMOVED or CONTROLLERDEVICEREMAPPED
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The joystick device index for the ADDED event, instance id for the REMOVED or REMAPPED event
        /// </summary>
        public readonly int Which;
    }

    /// <summary>
    /// Audio device event structure (event.adevice.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        /// <summary>
        /// AUDIODEVICEADDED or AUDIODEVICEREMOVED
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The audio device index for the ADDED event (valid until next GetNumAudioDevices() call), AudioDeviceID for the REMOVED event
        /// </summary>
        public readonly uint Which;
        /// <summary>
        /// Zero if an output device, non-zero if a capture device.
        /// </summary>
        public readonly byte Iscapture;
        private readonly byte Padding1;
        private readonly byte Padding2;
        private readonly byte Padding3;
    }

    /// <summary>
    /// Touch finger event structure (event.tfinger.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        /// <summary>
        /// FINGERMOTION or FINGERDOWN or FINGERUP
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public readonly long TouchId;
        public readonly long FingerId;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public readonly float X;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public readonly float Y;
        /// <summary>
        /// Normalized in the range -1...1
        /// </summary>
        public readonly float Dx;
        /// <summary>
        /// Normalized in the range -1...1 
        /// </summary>
        public readonly float Dy;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public readonly float Pressure;
    }

    /// <summary>
    /// Multiple Finger Gesture Event (event.mgesture.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct MultiGestureEvent
    {
        /// <summary>
        /// MULTIGESTURE
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public readonly long TouchId;
        public readonly float DTheta;
        public readonly float DDist;
        public readonly float X;
        public readonly float Y;
        public readonly ushort NumFingers;
        public readonly ushort Padding;
    }

    /// <summary>
    /// Dollar Gesture Event (event.dgesture.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct DollarGestureEvent
    {
        /// <summary>
        /// DOLLARGESTURE or DOLLARRECORD
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public readonly long TouchId;
        public readonly long GestureId;
        public readonly uint NumFingers;
        public readonly float Error;
        /// <summary>
        /// Normalized center of gesture
        /// </summary>
        public readonly float X;
        /// <summary>
        /// Normalized center of gesture
        /// </summary>
        public readonly float Y;
    }

    /// <summary>
    /// An event used to request a file open by the system (event.drop.*)
    /// This event is enabled by default, you can disable it with EventState().
    /// </summary>
    /// <remarks>
    /// If this event is enabled, you must free the filename in the event. 
    /// </remarks>
    [StructLayout (LayoutKind.Sequential)]
    unsafe public struct DropEvent
    {
        /// <summary>
        /// DROPBEGIN or DROPFILE or DROPTEXT or DROPCOMPLETE
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The file name, which should be freed with free(), is NULL on begin/complete
        /// </summary>
        public readonly char* File;
        /// <summary>
        /// The window that was dropped on, if any
        /// </summary>
        public readonly uint WindowID;
    }

    /// <summary>
    /// The "quit requested" event
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct QuitEvent
    {
        /// <summary>
        /// QUIT
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
    }

    /// <summary>
    /// OS Specific event
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    public struct OSEvent
    {
        /// <summary>
        /// QUIT
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
    }

    /// <summary>
    /// A user-defined event type (event.user.*)
    /// </summary>
    [StructLayout (LayoutKind.Sequential)]
    unsafe public struct UserEvent
    {
        /// <summary>
        /// USEREVENT through LASTEVENT-1
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// The associated window if any
        /// </summary>
        public readonly uint WindowID;
        /// <summary>
        /// User defined event code
        /// </summary>
        public readonly int Code;
        /// <summary>
        /// User defined data pointer
        /// </summary>
        public readonly void* Data1;
        /// <summary>
        /// User defined data pointer
        /// </summary>
        public readonly void* Data2;
    }

    /// <summary>
    /// A video driver dependent system event (event.syswm.*)
    /// This event is disabled by default, you can enable it with EventState()
    /// </summary>
    /// <remarks>
    /// If you want to use this event, you should include syswm.h.
    /// </remarks>
    [StructLayout (LayoutKind.Sequential)]
    unsafe public struct SysWMEvent
    {
        /// <summary>
        /// SYSWMEVENT
        /// </summary>
        public readonly EventType Type;
        public readonly uint Timestamp;
        /// <summary>
        /// Driver dependent data, defined in syswm.h
        /// </summary>
        public readonly IntPtr Msg; // SDL_SysWMmsg*, system-dependent
    }

    /// <summary>
    /// General event structure
    /// </summary>
    [StructLayout (LayoutKind.Explicit)]
    public struct Event
    {
        /// <summary>
        /// Event type, shared with all events
        /// </summary>
        [FieldOffset (0)] public readonly EventType Type;
        /// <summary>
        /// Common event data
        /// </summary>
        [FieldOffset (0)] public readonly CommonEvent Common;
        /// <summary>
        /// Window event data
        /// </summary>
        [FieldOffset (0)] public readonly WindowEvent Window;
        /// <summary>
        /// Keyboard event data
        /// </summary>
        [FieldOffset (0)] public readonly KeyboardEvent Key;
        /// <summary>
        /// Text editing event data
        /// </summary>
        [FieldOffset (0)] public readonly TextEditingEvent Edit;
        /// <summary>
        /// Text input event data
        /// </summary>
        [FieldOffset (0)] public readonly TextInputEvent Text;
        /// <summary>
        /// Mouse motion event data
        /// </summary>
        [FieldOffset (0)] public readonly MouseMotionEvent Motion;
        /// <summary>
        /// Mouse button event data
        /// </summary>
        [FieldOffset (0)] public readonly MouseButtonEvent Button;
        /// <summary>
        /// Mouse wheel event data
        /// </summary>
        [FieldOffset (0)] public readonly MouseWheelEvent Wheel;
        /// <summary>
        /// Joystick axis event data
        /// </summary>
        [FieldOffset (0)] public readonly JoyAxisEvent Jaxis;
        /// <summary>
        /// Joystick ball event data
        /// </summary>
        [FieldOffset (0)] public readonly JoyBallEvent Jball;
        /// <summary>
        /// Joystick hat event data
        /// </summary>
        [FieldOffset (0)] public readonly JoyHatEvent Jhat;
        /// <summary>
        /// Joystick button event data
        /// </summary>
        [FieldOffset (0)] public readonly JoyButtonEvent Jbutton;
        /// <summary>
        /// Joystick device change event data
        /// </summary>
        [FieldOffset (0)] public readonly JoyDeviceEvent Jdevice;
        /// <summary>
        /// Game Controller axis event data
        /// </summary>
        [FieldOffset (0)] public readonly ControllerAxisEvent Caxis;
        /// <summary>
        /// Game Controller button event data
        /// </summary>
        [FieldOffset (0)] public readonly ControllerButtonEvent Cbutton;
        /// <summary>
        /// Game Controller device event data
        /// </summary>
        [FieldOffset (0)] public readonly ControllerDeviceEvent Cdevice;
        /// <summary>
        /// Audio device event data
        /// </summary>
        [FieldOffset (0)] public readonly AudioDeviceEvent Adevice;
        /// <summary>
        /// Quit request event data
        /// </summary>
        [FieldOffset (0)] public readonly QuitEvent Quit;
        /// <summary>
        /// Custom event data
        /// </summary>
        [FieldOffset (0)] public readonly UserEvent User;
        /// <summary>
        /// System dependent window event data
        /// </summary>
        [FieldOffset (0)] public readonly SysWMEvent Syswm;
        /// <summary>
        /// Touch finger event data
        /// </summary>
        [FieldOffset (0)] public readonly TouchFingerEvent Tfinger;
        /// <summary>
        /// Gesture event data
        /// </summary>
        [FieldOffset (0)] public readonly MultiGestureEvent Mgesture;
        /// <summary>
        /// Gesture event data
        /// </summary>
        [FieldOffset (0)] public readonly DollarGestureEvent Dgesture;
        /// <summary>
        /// Drag and drop event data
        /// </summary>
        [FieldOffset (0)] public readonly DropEvent Drop;

        private unsafe struct Padding
        {
            private fixed byte _bytes[56];
        }

        /// <summary>
        /// This is necessary for ABI compatibility between Visual C++ and GCC.
        /// <para /> Visual C++ will respect the push pack pragma and use 52 bytes for this structure, and GCC will use the alignment of the largest datatype within the union, which is 8 bytes. So...we'll add padding to force the size to be 56 bytes for both.
        /// </summary>
        [FieldOffset (0)] Padding _padding;
    }

    unsafe public static partial class SDL
    {
        public enum EventAction
        {
            AddEvent,
            PeekEvent,
            GetEvent
        }

        public enum EventState : int
        {
            Query = -1,
            Ignore = 0,
            Disable = 0,
            Enable = 1
        }

        public const int TEXTEDITINGEVENT_TEXT_SIZE = 32;
        public const int TEXTINPUTEVENT_TEXT_SIZE = 32;

        /// <summary>
        /// Pumps the event loop, gathering events from the input devices.
        /// <para> This function updates the event queue and internal input device state. </para>
        /// <para> This should only be run in the thread that sets the video mode. </para>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_PumpEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static void PumpEvents ();

        /// <summary>
        /// Checks the event queue for messages and optionally returns them.
        /// <para> If <paramref name="action"/> is AddEvent, up to <paramref name="numevents"/> events will be added to the back of the event queue. </para>
        /// <para> If <paramref name="action"/> is PeekEvent, up to <paramref name="numevents"/> events at the front of the event queue, within the specified minimum and maximum type, will be returned and will not be removed from the queue. </para>
        /// <para> If <paramref name="action"/> is GetEvent, up to <paramref name="numevents"/> events at the front of the event queue, within the specified minimum and maximum type, will be returned and will be removed from the queue. </para>
        /// <para> This funcion is thread-safe. </para>
        /// </summary>
        /// <returns>
        /// The number of events actually stored, or -1 if there was an error.
        /// </returns>
        [DllImport (LibName, EntryPoint = "SDL_PeepEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static int PeepEvents (
            Event[] events,
            int numevents,
            EventAction action,
            EventType minType,
            EventType maxType);

        /// <summary>
        /// Checks to see if certain event types are in the event queue.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasEvent", CallingConvention = CallingConvention.Cdecl)]
        extern public static bool HasEvent (EventType type);

        /// <summary>
        /// Checks to see if certain event types are in the event queue.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_HasEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static bool HasEvents (EventType minType, EventType maxType);

        /// <summary>
        /// This function clears events from the event queue.
        /// <para> This function only affects currently queued events. If you want to make sure that all pending OS events are flushed, you can call PumpEvents() on the main thread immediately before the flush call. </para>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_FlushEvent", CallingConvention = CallingConvention.Cdecl)]
        extern public static void FlushEvent (EventType type);

        /// <summary>
        /// This function clears events from the event queue.
        /// <para> This function only affects currently queued events. If you want to make sure that all pending OS events are flushed, you can call PumpEvents() on the main thread immediately before the flush call. </para>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_FlushEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static void FlushEvents (EventType minType, EventType maxType);

        /// <summary>
        /// Polls for currently pending events.
        /// </summary>
        /// <returns> 1 if there are any pending events, or 0 if there are none available. </returns>
        /// <param name="sdlEvent"> If not NULL, the next event is removed from the queue and stored in that area. </param>
        [DllImport (LibName, EntryPoint = "SDL_PollEvent", CallingConvention = CallingConvention.Cdecl)]
        extern public static int PollEvent (out Event sdlEvent);

        /// <summary>
        /// Waits indefinitely for the next available event.
        /// </summary>
        /// <returns> 1, or 0 if there was an error while waiting for events. </returns>
        /// <param name="sdlEvent"> If not NULL, the next event is removed from the queue and stored in that area. </param>
        [DllImport (LibName, EntryPoint = "SDL_WaitEvent", CallingConvention = CallingConvention.Cdecl)]
        extern public static int WaitEvent (out Event sdlEvent);

        /// <summary>
        /// Waits until the specified timeout (in milliseconds) for the next available event.
        /// </summary>
        /// <returns> 1, or 0 if there was an error while waiting for events. </returns>
        /// <param name="sdlEvent"> If not NULL, the next event is removed from the queue and stored in that area. </param>
        /// <param name="timeout"> The timeout (in milliseconds) to wait for next event. </param>
        [DllImport (LibName, EntryPoint = "SDL_WaitEventTimeout", CallingConvention = CallingConvention.Cdecl)]
        extern public static int WaitEventTimeout (out Event sdlEvent, int timeout);

        /// <summary>
        /// Add an event to the event queue.
        /// </summary>
        /// <returns> 1 on success, 0 if the event was filtered, or -1 if the event queue was full or there was some other error. </returns>
        [DllImport (LibName, EntryPoint = "SDL_PushEvent", CallingConvention = CallingConvention.Cdecl)]
        extern public static int PushEvent (out Event sdlEvent);

        // TODO: Check if params are working
        [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
        public delegate int EventFilter (void* userData, ref Event sdlEvent);

        /// <summary>
        /// Sets up a filter to process all events before they change internal state and are posted to the internal event queue.
        /// <para> The filter is prototyped as: 
        /// <c> int EventFilter (void* userData, ref Event sdlEvent); </c> </para>
        /// If the filter returns 1, then the event will be added to the internal queue. 
        /// If it returns 0, then the event will be dropped from the queue, but the internal state will still be updated.This allows selective filtering of dynamically arriving events.
        /// <para> Be very careful of what you do in the event filter function, as it may run in a different thread! </para>
        /// <para> There is one caveat when dealing with the ::QuitEvent event type.  The event filter is only called when the window manager desires to close the application window.  If the event filter returns 1, then the window will be closed, otherwise the window will remain open if possible. </para>
        /// <para> If the quit event is generated by an interrupt signal, it will bypass the internal queue and be delivered to the application at the next event poll. </para>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_SetEventFilter", CallingConvention = CallingConvention.Cdecl)]
        extern public static void SetEventFilter (EventFilter filter, void* userData);

        /// <summary>
        /// Return the current event filter - can be used to "chain" filters.
        /// If there is no event filter set, this function returns FALSE.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetEventFilter", CallingConvention = CallingConvention.Cdecl)]
        extern public static bool GetEventFilter (out EventFilter filter, out void* userData);

        /// <summary>
        /// Add a function which is called when an event is added to the queue.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_AddEventWatch", CallingConvention = CallingConvention.Cdecl)]
        extern public static void AddEventWatch (EventFilter filter, void* userData);

        /// <summary>
        /// Remove an event watch function added with AddEventWatch().
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_DelEventWatch", CallingConvention = CallingConvention.Cdecl)]
        extern public static void DelEventWatch (EventFilter filter, void* userData);

        /// <summary>
        /// Run the filter function on the current event queue, removing any events for which the filter returns 0.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_FilterEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static void FilterEvents (EventFilter filter, void* userData);

        /// <summary>
        /// This function allows you to set the state of processing certain events.
        /// - If <paramref name="state"/> is set to EventState.Ignore, that event will be automatically dropped from the event queue and will not be filtered.
        /// - If <paramref name="state"/> is set to EventState.Enable, that event will be processed normally.
        /// - If <paramref name="state"/> is set to EventState.Query, EventState() will return the current processing state of the specified event.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_EventState", CallingConvention = CallingConvention.Cdecl)]
        extern public static byte GetEventState (EventType type, EventState state);

        public static byte GetEventState (EventType type)
        {
            return GetEventState (type, EventState.Query);
        }

        /// <summary>
        /// This function allocates a set of user-defined events, and returns the beginning event number for that set of events.
        /// <para> If there aren't enough user-defined events left, this function returns (uint)-1 </para>
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_RegisterEvents", CallingConvention = CallingConvention.Cdecl)]
        extern public static uint RegisterEvents (int numEvents);
    }
}