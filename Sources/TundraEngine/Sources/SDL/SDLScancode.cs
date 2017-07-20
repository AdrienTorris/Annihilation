using System.Security;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        /// <summary>
        /// The SDL keyboard scancode representation.
        /// <para /> Values of this type are used to represent keyboard keys, among other places in the <see cref="SDL_KeySym.ScanCode"/> field of the <see cref="Event"/> structure.
        /// <para /> The values in this enumeration are based on the USB usage page standard: http://www.usb.org/developers/devclass_docs/Hut1_12v2.pdf
        /// </summary>
        public enum SDL_ScanCode
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
    }
}