using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace Engine.Input
{
    public enum InputEventType : byte
    {
        None,
        Button,
        Axis,
        MouseMove,
        Text
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputEvent
    {
        [FieldOffset(0)] public InputEventType Type;
        [FieldOffset(1)] public ButtonEvent ButtonEvent;
        [FieldOffset(1)] public AxisEvent AxisEvent;
        [FieldOffset(1)] public MouseMoveEvent MouseMoveEvent;
        [FieldOffset(1)] public TextEvent TextEvent;
    }

    public enum Button : byte
    {
        // Keyboard
        A,
        B,
        C,
        D,
        E,
        F,
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O,
        P,
        Q,
        R,
        S,
        T,
        U,
        V,
        W,
        X,
        Y,
        Z,
        Alpha0,
        Alpha1,
        Alpha2,
        Alpha3,
        Alpha4,
        Alpha5,
        Alpha6,
        Alpha7,
        Alpha8,
        Alpha9,
        Keypad0,
        Keypad1,
        Keypad2,
        Keypad3,
        Keypad4,
        Keypad5,
        Keypad6,
        Keypad7,
        Keypad8,
        Keypad9,
        KeypadPeriod,
        KeypadDivide,
        KeypadMultiply,
        KeypadMinus,
        KeypadPlus,
        KeypadEnter,
        KeypadEquals,
        Backspace,
        Tab,
        Clear,
        Return,
        Pause,
        Escape,
        Space,
        Exclaim,
        DoubleQuote,
        Hash,
        Dollar,
        Ampersand,
        Quote,
        LeftParen,
        RightParen,
        Asterisk,
        Plus,
        Comma,
        Minus,
        Period,
        Slash,
        Colon,
        Semicolon,
        Less,
        Equals,
        Greater,
        Question,
        At,
        LeftBracket,
        RightBracket,
        Backslash,
        Caret,
        Underscore,
        BackQuote,
        Delete,
        UpArrow,
        DownArrow,
        LeftArrow,
        RightArrow,
        Insert,
        Home,
        End,
        PageUp,
        PageDown,
        F1,
        F2,
        F3,
        F4,
        F5,
        F6,
        F7,
        F8,
        F9,
        F10,
        F11,
        F12,
        F13,
        F14,
        F15,
        NumLock,
        CapsLock,
        ScrollLock,
        RightShift,
        LeftShift,
        RightControl,
        LeftControl,
        RightAlt,
        LeftAlt,
        RightWindowsCommand,
        LeftWindowsCommand,
        AltGr,
        Help,
        Print,
        SysReq,
        Break,
        Menu,

        NumKeys,

        // Mouse
        MouseLeft = NumKeys,
        MouseLeftDouble,
        MouseMiddle,
        MouseMiddleDouble,
        MouseRight,
        MouseRightDouble,
        MouseExtra1,
        MouseExtra2,
        MouseWheelUp,
        MouseWheelDown,
        MouseWheelLeft,
        MouseWheelRight,

        // Gamepad
        GamepadUp,
        GamepadDown,
        GamepadLeft,
        GamepadRight,
        GamepadStart,
        GamepadBack,
        GamepadGuide,
        GamepadThumbLeft,
        GamepadThumbRight,
        GamepadTriggerLeft,
        GamepadTriggerRight,
        GamepadA,
        GamepadY,
        GamepadB,
        GamepadX,

        NumButtons
    }

    public enum ButtonState : byte
    {
        Released = 0,
        Pressed = 1
    }

    [Flags]
    public enum ModifierKeys : byte
    {
        None = 0,
        LeftShift = 1 << 0,
        RightShift = 1 << 1,
        LeftControl = 1 << 2,
        RightControl = 1 << 3,
        LeftAlt = 1 << 4,
        RightAlt = 1 << 5,
        LeftCommand = 1 << 6,
        RightCommand = 1 << 7,
        Control = LeftControl | RightControl,
        Shift = LeftShift | RightShift,
        Alt = LeftAlt | RightAlt,
        Command = LeftCommand | RightCommand
    }

    public struct ButtonEvent
    {
        public Button Button;
        public ButtonState State;
        public byte PlayerId;
    }

    public enum Axis : byte
    {
        // Mouse
        MouseX,
        MouseY,
        MouseWheel,

        // Gamepad
        GamepadLeftStickX,
        GamepadLeftStickY,
        GamepadRightStickX,
        GamepadRightStickY,
        GamepadLeftTrigger,
        GamepadRightTrigger,

        NumAxis
    }

    public struct AxisEvent
    {
        public Axis Axis;
        public Vector2 Value;
        public byte PlayerId;
    }

    public struct MouseMoveEvent
    {
        public Vector2 Position;
        public Vector2 DeltaPosition;
        public TimeSpan DeltaTime;
        public byte PlayerId;
    }

    public struct TextEvent
    {
        public string Text;
        public byte PlayerId;
    }
}