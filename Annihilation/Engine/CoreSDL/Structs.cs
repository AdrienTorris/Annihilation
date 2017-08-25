using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    //
    // SDL_events.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct CommonEvent
    {
        public EventType Type;
        public uint TimeStamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public byte Event;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        public int Data1;
        public int Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public ButtonState State;
        public byte Repeat;
        private byte Padding2;
        private byte Padding3;
        public KeySym KeySym;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct TextEditingEvent
    {
        public const int TextEditingEventTextSize = 32;

        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public fixed byte Text[TextEditingEventTextSize];
        public int Start;
        public int Length;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct TextInputEvent
    {
        public const int TextInputEventTextSize = 32;

        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public fixed byte Text[TextInputEventTextSize];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public uint Which;
        public uint State;
        public int X;
        public int Y;
        public int Xrel;
        public int Yrel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public uint Which;
        public MouseButton Button;
        public ButtonState State;
        public byte Clicks;
        private byte Padding1;
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public uint Which;
        public int X;
        public int Y;
        public uint Direction;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        public short Value;
        public ushort Padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Ball;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        public short Xrel;
        public short Yrel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Hat;
        public byte Value;
        private byte Padding1;
        private byte Padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Button;
        public byte State;
        private byte Padding1;
        private byte Padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerAxisEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        public short Value;
        public ushort Padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerButtonEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
        public byte Button;
        public byte State;
        private byte Padding1;
        private byte Padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerDeviceEvent
    {
        public EventType Type;
        public uint Timestamp;
        public int Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint Which;
        public byte Iscapture;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        public EventType Type;
        public uint Timestamp;
        public long TouchId;
        public long FingerId;
        public float X;
        public float Y;
        public float Dx;
        public float Dy;
        public float Pressure;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MultiGestureEvent
    {
        public EventType Type;
        public uint Timestamp;
        public long TouchId;
        public float DTheta;
        public float DDist;
        public float X;
        public float Y;
        public ushort NumFingers;
        public ushort Padding;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DollarGestureEvent
    {
        public EventType Type;
        public uint Timestamp;
        public long TouchId;
        public long GestureId;
        public uint NumFingers;
        public float Error;
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct DropEvent
    {
        public EventType Type;
        public uint Timestamp;
        public byte* File;
        public uint WindowID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        public EventType Type;
        public uint Timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct OSEvent
    {
        public EventType Type;
        public uint Timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct UserEvent
    {
        public EventType Type;
        public uint Timestamp;
        public uint WindowID;
        public int Code;
        public void* Data1;
        public void* Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct SysWMEvent
    {
        public EventType Type;
        public uint Timestamp;
        public IntPtr Msg;
    }

    [StructLayout(LayoutKind.Explicit, Size = 56)]
    public struct Event
    {
        [FieldOffset(0)] public EventType Type;
        [FieldOffset(0)] public CommonEvent Common;
        [FieldOffset(0)] public WindowEvent Window;
        [FieldOffset(0)] public KeyboardEvent Key;
        [FieldOffset(0)] public TextEditingEvent Edit;
        [FieldOffset(0)] public TextInputEvent Text;
        [FieldOffset(0)] public MouseMotionEvent MouseMotion;
        [FieldOffset(0)] public MouseButtonEvent MouseButton;
        [FieldOffset(0)] public MouseWheelEvent MouseWheel;
        [FieldOffset(0)] public JoyAxisEvent Jaxis;
        [FieldOffset(0)] public JoyBallEvent Jball;
        [FieldOffset(0)] public JoyHatEvent Jhat;
        [FieldOffset(0)] public JoyButtonEvent Jbutton;
        [FieldOffset(0)] public JoyDeviceEvent Jdevice;
        [FieldOffset(0)] public ControllerAxisEvent Caxis;
        [FieldOffset(0)] public ControllerButtonEvent Cbutton;
        [FieldOffset(0)] public ControllerDeviceEvent Cdevice;
        [FieldOffset(0)] public AudioDeviceEvent Adevice;
        [FieldOffset(0)] public QuitEvent Quit;
        [FieldOffset(0)] public UserEvent User;
        [FieldOffset(0)] public SysWMEvent Syswm;
        [FieldOffset(0)] public TouchFingerEvent Tfinger;
        [FieldOffset(0)] public MultiGestureEvent Mgesture;
        [FieldOffset(0)] public DollarGestureEvent Dgesture;
        [FieldOffset(0)] public DropEvent Drop;
    }

    //
    // SDL_gamecontroller.h
    //
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct Hat
    {
        public readonly int HatIndex;
        public readonly int HatMask;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GameControllerButtonBind
    {
        [FieldOffset(0)] public readonly GameControllerBindType BindType;
        [FieldOffset(4)] public readonly int Button;
        [FieldOffset(4)] public readonly int Axis;
        [FieldOffset(4)] public readonly Hat Hat;
    }

    //
    // SDL_keyboard.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct KeySym
    {
        public readonly ScanCode ScanCode;
        public readonly KeyCode Sym;
        public readonly KeyMod Mod;
        public readonly uint Unused;
    }

    //
    // SDL_messagebox.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MessageBoxButtonData
    {
        public MessageBoxButtonFlags Flags;
        public int ButtonID;
        public Text Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColorScheme
    {
        public MessageBoxColor BackgroundColor;
        public MessageBoxColor TextColor;
        public MessageBoxColor ButtonBorderColor;
        public MessageBoxColor ButtonBackgroundColor;
        public MessageBoxColor ButtonSelectedColor;
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MessageBoxData
    {
        public MessageBoxFlags Flags;
        public Window Window;
        public Text Title;
        public Text Message;
        public int NumButtons;
        public MessageBoxButtonData* Buttons;
        public MessageBoxColorScheme* ColorScheme;
    }

    //
    // SDL_pixels.h
    //
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    public struct Palette
    {
        internal IntPtr NativeHandle;
        /*public int NumColors;
        public Color* Colors;
        public uint Version;
        public int RefCount;*/
    }

    public unsafe struct PixelFormat
    {
        public readonly uint Format;
        public readonly Palette Palette;
        public readonly byte BitsPerPixel;
        public readonly byte BytesPerPixel;
        public fixed byte Padding[2];
        public readonly uint RMask;
        public readonly uint GMask;
        public readonly uint BMask;
        public readonly uint AMask;
        public readonly byte RLoss;
        public readonly byte GLoss;
        public readonly byte BLoss;
        public readonly byte ALoss;
        public readonly byte RShift;
        public readonly byte GShift;
        public readonly byte BShift;
        public readonly byte AShift;
        public readonly int RefCount;
        public readonly PixelFormat* Next;
    }

    //
    // SDL_rect.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    //
    // SDL_render.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RendererInfo
    {
        public byte* Name;
        public RendererFlags Flags;
        public uint NumTextureFormats;
        public fixed uint TextureFormats[16];
        public int MaxTextureWidth;
        public int MaxTextureHeight;
    }

    //
    // SDL_rwops.h
    //
    public struct RWops
    {
        public IntPtr Handle;
    }

    //
    // SDL_shape.h
    //
    [StructLayout(LayoutKind.Explicit, Pack = 1)]
    public struct WindowShapeParams
    {
        [FieldOffset(0)] public byte BinarizationCutoff;
        [FieldOffset(0)] public Color ColorKey;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowShape
    {
        public WindowShapeMode Mode;
        public WindowShapeParams Parameters;
    }

    //
    // SDL_surface.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Surface
    {
        public readonly uint Flags;
        public readonly PixelFormat* Format;
        public readonly int W;
        public readonly int H;
        public readonly int Pitch;
        public void* Pixels;
        public void* UserData;
        public readonly int Locked;
        public readonly void* LockData;
        public readonly Rect ClipRect;
        private IntPtr Map;
        public int RefCount;
    }

    //
    // SDL_syswm.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct SysWMInfo
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct InfoUnion
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct WindowsInfo
            {
                public readonly IntPtr Window;
                public readonly IntPtr Hdc;
                public readonly IntPtr HInstance;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WinRTInfo
            {
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct X11Info
            {
                public readonly IntPtr Display;
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct DirectFBInfo
            {
                public readonly IntPtr DirectFB;
                public readonly IntPtr Window;
                public readonly IntPtr Surface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct CocoaInfo
            {
                public readonly IntPtr Window;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct UIKitInfo
            {
                public readonly IntPtr Window;
                public readonly uint FrameBuffer;
                public readonly uint ColorBuffer;
                public readonly uint ResolveFrameBuffer;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct WaylandInfo
            {
                public readonly IntPtr Display;
                public readonly IntPtr Surface;
                public readonly IntPtr ShellSurface;
            }

            [StructLayout(LayoutKind.Sequential)]
            public struct MirInfo
            {
                public readonly IntPtr Connection;
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

    //
    // SDL_timer.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct TimerID
    {
        public int ID;
    }

    //
    // SDL_version.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public struct Version
    {
        public readonly byte Major;
        public readonly byte Minor;
        public readonly byte Patch;

        public const int MajorVersion = 2;
        public const int MinorVersion = 0;
        public const int PatchLevel = 5;

        public static Version Current => new Version(MajorVersion, MinorVersion, PatchLevel);

        public Version(byte major, byte minor, byte patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }
    }

    //
    // SDL_video.h
    //
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct DisplayMode
    {
        public uint Format;
        public int Width;
        public int Height;
        public int RefreshRate;
        public void* DriverData;
    }
}