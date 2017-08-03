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

    /// <summary>
    /// General keyboard/mouse state definitions
    /// </summary>
    public enum ButtonState : byte
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
        /// This last event is only for bounding Native arrays
        /// </summary>
        LastEvent = 0xFFFF
    }

    /// <summary>
    /// The SDL keyboard scancode representation.
    /// <para /> Values of this type are used to represent keyboard keys, among other places in the <see cref="KeySym.ScanCode"/> field of the <see cref="Event"/> structure.
    /// <para /> The values in this enumeration are based on the USB usage page standard: http://www.usb.org/developers/devclass_docs/Hut1_12v2.pdf
    /// </summary>
    public enum ScanCode
    {
        Unknown = 0,

        /**
         *  \name Usage page 0x07
         *
         *  These values are from usage page 0x07 (USB keyboard page).
         */
        /* @{ */

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
        /// <summary>
        /// Located at the lower left of the return key on ISO keyboards and at the right end of the QWERTY row on ANSI keyboards. Produces REVERSE SOLIDUS (backslash) and VERTICAL LINE in a US layout, REVERSE SOLIDUS and VERTICAL LINE in a UK Mac layout, NUMBER SIGN and TILDE in a UK Windows layout, DOLLAR SIGN and POUND SIGN in a Swiss German layout, NUMBER SIGN and APOSTROPHE in a German layout, GRAVE ACCENT and POUND SIGN in a French Mac layout, and ASTERISK and MICRO SIGN in a French Windows layout.
        /// </summary>
        Backslash = 49,
        /// <summary>
        /// ISO USB keyboards actually use this code instead of 49 for the same key, but all OSes I've seen treat the two codes identically. So, as an implementor, unless your keyboard generates both of those codes and your OS treats them differently, you should generate BACKSLASH instead of this code. As a user, you should not rely on this code because SDL will never generate it with most (all?) keyboards.
        /// </summary>
        NonUSHash = 50,
        Semicolon = 51,
        Apostrophe = 52,
        /// <summary>
        /// Located in the top left corner (on both ANSI and ISO keyboards). Produces GRAVE ACCENT and TILDE in a US Windows layout and in US and UK Mac layouts on ANSI keyboards, GRAVE ACCENT and NOT SIGN in a UK Windows layout, SECTION SIGN and PLUS-MINUS SIGN in US and UK Mac layouts on ISO keyboards, SECTION SIGN and DEGREE SIGN in a Swiss German layout (Mac: only on ISO keyboards), CIRCUMFLEX ACCENT and DEGREE SIGN in a German layout (Mac: only on ISO keyboards), SUPERSCRIPT TWO and TILDE in a French Windows layout, COMMERCIAL AT and NUMBER SIGN in a French Mac layout on ISO keyboards, and LESS-THAN SIGN and GREATER-THAN SIGN in a Swiss German, German, or French Mac layout on ANSI keyboards.
        /// </summary>
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
        /// <summary>
        /// insert on PC, help on some Mac keyboards (but does send code 73, not 117)
        /// </summary>
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

        /// <summary>
        /// Num lock on PC, clear on Mac keyboards
        /// </summary>
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

        /// <summary>
        /// This is the additional key that ISO keyboards have over ANSI ones, located between left shift and Y. Produces GRAVE ACCENT and TILDE in a US or UK Mac layout, REVERSE SOLIDUS (backslash) and VERTICAL LINE in a US or UK Windows layout, and LESS-THAN SIGN and GREATER-THAN SIGN in a Swiss German, German, or French layout.
        /// </summary>
        NonUSBackslash = 100,
        /// <summary>
        /// Windows contextual menu, compose
        /// </summary>
        Application = 101,
        /// <summary>
        /// The USB document says this is a status flag, not a physical key - but some Mac keyboards do have a power key.
        /// </summary>
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
        /// <summary>
        /// Redo
        /// </summary>
        Again = 121,
        Undo = 122,
        Cut = 123,
        Copy = 124,
        Paste = 125,
        Find = 126,
        Mute = 127,
        VolumeUp = 128,
        VolumeDown = 129,
        /* not sure whether there's a reason to enable these */
        /*     LOCKINGCAPSLOCK = 130,  */
        /*     LOCKINGNUMLOCK = 131, */
        /*     LOCKINGSCROLLLOCK = 132, */
        NumComma = 133,
        NumEqualsAs400 = 134,

        /// <summary>
        /// Used on Asian keyboards, see footnotes in USB doc
        /// </summary>
        International1 = 135,
        International2 = 136,
        /// <summary>
        /// Yen
        /// </summary>
        International3 = 137,
        International4 = 138,
        International5 = 139,
        International6 = 140,
        International7 = 141,
        International8 = 142,
        International9 = 143,
        /// <summary>
        /// Hangul/English toggle
        /// </summary>
        Lang1 = 144,
        /// <summary>
        /// Hanja conversion
        /// </summary>
        Lang2 = 145,
        /// <summary>
        /// Katakana
        /// </summary>
        Lang3 = 146,
        /// <summary>
        /// Hiragana
        /// </summary>
        Lang4 = 147,
        /// <summary>
        /// Zenkaku/Hankak
        /// </summary>
        Lang5 = 148,
        /// <summary>
        /// Reserved
        /// </summary>
        Lang6 = 149,
        /// <summary>
        /// Reserved
        /// </summary>
        Lang7 = 150,
        /// <summary>
        /// Reserved
        /// </summary>
        Lang8 = 151,
        /// <summary>
        /// Reserved
        /// </summary>
        Lang9 = 152,

        AltErase = 153, /**< Erase-Eaze */
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
        /// <summary>
        /// alt, option
        /// </summary>
        LeftAlt = 226,
        /// <summary>
        /// windows, command (apple), meta
        /// </summary>
        LeftGUI = 227,
        RightControl = 228,
        RightShift = 229,
        /// <summary>
        /// alt gr, option
        /// </summary>
        RightAlt = 230,
        /// <summary>
        /// windows, command (apple), meta
        /// </summary>
        RightGUI = 231,

        /// <summary>
        /// I'm not sure if this is really not covered by any of the above, but since there's a special KMOD_MODE for it I'm adding it here
        /// </summary>
        Mode = 257,

        /* @} *//* Usage page 0x07 */

        /**
         *  \name Usage page 0x0C
         *
         *  These values are mapped from usage page 0x0C (USB consumer page).
         */
        /* @{ */

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

        /* @} *//* Usage page 0x0C */

        /**
         *  \name Walther keys
         *
         *  These are values that Christian Walther added (for mac keyboard?).
         */
        /* @{ */

        BrightnessDown = 275,
        BrightnessUp = 276,
        /// <summary>
        /// Display mirroring/dual display switch, video mode switch
        /// </summary>
        DisplaySwitch = 277,
        KeyboardLightToggle = 278,
        KeyboardLightDown = 279,
        KeyboardLightUp = 280,
        Eject = 281,
        Sleep = 282,

        App1 = 283,
        App2 = 284,

        /* @} *//* Walther keys */

        /* Add any other keys here. */

        /// <summary>
        /// Not a key, just marks the number of scancodes for array bounds
        /// </summary>
        NumScancodes = 512
    }
    
    /// <summary>
    /// The SDL virtual key representation.
    /// <para /> Values of this type are used to represent keyboard keys using the current layout of the keyboard. These values include Unicode values representing the unmodified character that would be generated by pressing the key, or an SDLK_* constant for those keys that do not generate characters.
    /// </summary>
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

    /// <summary>
    /// Enumeration of valid key mods (possibly OR'd together).
    /// </summary>
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

    /// <summary>
    /// Fields shared by every event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct CommonEvent
    {
        public EventType Type;
        public uint TimeStamp;
    }

    /// <summary>
    /// Window state change event data (event.window.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct WindowEvent
    {
        /// <summary>
        /// WINDOWEVENT
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The associated window
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// WindowEventID
        /// </summary>
        public byte Event;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        /// <summary>
        /// Event dependent data
        /// </summary>
        public int Data1;
        /// <summary>
        /// Event dependent data
        /// </summary>
        public int Data2;
    }

    /// <summary>
    /// Keyboard button event structure (event.key.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        /// <summary>
        /// KEYDOWN or KEYUP
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public ButtonState State;
        /// <summary>
        /// Non-zero if this is a key repeat
        /// </summary>
        public byte Repeat;
        private byte Padding2;
        private byte Padding3;
        /// <summary>
        /// The key that was pressed or released
        /// </summary>
        public KeySym KeySym;
    }

    /// <summary>
    /// Keyboard text editing event structure (event.edit.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct TextEditingEvent
    {
        public const int TextEditingEventTextSize = 32;

        /// <summary>
        /// TEXTEDITING
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// The editing text
        /// </summary>
        public fixed byte Text[TextEditingEventTextSize];
        /// <summary>
        /// The start cursor of selected editing text
        /// </summary>
        public int Start;
        /// <summary>
        /// The length of selected editing text
        /// </summary>
        public int Length;
    }

    /// <summary>
    /// Keyboard text input event structure (event.text.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct TextInputEvent
    {
        public const int TextInputEventTextSize = 32;

        /// <summary>
        /// TEXTINPUT
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with keyboard focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// The input text
        /// </summary>
        public fixed byte Text[TextInputEventTextSize];
    }

    /// <summary>
    /// Mouse motion event structure (event.motion.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        /// <summary>
        /// MOUSEMOTION
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public uint Which;
        /// <summary>
        /// The current button state
        /// </summary>
        public uint State;
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public int X;
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public int Y;
        /// <summary>
        /// The relative motion in the X direction
        /// </summary>
        public int Xrel;
        /// <summary>
        /// The relative motion in the Y direction
        /// </summary>
        public int Yrel;
    }

    /// <summary>
    /// Mouse button event structure (event.button.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        /// <summary>
        /// MOUSEBUTTONDOWN or MOUSEBUTTONUP
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public uint Which;
        /// <summary>
        /// The mouse button index
        /// </summary>
        public MouseButton Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public ButtonState State;
        /// <summary>
        /// 1 for single-click, 2 for double-click, etc.
        /// </summary>
        public byte Clicks;
        private byte Padding1;
        /// <summary>
        /// X coordinate, relative to window
        /// </summary>
        public int X;
        /// <summary>
        /// Y coordinate, relative to window
        /// </summary>
        public int Y;
    }

    /// <summary>
    /// Mouse wheel event structure (event.wheel.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        /// <summary>
        /// MOUSEWHEEL
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The window with mouse focus, if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// The mouse instance id, or TOUCH_MOUSEID
        /// </summary>
        public uint Which;
        /// <summary>
        /// The amount scrolled horizontally, positive to the right and negative to the left
        /// </summary>
        public int X;
        /// <summary>
        /// The amount scrolled vertically, positive away from the user and negative toward the user
        /// </summary>
        public int Y;
        /// <summary>
        /// Set to one of the MOUSEWHEEL_* defines. When FLIPPED the values in X and Y will be opposite. Multiply by -1 to change them back
        /// </summary>
        public uint Direction;
    }

    /// <summary>
    /// Joystick axis motion event structure (event.jaxis.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        /// <summary>
        /// JOYAXISMOTION
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The joystick axis index
        /// </summary>
        public byte Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public short Value;
        public ushort Padding4;
    }

    /// <summary>
    /// Joystick trackball motion event structure (event.jball.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        /// <summary>
        /// JOYBALLMOTION
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The joystick trackball index
        /// </summary>
        public byte Ball;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        /// <summary>
        /// The relative motion in the X direction
        /// </summary>
        public short Xrel;
        /// <summary>
        /// The relative motion in the Y direction
        /// </summary>
        public short Yrel;
    }

    /// <summary>
    /// Joystick hat position change event structure (event.jhat.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        /// <summary>
        /// JOYHATMOTION
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The joystick hat index
        /// </summary>
        public byte Hat;
        /// <summary>
        /// The hat position value. 
        /// <para /> HAT_LEFTUP HAT_UP HAT_RIGHTUP
        /// <para /> HAT_LEFT HAT_CENTERED HAT_RIGHT
        /// <para /> HAT_LEFTDOWN HAT_DOWN HAT_RIGHTDOWN
        /// </summary>
        /// <remarks> Note that zero means the POV is centered. </remarks>
        public byte Value;
        private byte Padding1;
        private byte Padding2;
    }

    /// <summary>
    /// Joystick button event structure (event.jbutton.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        /// <summary>
        /// JOYBUTTONDOWN or JOYBUTTONUP
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The joystick button index
        /// </summary>
        public byte Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public byte State;
        private byte Padding1;
        private byte Padding2;
    }

    /// <summary>
    /// Joystick device event structure (event.jdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        /// <summary>
        /// JOYDEVICEADDED or JOYDEVICEREMOVED
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick device index for the ADDED event, instance id for the REMOVED event
        /// </summary>
        public int Which;
    }

    /// <summary>
    /// Game controller axis motion event structure (event.caxis.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerAxisEvent
    {
        /// <summary>
        /// CONTROLLERAXISMOTION
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The controller axis (GameControllerAxis)
        /// </summary>
        public byte Axis;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public short Value;
        public ushort Padding4;
    }

    /// <summary>
    /// Game controller button event structure (event.cbutton.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerButtonEvent
    {
        /// <summary>
        /// CONTROLLERBUTTONDOWN or CONTROLLERBUTTONUP
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick instance id
        /// </summary>
        public int Which;
        /// <summary>
        /// The controller button (GameControllerButton)
        /// </summary>
        public byte Button;
        /// <summary>
        /// PRESSED or RELEASED
        /// </summary>
        public byte State;
        private byte Padding1;
        private byte Padding2;
    }

    /// <summary>
    /// Controller device event structure (event.cdevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct ControllerDeviceEvent
    {
        /// <summary>
        /// CONTROLLERDEVICEADDED, CONTROLLERDEVICEREMOVED or CONTROLLERDEVICEREMAPPED
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The joystick device index for the ADDED event, instance id for the REMOVED or REMAPPED event
        /// </summary>
        public int Which;
    }

    /// <summary>
    /// Audio device event structure (event.adevice.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        /// <summary>
        /// AUDIODEVICEADDED or AUDIODEVICEREMOVED
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The audio device index for the ADDED event (valid until next GetNumAudioDevices() call), AudioDeviceID for the REMOVED event
        /// </summary>
        public uint Which;
        /// <summary>
        /// Zero if an output device, non-zero if a capture device.
        /// </summary>
        public byte Iscapture;
        private byte Padding1;
        private byte Padding2;
        private byte Padding3;
    }

    /// <summary>
    /// Touch finger event structure (event.tfinger.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        /// <summary>
        /// FINGERMOTION or FINGERDOWN or FINGERUP
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public long TouchId;
        public long FingerId;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public float X;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public float Y;
        /// <summary>
        /// Normalized in the range -1...1
        /// </summary>
        public float Dx;
        /// <summary>
        /// Normalized in the range -1...1 
        /// </summary>
        public float Dy;
        /// <summary>
        /// Normalized in the range 0...1
        /// </summary>
        public float Pressure;
    }

    /// <summary>
    /// Multiple Finger Gesture Event (event.mgesture.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct MultiGestureEvent
    {
        /// <summary>
        /// MULTIGESTURE
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public long TouchId;
        public float DTheta;
        public float DDist;
        public float X;
        public float Y;
        public ushort NumFingers;
        public ushort Padding;
    }

    /// <summary>
    /// Dollar Gesture Event (event.dgesture.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct DollarGestureEvent
    {
        /// <summary>
        /// DOLLARGESTURE or DOLLARRECORD
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The touch device id
        /// </summary>
        public long TouchId;
        public long GestureId;
        public uint NumFingers;
        public float Error;
        /// <summary>
        /// Normalized center of gesture
        /// </summary>
        public float X;
        /// <summary>
        /// Normalized center of gesture
        /// </summary>
        public float Y;
    }

    /// <summary>
    /// An event used to request a file open by the system (event.drop.*)
    /// This event is enabled by default, you can disable it with EventState().
    /// </summary>
    /// <remarks>
    /// If this event is enabled, you must free the filename in the event. 
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct DropEvent
    {
        /// <summary>
        /// DROPBEGIN or DROPFILE or DROPTEXT or DROPCOMPLETE
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The file name, which should be freed with free(), is NULL on begin/complete
        /// </summary>
        public byte* File;
        /// <summary>
        /// The window that was dropped on, if any
        /// </summary>
        public uint WindowID;
    }

    /// <summary>
    /// The "quit requested" event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        /// <summary>
        /// QUIT
        /// </summary>
        public EventType Type;
        public uint Timestamp;
    }

    /// <summary>
    /// OS Specific event
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct OSEvent
    {
        /// <summary>
        /// QUIT
        /// </summary>
        public EventType Type;
        public uint Timestamp;
    }

    /// <summary>
    /// A user-defined event type (event.user.*)
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct UserEvent
    {
        /// <summary>
        /// USEREVENT through LASTEVENT-1
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// The associated window if any
        /// </summary>
        public uint WindowID;
        /// <summary>
        /// User defined event code
        /// </summary>
        public int Code;
        /// <summary>
        /// User defined data pointer
        /// </summary>
        public void* Data1;
        /// <summary>
        /// User defined data pointer
        /// </summary>
        public void* Data2;
    }

    /// <summary>
    /// A video driver dependent system event (event.syswm.*)
    /// This event is disabled by default, you can enable it with EventState()
    /// </summary>
    /// <remarks>
    /// If you want to use this event, you should include syswm.h.
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    unsafe public struct SysWMEvent
    {
        /// <summary>
        /// SYSWMEVENT
        /// </summary>
        public EventType Type;
        public uint Timestamp;
        /// <summary>
        /// Driver dependent data, defined in syswm.h
        /// </summary>
        public IntPtr Msg; // SDL_SysWMmsg*, system-dependent
    }

    /// <summary>
    /// General event structure
    /// </summary>
    [StructLayout(LayoutKind.Explicit, Size = 56)]
    public struct Event
    {
        /// <summary>
        /// Event type, shared with all events
        /// </summary>
        [FieldOffset(0)] public EventType Type;
        /// <summary>
        /// Common event data
        /// </summary>
        [FieldOffset(0)] public CommonEvent Common;
        /// <summary>
        /// Window event data
        /// </summary>
        [FieldOffset(0)] public WindowEvent Window;
        /// <summary>
        /// Keyboard event data
        /// </summary>
        [FieldOffset(0)] public KeyboardEvent Key;
        /// <summary>
        /// Text editing event data
        /// </summary>
        [FieldOffset(0)] public TextEditingEvent Edit;
        /// <summary>
        /// Text input event data
        /// </summary>
        [FieldOffset(0)] public TextInputEvent Text;
        /// <summary>
        /// Mouse motion event data
        /// </summary>
        [FieldOffset(0)] public MouseMotionEvent MouseMotion;
        /// <summary>
        /// Mouse button event data
        /// </summary>
        [FieldOffset(0)] public MouseButtonEvent MouseButton;
        /// <summary>
        /// Mouse wheel event data
        /// </summary>
        [FieldOffset(0)] public MouseWheelEvent MouseWheel;
        /// <summary>
        /// Joystick axis event data
        /// </summary>
        [FieldOffset(0)] public JoyAxisEvent Jaxis;
        /// <summary>
        /// Joystick ball event data
        /// </summary>
        [FieldOffset(0)] public JoyBallEvent Jball;
        /// <summary>
        /// Joystick hat event data
        /// </summary>
        [FieldOffset(0)] public JoyHatEvent Jhat;
        /// <summary>
        /// Joystick button event data
        /// </summary>
        [FieldOffset(0)] public JoyButtonEvent Jbutton;
        /// <summary>
        /// Joystick device change event data
        /// </summary>
        [FieldOffset(0)] public JoyDeviceEvent Jdevice;
        /// <summary>
        /// Game Controller axis event data
        /// </summary>
        [FieldOffset(0)] public ControllerAxisEvent Caxis;
        /// <summary>
        /// Game Controller button event data
        /// </summary>
        [FieldOffset(0)] public ControllerButtonEvent Cbutton;
        /// <summary>
        /// Game Controller device event data
        /// </summary>
        [FieldOffset(0)] public ControllerDeviceEvent Cdevice;
        /// <summary>
        /// Audio device event data
        /// </summary>
        [FieldOffset(0)] public AudioDeviceEvent Adevice;
        /// <summary>
        /// Quit request event data
        /// </summary>
        [FieldOffset(0)] public QuitEvent Quit;
        /// <summary>
        /// Custom event data
        /// </summary>
        [FieldOffset(0)] public UserEvent User;
        /// <summary>
        /// System dependent window event data
        /// </summary>
        [FieldOffset(0)] public SysWMEvent Syswm;
        /// <summary>
        /// Touch finger event data
        /// </summary>
        [FieldOffset(0)] public TouchFingerEvent Tfinger;
        /// <summary>
        /// Gesture event data
        /// </summary>
        [FieldOffset(0)] public MultiGestureEvent Mgesture;
        /// <summary>
        /// Gesture event data
        /// </summary>
        [FieldOffset(0)] public DollarGestureEvent Dgesture;
        /// <summary>
        /// Drag and drop event data
        /// </summary>
        [FieldOffset(0)] public DropEvent Drop;
    }
}