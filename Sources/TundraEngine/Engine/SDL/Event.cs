using System;
using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public unsafe delegate int EventFilter(void* userdata, Event* sdlEvent);

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
    
    public enum ButtonState : byte
    {
        Released = 0,
        Pressed = 1
    }
    
    public enum EventType : uint
    {
        FirstEvent = 0,
        
        Quit = 0x100,
        
        AppTerminating,
        AppLowMemory,
        AppWillEnterBackground,
        AppDidEnterBackground,
        AppWillEnterForeground,
        AppDidEnterForeground,
        
        WindowEvent = 0x200,
        SysWMEvent,
        
        KeyDown = 0x300,
        KeyUp,
        TextEditing,
        TextInput,
        KeyMapChanged,
        
        MouseMotion = 0x400,
        MouseButtonDown,
        MouseButtonUp,
        MouseWheel,
        
        JoyAxisMotion = 0x600,
        JoyBallMotion,
        JoyHatMotion,
        JoyButtonDown,
        JoyButtonUp,
        JoyDeviceAdded,
        JoyDeviceRemoved,
        
        ControllerAxisMotion = 0x650,
        ControllerButtonDown,
        ControllerButtonUp,
        ControllerDeviceAdded,
        ControllerDeviceRemoved,
        ControllerDeviceMapped,
        
        FingerDown = 0x700,
        FingerUp,
        FingerMotion,
        
        DollarGesture = 0x800,
        DollarRecord,
        MultiGesture,
        
        ClipboardUpdate = 0x900,
        
        DropFile = 0x1000,
        DropText,
        DropBegin,
        DropComplete,
        
        AudioDeviceAdded = 0x1100,
        AudioDeviceRemoved,
        
        RenderTargetsReset = 0x2000,
        RenderDeviceReset,
        
        UserEvent = 0x8000,
        
        LastEvent = 0xFFFF
    }
    
    public enum ScanCode
    {
        Unknown = 0,
        
        A = 4,
        B = 5,
        C = 6,
        D = 7,
        E = 8,
        F = 9,
        G = 10,
        H = 11,
        I = 12,
        J = 13,
        K = 14,
        L = 15,
        M = 16,
        N = 17,
        O = 18,
        P = 19,
        Q = 20,
        R = 21,
        S = 22,
        T = 23,
        U = 24,
        V = 25,
        W = 26,
        X = 27,
        Y = 28,
        Z = 29,

        Alpha1 = 30,
        Alpha2 = 31,
        Alpha3 = 32,
        Alpha4 = 33,
        Alpha5 = 34,
        Alpha6 = 35,
        Alpha7 = 36,
        Alpha8 = 37,
        Alpha9 = 38,
        Alpha0 = 39,

        Return = 40,
        Escape = 41,
        Backspace = 42,
        Tab = 43,
        Space = 44,

        Minus = 45,
        Equals = 46,
        LeftBracket = 47,
        RightBracket = 48,
        Backslash = 49,
        NonUSHash = 50,
        Semicolon = 51,
        Apostrophe = 52,
        Grave = 53,
        Comma = 54,
        Period = 55,
        Slash = 56,

        Capslock = 57,

        F1 = 58,
        F2 = 59,
        F3 = 60,
        F4 = 61,
        F5 = 62,
        F6 = 63,
        F7 = 64,
        F8 = 65,
        F9 = 66,
        F10 = 67,
        F11 = 68,
        F12 = 69,

        Printscreen = 70,
        ScrollLock = 71,
        Pause = 72,
        Insert = 73,
        Home = 74,
        PageUp = 75,
        Delete = 76,
        End = 77,
        PageDown = 78,
        Right = 79,
        Left = 80,
        Down = 81,
        Up = 82,
        
        NumlockClear = 83,
        NumDivide = 84,
        NumMultiply = 85,
        NumMinus = 86,
        NumPlus = 87,
        NumEnter = 88,
        Num1 = 89,
        Num2 = 90,
        Num3 = 91,
        Num4 = 92,
        Num5 = 93,
        Num6 = 94,
        Num7 = 95,
        Num8 = 96,
        Num9 = 97,
        Num0 = 98,
        NumPeriod = 99,
        
        NonUSBackslash = 100,
        Application = 101,
        Power = 102,
        NumEquals = 103,
        F13 = 104,
        F14 = 105,
        F15 = 106,
        F16 = 107,
        F17 = 108,
        F18 = 109,
        F19 = 110,
        F20 = 111,
        F21 = 112,
        F22 = 113,
        F23 = 114,
        F24 = 115,
        Execute = 116,
        Help = 117,
        Menu = 118,
        Select = 119,
        Stop = 120,
        Again = 121,
        Undo = 122,
        Cut = 123,
        Copy = 124,
        Paste = 125,
        Find = 126,
        Mute = 127,
        VolumeUp = 128,
        VolumeDown = 129,
        NumComma = 133,
        NumEqualsAs400 = 134,
        
        International1 = 135,
        International2 = 136,
        International3 = 137,
        International4 = 138,
        International5 = 139,
        International6 = 140,
        International7 = 141,
        International8 = 142,
        International9 = 143,
        Lang1 = 144,
        Lang2 = 145,
        Lang3 = 146,
        Lang4 = 147,
        Lang5 = 148,
        Lang6 = 149,
        Lang7 = 150,
        Lang8 = 151,
        Lang9 = 152,

        AltErase = 153,
        SysReq = 154,
        Cancel = 155,
        Clear = 156,
        Prior = 157,
        Return2 = 158,
        Separator = 159,
        Out = 160,
        Oper = 161,
        ClearAgain = 162,
        CRSel = 163,
        EXSel = 164,

        Num00 = 176,
        Num000 = 177,
        ThousandsSeparator = 178,
        DecimalSeparator = 179,
        CurrencyUnit = 180,
        CurrencySubUnit = 181,
        NumLeftParen = 182,
        NumRightParen = 183,
        NumLeftBrace = 184,
        NumRightBrace = 185,
        NumTab = 186,
        NumBackspace = 187,
        NumA = 188,
        NumB = 189,
        NumC = 190,
        NumD = 191,
        NumE = 192,
        NumF = 193,
        NumXor = 194,
        NumPower = 195,
        NumPercent = 196,
        NumLess = 197,
        NumGreater = 198,
        NumAmpersand = 199,
        NumDoubleAmpersand = 200,
        NumVerticalBar = 201,
        NumDoubleVerticalBar = 202,
        NumColon = 203,
        NumHash = 204,
        NumSpace = 205,
        NumAt = 206,
        NumExclam = 207,
        NumMemStore = 208,
        NumMemRecall = 209,
        NumMemClear = 210,
        NumMemAdd = 211,
        NumMemSubtract = 212,
        NumMemMultiply = 213,
        NumMemDivide = 214,
        NumPlusMinus = 215,
        NumClear = 216,
        NumClearEntry = 217,
        NumBinary = 218,
        NumOctal = 219,
        NumDecimal = 220,
        NumHexadecimal = 221,

        LeftControl = 224,
        LeftShift = 225,
        LeftAlt = 226,
        LeftGUI = 227,
        RightControl = 228,
        RightShift = 229,
        RightAlt = 230,
        RightGUI = 231,
        
        Mode = 257,
        
        AudioNext = 258,
        AudioPrevious = 259,
        AudioStop = 260,
        AudioPlay = 261,
        AudioMute = 262,
        MediaSelect = 263,
        WWW = 264,
        Mail = 265,
        Calculator = 266,
        Computer = 267,
        ACSearch = 268,
        ACHome = 269,
        ACBack = 270,
        ACForward = 271,
        ACStop = 272,
        ACRefresh = 273,
        ACBookmark = 274,
        
        BrightnessDown = 275,
        BrightnessUp = 276,
        DisplaySwitch = 277,
        KeyboardLightToggle = 278,
        KeyboardLightDown = 279,
        KeyboardLightUp = 280,
        Eject = 281,
        Sleep = 282,

        App1 = 283,
        App2 = 284,
        
        NumScancodes = 512
    }
    
    public enum KeyCode
    {
        Unknown = 0,

        Return = '\r',
        Escape = 27, // '\033'
        Backspace = '\b',
        Tab = '\t',
        Space = ' ',
        Exclaim = '!',
        QuoteDouble = '"',
        Hash = '#',
        Percent = '%',
        Dollar = '$',
        Ampersand = '&',
        Quote = '\'',
        LeftParenthesis = '(',
        RightParenthesis = ')',
        Asterisk = '*',
        Plus = '+',
        Comma = ',',
        Minus = '-',
        Period = '.',
        Slash = '/',
        Alpha0 = '0',
        Alpha1 = '1',
        Alpha2 = '2',
        Alpha3 = '3',
        Alpha4 = '4',
        Alpha5 = '5',
        Alpha6 = '6',
        Alpha7 = '7',
        Alpha8 = '8',
        Alpha9 = '9',
        Colon = ':',
        Semicolon = ';',
        Less = '<',
        Equals = '=',
        Greater = '>',
        Question = '?',
        At = '@',

        // Skip uppercase letters

        LeftBracket = '[',
        Backslash = '\\',
        RightBracket = ']',
        Caret = '^',
        Underscore = '_',
        Backquote = '`',
        a = 'a',
        b = 'b',
        c = 'c',
        d = 'd',
        e = 'e',
        f = 'f',
        g = 'g',
        h = 'h',
        i = 'i',
        j = 'j',
        k = 'k',
        l = 'l',
        m = 'm',
        n = 'n',
        o = 'o',
        p = 'p',
        q = 'q',
        r = 'r',
        s = 's',
        t = 't',
        u = 'u',
        v = 'v',
        w = 'w',
        x = 'x',
        y = 'y',
        z = 'z',

        Capslock = ScanCode.Capslock | SDL.ScanCodeMask,

        F1 = ScanCode.F1 | SDL.ScanCodeMask,
        F2 = ScanCode.F2 | SDL.ScanCodeMask,
        F3 = ScanCode.F3 | SDL.ScanCodeMask,
        F4 = ScanCode.F4 | SDL.ScanCodeMask,
        F5 = ScanCode.F5 | SDL.ScanCodeMask,
        F6 = ScanCode.F6 | SDL.ScanCodeMask,
        F7 = ScanCode.F7 | SDL.ScanCodeMask,
        F8 = ScanCode.F8 | SDL.ScanCodeMask,
        F9 = ScanCode.F9 | SDL.ScanCodeMask,
        F10 = ScanCode.F10 | SDL.ScanCodeMask,
        F11 = ScanCode.F11 | SDL.ScanCodeMask,
        F12 = ScanCode.F12 | SDL.ScanCodeMask,

        Printscreen = ScanCode.Printscreen | SDL.ScanCodeMask,
        ScrollLock = ScanCode.ScrollLock | SDL.ScanCodeMask,
        Pause = ScanCode.Pause | SDL.ScanCodeMask,
        Insert = ScanCode.Insert | SDL.ScanCodeMask,
        Home = ScanCode.Home | SDL.ScanCodeMask,
        PageUp = ScanCode.PageUp | SDL.ScanCodeMask,
        Delete = 127, // '\177',
        End = ScanCode.End | SDL.ScanCodeMask,
        PageDown = ScanCode.PageDown | SDL.ScanCodeMask,
        Right = ScanCode.Right | SDL.ScanCodeMask,
        Left = ScanCode.Left | SDL.ScanCodeMask,
        Down = ScanCode.Down | SDL.ScanCodeMask,
        Up = ScanCode.Up | SDL.ScanCodeMask,

        NumlockClear = ScanCode.NumlockClear | SDL.ScanCodeMask,
        NumDivide = ScanCode.NumDivide | SDL.ScanCodeMask,
        NumMultiply = ScanCode.NumMultiply | SDL.ScanCodeMask,
        NumMinus = ScanCode.NumMinus | SDL.ScanCodeMask,
        NumPlus = ScanCode.NumPlus | SDL.ScanCodeMask,
        NumEnter = ScanCode.NumEnter | SDL.ScanCodeMask,
        Num1 = ScanCode.Num1 | SDL.ScanCodeMask,
        Num2 = ScanCode.Num2 | SDL.ScanCodeMask,
        Num3 = ScanCode.Num3 | SDL.ScanCodeMask,
        Num4 = ScanCode.Num4 | SDL.ScanCodeMask,
        Num5 = ScanCode.Num5 | SDL.ScanCodeMask,
        Num6 = ScanCode.Num6 | SDL.ScanCodeMask,
        Num7 = ScanCode.Num7 | SDL.ScanCodeMask,
        Num8 = ScanCode.Num8 | SDL.ScanCodeMask,
        Num9 = ScanCode.Num9 | SDL.ScanCodeMask,
        Num0 = ScanCode.Num0 | SDL.ScanCodeMask,
        NumPeriod = ScanCode.NumPeriod | SDL.ScanCodeMask,

        Application = ScanCode.Application | SDL.ScanCodeMask,
        Power = ScanCode.Power | SDL.ScanCodeMask,
        NumEquals = ScanCode.NumEquals | SDL.ScanCodeMask,
        F13 = ScanCode.F13 | SDL.ScanCodeMask,
        F14 = ScanCode.F14 | SDL.ScanCodeMask,
        F15 = ScanCode.F15 | SDL.ScanCodeMask,
        F16 = ScanCode.F16 | SDL.ScanCodeMask,
        F17 = ScanCode.F17 | SDL.ScanCodeMask,
        F18 = ScanCode.F18 | SDL.ScanCodeMask,
        F19 = ScanCode.F19 | SDL.ScanCodeMask,
        F20 = ScanCode.F20 | SDL.ScanCodeMask,
        F21 = ScanCode.F21 | SDL.ScanCodeMask,
        F22 = ScanCode.F22 | SDL.ScanCodeMask,
        F23 = ScanCode.F23 | SDL.ScanCodeMask,
        F24 = ScanCode.F24 | SDL.ScanCodeMask,
        Execute = ScanCode.Execute | SDL.ScanCodeMask,
        Help = ScanCode.Help | SDL.ScanCodeMask,
        Menu = ScanCode.Menu | SDL.ScanCodeMask,
        Select = ScanCode.Select | SDL.ScanCodeMask,
        Stop = ScanCode.Stop | SDL.ScanCodeMask,
        Again = ScanCode.Again | SDL.ScanCodeMask,
        Undo = ScanCode.Undo | SDL.ScanCodeMask,
        Cut = ScanCode.Cut | SDL.ScanCodeMask,
        Copy = ScanCode.Copy | SDL.ScanCodeMask,
        Paste = ScanCode.Paste | SDL.ScanCodeMask,
        Find = ScanCode.Find | SDL.ScanCodeMask,
        Mute = ScanCode.Mute | SDL.ScanCodeMask,
        VolumeUp = ScanCode.VolumeUp | SDL.ScanCodeMask,
        VolumeDown = ScanCode.VolumeDown | SDL.ScanCodeMask,
        NumComma = ScanCode.NumComma | SDL.ScanCodeMask,
        NumEqualsAs400 = ScanCode.NumEqualsAs400 | SDL.ScanCodeMask,

        AltErase = ScanCode.AltErase | SDL.ScanCodeMask,
        SysReq = ScanCode.SysReq | SDL.ScanCodeMask,
        Cancel = ScanCode.Cancel | SDL.ScanCodeMask,
        Clear = ScanCode.Clear | SDL.ScanCodeMask,
        Prior = ScanCode.Prior | SDL.ScanCodeMask,
        Return2 = ScanCode.Return2 | SDL.ScanCodeMask,
        Separator = ScanCode.Separator | SDL.ScanCodeMask,
        Out = ScanCode.Out | SDL.ScanCodeMask,
        Oper = ScanCode.Oper | SDL.ScanCodeMask,
        ClearAgain = ScanCode.ClearAgain | SDL.ScanCodeMask,
        CRSel = ScanCode.CRSel | SDL.ScanCodeMask,
        EXSel = ScanCode.EXSel | SDL.ScanCodeMask,

        Num00 = ScanCode.Num00 | SDL.ScanCodeMask,
        Num000 = ScanCode.Num000 | SDL.ScanCodeMask,
        ThousandsSeparator = ScanCode.ThousandsSeparator | SDL.ScanCodeMask,
        DecimalSeparator = ScanCode.DecimalSeparator | SDL.ScanCodeMask,
        CurrencyUnit = ScanCode.CurrencyUnit | SDL.ScanCodeMask,
        CurrencySubUnit = ScanCode.CurrencySubUnit | SDL.ScanCodeMask,
        NumLeftParen = ScanCode.NumLeftParen | SDL.ScanCodeMask,
        NumRightParen = ScanCode.NumRightParen | SDL.ScanCodeMask,
        NumLeftBrace = ScanCode.NumLeftBrace | SDL.ScanCodeMask,
        NumRightBrace = ScanCode.NumRightBrace | SDL.ScanCodeMask,
        NumTab = ScanCode.NumTab | SDL.ScanCodeMask,
        NumBackspace = ScanCode.NumBackspace | SDL.ScanCodeMask,
        NumA = ScanCode.NumA | SDL.ScanCodeMask,
        NumB = ScanCode.NumB | SDL.ScanCodeMask,
        NumC = ScanCode.NumC | SDL.ScanCodeMask,
        NumD = ScanCode.NumD | SDL.ScanCodeMask,
        NumE = ScanCode.NumE | SDL.ScanCodeMask,
        NumF = ScanCode.NumF | SDL.ScanCodeMask,
        NumXor = ScanCode.NumXor | SDL.ScanCodeMask,
        NumPower = ScanCode.NumPower | SDL.ScanCodeMask,
        NumPercent = ScanCode.NumPercent | SDL.ScanCodeMask,
        NumLess = ScanCode.NumLess | SDL.ScanCodeMask,
        NumGreater = ScanCode.NumGreater | SDL.ScanCodeMask,
        NumAmpersand = ScanCode.NumAmpersand | SDL.ScanCodeMask,
        NumDoubleAmpersand = ScanCode.NumDoubleAmpersand | SDL.ScanCodeMask,
        NumVerticalBar = ScanCode.NumVerticalBar | SDL.ScanCodeMask,
        NumDoubleVerticalBar = ScanCode.NumDoubleVerticalBar | SDL.ScanCodeMask,
        NumColon = ScanCode.NumColon | SDL.ScanCodeMask,
        NumHash = ScanCode.NumHash | SDL.ScanCodeMask,
        NumSpace = ScanCode.NumSpace | SDL.ScanCodeMask,
        NumAt = ScanCode.NumAt | SDL.ScanCodeMask,
        NumExclam = ScanCode.NumExclam | SDL.ScanCodeMask,
        NumMemStore = ScanCode.NumMemStore | SDL.ScanCodeMask,
        NumMemRecall = ScanCode.NumMemRecall | SDL.ScanCodeMask,
        NumMemClear = ScanCode.NumMemClear | SDL.ScanCodeMask,
        NumMemAdd = ScanCode.NumMemAdd | SDL.ScanCodeMask,
        NumMemSubtract = ScanCode.NumMemSubtract | SDL.ScanCodeMask,
        NumMemMultiply = ScanCode.NumMemMultiply | SDL.ScanCodeMask,
        NumMemDivide = ScanCode.NumMemDivide | SDL.ScanCodeMask,
        NumPlusMinus = ScanCode.NumPlusMinus | SDL.ScanCodeMask,
        NumClear = ScanCode.NumClear | SDL.ScanCodeMask,
        NumClearEntry = ScanCode.NumClearEntry | SDL.ScanCodeMask,
        NumBinary = ScanCode.NumBinary | SDL.ScanCodeMask,
        NumOctal = ScanCode.NumOctal | SDL.ScanCodeMask,
        NumDecimal = ScanCode.NumDecimal | SDL.ScanCodeMask,
        NumHexadecimal = ScanCode.NumHexadecimal | SDL.ScanCodeMask,

        LeftControl = ScanCode.LeftControl | SDL.ScanCodeMask,
        LeftShift = ScanCode.LeftShift | SDL.ScanCodeMask,
        LeftAlt = ScanCode.LeftAlt | SDL.ScanCodeMask,
        LeftGUI = ScanCode.LeftGUI | SDL.ScanCodeMask,
        RightControl = ScanCode.RightControl | SDL.ScanCodeMask,
        RightShift = ScanCode.RightShift | SDL.ScanCodeMask,
        RightAlt = ScanCode.RightAlt | SDL.ScanCodeMask,
        RightGUI = ScanCode.RightGUI | SDL.ScanCodeMask,

        Mode = ScanCode.Mode | SDL.ScanCodeMask,

        AudioNext = ScanCode.AudioNext | SDL.ScanCodeMask,
        AudioPrevious = ScanCode.AudioPrevious | SDL.ScanCodeMask,
        AudioStop = ScanCode.AudioStop | SDL.ScanCodeMask,
        AudioPlay = ScanCode.AudioPlay | SDL.ScanCodeMask,
        AudioMute = ScanCode.AudioMute | SDL.ScanCodeMask,
        MediaSelect = ScanCode.MediaSelect | SDL.ScanCodeMask,
        WWW = ScanCode.WWW | SDL.ScanCodeMask,
        Mail = ScanCode.Mail | SDL.ScanCodeMask,
        Calculator = ScanCode.Calculator | SDL.ScanCodeMask,
        Computer = ScanCode.Computer | SDL.ScanCodeMask,
        ACSearch = ScanCode.ACSearch | SDL.ScanCodeMask,
        ACHome = ScanCode.ACHome | SDL.ScanCodeMask,
        ACBack = ScanCode.ACBack | SDL.ScanCodeMask,
        ACForward = ScanCode.ACForward | SDL.ScanCodeMask,
        ACStop = ScanCode.ACStop | SDL.ScanCodeMask,
        ACRefresh = ScanCode.ACRefresh | SDL.ScanCodeMask,
        ACBookmark = ScanCode.ACBookmark | SDL.ScanCodeMask,

        BrightnessDown = ScanCode.BrightnessDown | SDL.ScanCodeMask,
        BrightnessUp = ScanCode.BrightnessUp | SDL.ScanCodeMask,
        DisplaySwitch = ScanCode.DisplaySwitch | SDL.ScanCodeMask,
        KeyboardLightToggle = ScanCode.KeyboardLightToggle | SDL.ScanCodeMask,
        KeyboardLightDown = ScanCode.KeyboardLightDown | SDL.ScanCodeMask,
        KeyboardLightUp = ScanCode.KeyboardLightUp | SDL.ScanCodeMask,
        Eject = ScanCode.Eject | SDL.ScanCodeMask,
        Sleep = ScanCode.Sleep | SDL.ScanCodeMask
    }
    
    public enum KeyMod : ushort
    {
        None = 0x0000,
        LeftShift = 0x0001,
        RightShift = 0x0002,
        LeftControl = 0x0040,
        RightControl = 0x0080,
        LeftAlt = 0x0100,
        RightAlt = 0x0200,
        LeftGUI = 0x0400,
        RightGUI = 0x0800,
        Num = 0x1000,
        Caps = 0x2000,
        Mode = 0x4000,
        Reserved = 0x8000,
        Control = LeftControl | RightControl,
        Shift = LeftShift | RightShift,
        Alt = LeftAlt | RightAlt,
        GUI = LeftGUI | RightGUI
    }

    public enum MouseButton : byte
    {
        Left = 1,
        Middle = 2,
        Right = 3,
        X1 = 4,
        X2 = 5,
    }

    [Flags]
    public enum MouseButtonState : uint
    {
        Left = 1 << 0,
        Middle = 1 << 1,
        Right = 1 << 2,
        X1 = 1 << 3,
        X2 = 1 << 4
    }

    public enum SystemCursor
    {
        Arrow,
        IBeam,
        Wait,
        Crosshair,
        WaitArrow,
        SizeNWSE,
        SizeNESW,
        SizeWE,
        SizeNS,
        SizeAll,
        No,
        Hand,
        Count
    }

    public enum MouseWheelDirection
    {
        Normal,
        Flipped
    }
    
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
}