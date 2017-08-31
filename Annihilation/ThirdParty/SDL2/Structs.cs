using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        //
        // SDL_atomic.h
        //
        public struct Atomic
        {
            public int Value;
        }

        //
        // SDL_audio.h
        //
        [StructLayout(LayoutKind.Sequential)]
        public struct AudioSpec
        {
            public int Frequency;
            public AudioFormat Format;
            public byte Channels;
            public byte Silence;
            public ushort Samples;
            public ushort Padding;
            public uint Size;
            public AudioCallback Callback;
            public IntPtr UserData;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AudioCVT
        {
            public int Needed;
            public AudioFormat SrcFormat;
            public AudioFormat DstFormat;
            public double RateIncrement;
            public byte[] Buffer;
            public int Length;
            public int LengthCVT;
            public int LengthMultiplier;
            public double LengthRatio;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = AudioCVTMaxFilters + 1)]
            public AudioFilter[] Filters;
            public int FilterIndex;
        }

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
            public WindowEventID Event;
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
            public Text File;
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
        public struct UserEvent
        {
            public EventType Type;
            public uint Timestamp;
            public uint WindowID;
            public int Code;
            public IntPtr Data1;
            public IntPtr Data2;
        }

        [StructLayout(LayoutKind.Sequential)]
        unsafe public struct SysWMEvent
        {
            public EventType Type;
            public uint Timestamp;
            public SysWMmsg* Msg;
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
        [StructLayout(LayoutKind.Sequential)]
        public struct Hat
        {
            public int HatIndex;
            public int HatMask;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct GameControllerButtonBind
        {
            [FieldOffset(0)] public GameControllerBindType BindType;
            [FieldOffset(4)] public int Button;
            [FieldOffset(4)] public int Axis;
            [FieldOffset(4)] public Hat Hat;
        }

        //
        // SDL_haptic.h
        //
        [StructLayout(LayoutKind.Sequential)]
        public struct HapticDirection
        {
            public HapticDirectionType Type;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HapticConstant
        {
            public HapticEffectType Type;
            public HapticDirection Direction;
            public uint Length;
            public ushort Delay;
            public ushort Button;
            public ushort Interval;
            public short Level;
            public ushort AttackLength;
            public ushort AttackLevel;
            public ushort FadeLength;
            public ushort FadeLevel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HapticPeriodic
        {
            public HapticEffectType Type;
            public HapticDirection Direction;
            public uint Length;
            public ushort Delay;
            public ushort Button;
            public ushort Interval;
            public ushort Period;
            public short Magnitude;
            public short Offset;
            public ushort Phase;
            public ushort AttackLength;
            public ushort AttackLevel;
            public ushort FadeLength;
            public ushort FadeLevel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HapticCondition
        {
            public HapticEffectType Type;
            public HapticDirection Direction;
            public uint Length;
            public ushort Delay;
            public ushort Button;
            public ushort Interval;
            public fixed ushort RightSat[3];
            public fixed ushort LeftSat[3];
            public fixed short RightCoeff[3];
            public fixed short LeftCoeff[3];
            public fixed ushort Deadband[3];
            public fixed short Center[3];
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HapticRamp
        {
            public HapticEffectType Type;
            public HapticDirection Direction;
            public uint Length;
            public ushort Delay;
            public ushort Button;
            public ushort Interval;
            public short Start;
            public short End;
            public ushort AttackLength;
            public ushort AttackLevel;
            public ushort FadeLength;
            public ushort FadeLevel;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct HapticLeftRight
        {
            public HapticEffectType Type;
            public uint Length;
            public ushort LargeMagnitude;
            public ushort SmallMagnitude;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct HapticCustom
        {
            public HapticEffectType Type;
            public HapticDirection Direction;
            public uint Length;
            public ushort Delay;
            public ushort Button;
            public ushort Interval;
            public byte Channels;
            public ushort Period;
            public ushort Samples;
            public ushort[] Data;
            public ushort AttackLength;
            public ushort AttackLevel;
            public ushort FadeLength;
            public ushort FadeLevel;
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct HapticEffect
        {
            [FieldOffset(0)] public HapticEffectType Type;
            [FieldOffset(0)] public HapticConstant Constant;
            [FieldOffset(0)] public HapticPeriodic Periodic;
            [FieldOffset(0)] public HapticCondition Condition;
            [FieldOffset(0)] public HapticRamp Ramp;
            [FieldOffset(0)] public HapticLeftRight LeftRight;
            [FieldOffset(0)] public HapticCustom Custom;
        }

        //
        // SDL_keyboard.h
        //
        [StructLayout(LayoutKind.Sequential)]
        public struct KeySym
        {
            public Scancode ScanCode;
            public Keycode Sym;
            public KeyMod Mod;
            private uint Unused;
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
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int)MessageBoxColorType.Max)]
            public MessageBoxColor[] Colors;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct MessageBoxData
        {
            public MessageBoxFlags Flags;
            public Window Window;
            public Text Title;
            public Text Message;
            public int NumButtons;
            public MessageBoxButtonData[] Buttons;
            public MessageBoxColorScheme? ColorScheme;
        }

        //
        // SDL_pixels.h
        //
        [StructLayout(LayoutKind.Sequential)]
        public struct Color
        {
            public byte R;
            public byte G;
            public byte B;
            public byte A;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Palette
        {
            public int NumColors;
            public Color[] Colors;
            public uint Version;
            public int RefCount;
        }

        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct PixelFormat
        {
            public uint Format;
            public IntPtr Palette;
            public byte BitsPerPixel;
            public byte BytesPerPixel;
            public fixed byte Padding[2];
            public uint RMask;
            public uint GMask;
            public uint BMask;
            public uint AMask;
            public byte RLoss;
            public byte GLoss;
            public byte BLoss;
            public byte ALoss;
            public byte RShift;
            public byte GShift;
            public byte BShift;
            public byte AShift;
            public int RefCount;
            public IntPtr Next;
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
            public Text Name;
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
            public uint Flags;
            public PixelFormat* Format;
            public int W;
            public int H;
            public int Pitch;
            public void* Pixels;
            public void* UserData;
            public int Locked;
            public void* LockData;
            public Rect ClipRect;
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
                    public IntPtr Window;
                    public IntPtr Hdc;
                    public IntPtr HInstance;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct WinRTInfo
                {
                    public IntPtr Window;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct X11Info
                {
                    public IntPtr Display;
                    public IntPtr Window;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct DirectFBInfo
                {
                    public IntPtr DirectFB;
                    public IntPtr Window;
                    public IntPtr Surface;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct CocoaInfo
                {
                    public IntPtr Window;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct UIKitInfo
                {
                    public IntPtr Window;
                    public uint FrameBuffer;
                    public uint ColorBuffer;
                    public uint ResolveFrameBuffer;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct WaylandInfo
                {
                    public IntPtr Display;
                    public IntPtr Surface;
                    public IntPtr ShellSurface;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct MirInfo
                {
                    public IntPtr Connection;
                    public IntPtr Surface;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct AndroidInfo
                {
                    public IntPtr Window;
                    public IntPtr Surface;
                }

                [StructLayout(LayoutKind.Sequential)]
                public struct VivanteInfo
                {
                    public IntPtr Display;
                    public IntPtr Window;
                }

                [FieldOffset(0)] public WindowsInfo Windows;
                [FieldOffset(0)] public WinRTInfo WinRT;
                [FieldOffset(0)] public X11Info X11;
                [FieldOffset(0)] public DirectFBInfo DirectFB;
                [FieldOffset(0)] public CocoaInfo Cocoa;
                [FieldOffset(0)] public UIKitInfo UIKit;
                [FieldOffset(0)] public WaylandInfo Wayland;
                [FieldOffset(0)] public MirInfo Mir;
                [FieldOffset(0)] public AndroidInfo Android;
                [FieldOffset(0)] public VivanteInfo Vivante;
            }

            public Version Version;
            public SysWMType SubSystem;
            public InfoUnion Info;
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
            public byte Major;
            public byte Minor;
            public byte Patch;

            public const int MajorVersion = 2;
            public const int MinorVersion = 0;
            public const int PatchLevel = 6;

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
}