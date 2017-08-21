using System;

namespace SDL2
{
    //
    // SDL.h
    //
    [Flags]
    public enum InitFlags : uint
    {
        Timer = 0x00000001,
        Audio = 0x00000010,
        Video = 0x00000020,
        Joystick = 0x00000200,
        Haptic = 0x00001000,
        GameController = 0x00002000,
        Events = 0x00004000,
        NoParachute = 0x00100000,
        Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events
    }

    //
    // SDL_blendmode.h
    //
    [Flags]
    public enum BlendMode
    {
        None = 0,
        Blend = 1 << 0,
        Add = 1 << 1,
        Mod = 1 << 2
    }

    public enum BlendOperation
    {
        Add = 0x1,
        Subtract = 0x2,
        ReverseSubtract = 0x3,
        Minimum = 0x4,
        Maximum = 0x5
    }

    public enum BlendFactor
    {
        Zero = 0x1,
        One = 0x2,
        SrcColor = 0x3,
        OneMinusSrcColor = 0x4,
        SrcAlpha = 0x5,
        OneMinusSrcAlpha = 0x6,
        DstColor = 0x7,
        OneMinusDstColor = 0x8,
        DstAlpha = 0x9,
        OneMinusDstAlpha = 0xA,
    }

    //
    // SDL_events.h
    //
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

    public enum EventAction
    {
        AddEvent,
        PeekEvent,
        GetEvent
    }

    public enum State : int
    {
        Query = -1,
        Ignore = 0,
        Disable = 0,
        Enable = 1
    }

    //
    // SDL_gamecontroller.h
    //
    public enum GameControllerBindType
    {
        None = 0,
        Button,
        Axis,
        Hat
    }

    public enum GameControllerAxis
    {
        Invalid = -1,
        LeftX,
        LeftY,
        RightX,
        RightY,
        TriggertLeft,
        TriggerRight,
        Max
    }

    public enum GameControllerButton
    {
        Invalid = -1,
        A,
        B,
        X,
        Y,
        Back,
        Guide,
        Start,
        LeftStick,
        RightStick,
        LeftShoulder,
        RightShoulder,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        Max
    }

    //
    // SDL_hints.h
    //
    public enum HintPriority
    {
        Default,
        Normal,
        Override
    }

    //
    // SDL_joystick.h
    //
    public enum JoystickType
    {
        Unknown,
        GameController,
        Wheel,
        ArcadeStick,
        FlightStick,
        DancePad,
        Guitar,
        DrumKit,
        ArcadePad,
        Throttle
    }

    public enum JoystickPowerLevel
    {
        Unknown = -1,
        Empty,
        Low,
        Medium,
        Full,
        Wired,
        Max
    }

    public enum JoystickAxis
    {
        X = 0,
        Y = 1
    }

    public enum JoystickHat : byte
    {
        Centered = 0x00,
        Up = 0x01,
        Right = 0x02,
        Down = 0x04,
        Left = 0x08,
        RightUp = Right | Up,
        RightDown = Right | Down,
        LeftUp = Left | Up,
        LeftDown = Left | Down
    }

    //
    // SDL_keyboard.h
    //
    public enum ButtonState : byte
    {
        Released = 0,
        Pressed = 1
    }

    //
    // SDL_keycode.h
    //
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

    //
    // SDL_log.h
    //
    public enum LogCategory
    {
        Application,
        Error,
        Assert,
        System,
        Audio,
        Video,
        Render,
        Input,
        Test,

        Reserved1,
        Reserved2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,

        Custom,
    }

    public enum LogPriority
    {
        Verbose = 1,
        Debug,
        Info,
        Warn,
        Error,
        Critical,
    }

    //
    // SDL_messagebox.h
    //
    [Flags]
    public enum MessageBoxFlags : uint
    {
        Error = 1 << 4,
        Warning = 1 << 5,
        Information = 1 << 6
    }

    [Flags]
    public enum MessageBoxButtonFlags : uint
    {
        ReturnKeyDefault = 1 << 0,
        EscapeKeyDefault = 1 << 1
    }

    public enum MessageBoxColorType
    {
        Background,
        Text,
        ButtonBorder,
        ButtonBackground,
        ButtonSelected,
        Max
    }

    //
    // SDL_mouse.h
    //
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

    //
    // SDL_pixels.h
    //
    public enum PixelType : uint
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        ArrayU8,
        ArrayU16,
        ArrayU32,
        ArrayF16,
        ArrayF32
    }

    public enum PixelOrder : uint
    {
        BitmapNone = 0,
        Bitmap4321,
        Bitmap1234,

        PackedNone = 0,
        PackedXRGB,
        PackedRGBX,
        PackedARGB,
        PackedRGBA,
        PackedXBGR,
        PackedBGRX,
        PackedABGR,
        PackedBGRA,

        ArrayNone = 0,
        ArrayRGB,
        ArrayRGBA,
        ArrayARGB,
        ArrayBGR,
        ArrayBGRA,
        ArrayABGR
    }

    public enum PackedLayout : uint
    {
        None,
        Layout332,
        Layout4444,
        Layout1555,
        Layout5551,
        Layout565,
        Layout8888,
        Layout2101010,
        Layout1010102
    }

    //
    // SDL_power.h
    //
    public enum PowerState
    {
        Unknown,
        OnBattery,
        NoBattery,
        Charging,
        Charged
    }

    //
    // SDL_render.h
    // 
    [Flags]
    public enum RendererFlags
    {
        Software = 1 << 0,
        Accelerated = 1 << 1,
        PresentVSync = 1 << 2,
        TargetTexture = 1 << 3
    }

    public enum TextureAccess
    {
        Static,
        Streaming,
        Target
    }

    [Flags]
    public enum TextureModulate
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1
    }

    [Flags]
    public enum RendererFlip
    {
        None = 0,
        Horizontal = 1 << 0,
        Vertical = 1 << 1
    }

    //
    // SDL_scancode.h
    //
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

    //
    // SDL_shape.h
    //
    public enum WindowShapeMode
    {
        Default,
        BinarizeAlpha,
        ReverseBinarizeAlpha,
        ColorKey
    }

    //
    // SDL_surface.h
    //
    [Flags]
    public enum SurfaceFlags : uint
    {
        Software = 0,
        PreAllocated = 1 << 0,
        EncodedRLE = 1 << 1,
        DontFree = 1 << 2
    }

    //
    // SDL_syswm.h
    //
    public enum SysWMType
    {
        Unknown,
        Windows,
        X11,
        DirectFB,
        Cocoa,
        UIKit,
        Wayland,
        Mir,
        WinRT,
        Android,
        Vivante,
        OS2
    }

    //
    // SDL_video.h
    //
    [Flags]
    public enum WindowFlags : uint
    {
        Fullscreen = 1 << 0,
        OpenGL = 1 << 1,
        Shown = 1 << 2,
        Hidden = 1 << 3,
        Borderless = 1 << 4,
        Resizable = 1 << 5,
        Minimized = 1 << 6,
        Maximized = 1 << 7,
        InputGrabbed = 1 << 8,
        InputFocus = 1 << 9,
        MouseFocus = 1 << 10,
        FullscreenDeskTop = (Fullscreen | 1 << 12),
        Foreign = 1 << 11,
        AllowHighDPI = 1 << 13,
        MouseCapture = 1 << 14,
        AlwaysOnTop = 1 << 15,
        SkipTaskbar = 1 << 16,
        Utility = 1 << 17,
        Tooltip = 1 << 18,
        PopupMenu = 1 << 19,
        Vulkan = 1 << 20,
    }

    public enum WindowEventID
    {
        None,
        Shown,
        Hidden,
        Exposed,
        Moved,
        Resized,
        SizeChanged,
        Minimized,
        Maximized,
        Restored,
        Enter,
        Leave,
        FocusGained,
        FocusLost,
        Close,
        TakeFocus,
        HitTest
    }

    public enum HitTestResult
    {
        Normal,
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
}