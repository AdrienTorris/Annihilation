using System;

namespace SDL2
{
    public static partial class SDL
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
        // SDL_audio.h
        //
        [Flags]
        public enum AudioFormat : ushort
        {
            U8 = 0x0008,
            S8 = 0x8008,
            U16LSB = 0x0010,
            S16LSB = 0x8010,
            U16MSB = 0x1010,
            S16MSB = 0x9010,
            U16 = U16LSB,
            S16 = S16LSB
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

        public enum State : sbyte
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
        // SDL_haptic.h
        //
        [Flags]
        public enum HapticEffectType : ushort
        {
            Constant = 1 << 0,
            Sine = 1 << 1,
            LeftRight = 1 << 2,
            Triangle = 1 << 3,
            SawtoothUp = 1 << 4,
            SawtoothDown = 1 << 5,
            Ramp = 1 << 6,
            Spring = 1 << 7,
            Damper = 1 << 8,
            Inertia = 1 << 9,
            Friction = 1 << 10,
            Custom = 1 << 11,
            Gain = 1 << 12,
            AutoCenter = 1 << 13,
            Status = 1 << 14,
            Pause = 1 << 15
        }

        public enum HapticDirectionType : byte
        {
            Polar = 0,
            Cartesian = 1,
            Spherical = 2
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
            A = 'a',
            B = 'b',
            C = 'c',
            D = 'd',
            E = 'e',
            F = 'f',
            G = 'g',
            H = 'h',
            I = 'i',
            J = 'j',
            K = 'k',
            L = 'l',
            M = 'm',
            N = 'n',
            O = 'o',
            P = 'p',
            Q = 'q',
            R = 'r',
            S = 's',
            T = 't',
            U = 'u',
            V = 'v',
            W = 'w',
            X = 'x',
            Y = 'y',
            Z = 'z',

            Capslock = Scancode.Capslock | ScanCodeMask,

            F1 = Scancode.F1 | ScanCodeMask,
            F2 = Scancode.F2 | ScanCodeMask,
            F3 = Scancode.F3 | ScanCodeMask,
            F4 = Scancode.F4 | ScanCodeMask,
            F5 = Scancode.F5 | ScanCodeMask,
            F6 = Scancode.F6 | ScanCodeMask,
            F7 = Scancode.F7 | ScanCodeMask,
            F8 = Scancode.F8 | ScanCodeMask,
            F9 = Scancode.F9 | ScanCodeMask,
            F10 = Scancode.F10 | ScanCodeMask,
            F11 = Scancode.F11 | ScanCodeMask,
            F12 = Scancode.F12 | ScanCodeMask,

            Printscreen = Scancode.Printscreen | ScanCodeMask,
            ScrollLock = Scancode.ScrollLock | ScanCodeMask,
            Pause = Scancode.Pause | ScanCodeMask,
            Insert = Scancode.Insert | ScanCodeMask,
            Home = Scancode.Home | ScanCodeMask,
            PageUp = Scancode.PageUp | ScanCodeMask,
            Delete = 127, // '\177',
            End = Scancode.End | ScanCodeMask,
            PageDown = Scancode.PageDown | ScanCodeMask,
            Right = Scancode.Right | ScanCodeMask,
            Left = Scancode.Left | ScanCodeMask,
            Down = Scancode.Down | ScanCodeMask,
            Up = Scancode.Up | ScanCodeMask,

            NumlockClear = Scancode.NumlockClear | ScanCodeMask,
            NumDivide = Scancode.NumDivide | ScanCodeMask,
            NumMultiply = Scancode.NumMultiply | ScanCodeMask,
            NumMinus = Scancode.NumMinus | ScanCodeMask,
            NumPlus = Scancode.NumPlus | ScanCodeMask,
            NumEnter = Scancode.NumEnter | ScanCodeMask,
            Num1 = Scancode.Num1 | ScanCodeMask,
            Num2 = Scancode.Num2 | ScanCodeMask,
            Num3 = Scancode.Num3 | ScanCodeMask,
            Num4 = Scancode.Num4 | ScanCodeMask,
            Num5 = Scancode.Num5 | ScanCodeMask,
            Num6 = Scancode.Num6 | ScanCodeMask,
            Num7 = Scancode.Num7 | ScanCodeMask,
            Num8 = Scancode.Num8 | ScanCodeMask,
            Num9 = Scancode.Num9 | ScanCodeMask,
            Num0 = Scancode.Num0 | ScanCodeMask,
            NumPeriod = Scancode.NumPeriod | ScanCodeMask,

            Application = Scancode.Application | ScanCodeMask,
            Power = Scancode.Power | ScanCodeMask,
            NumEquals = Scancode.NumEquals | ScanCodeMask,
            F13 = Scancode.F13 | ScanCodeMask,
            F14 = Scancode.F14 | ScanCodeMask,
            F15 = Scancode.F15 | ScanCodeMask,
            F16 = Scancode.F16 | ScanCodeMask,
            F17 = Scancode.F17 | ScanCodeMask,
            F18 = Scancode.F18 | ScanCodeMask,
            F19 = Scancode.F19 | ScanCodeMask,
            F20 = Scancode.F20 | ScanCodeMask,
            F21 = Scancode.F21 | ScanCodeMask,
            F22 = Scancode.F22 | ScanCodeMask,
            F23 = Scancode.F23 | ScanCodeMask,
            F24 = Scancode.F24 | ScanCodeMask,
            Execute = Scancode.Execute | ScanCodeMask,
            Help = Scancode.Help | ScanCodeMask,
            Menu = Scancode.Menu | ScanCodeMask,
            Select = Scancode.Select | ScanCodeMask,
            Stop = Scancode.Stop | ScanCodeMask,
            Again = Scancode.Again | ScanCodeMask,
            Undo = Scancode.Undo | ScanCodeMask,
            Cut = Scancode.Cut | ScanCodeMask,
            Copy = Scancode.Copy | ScanCodeMask,
            Paste = Scancode.Paste | ScanCodeMask,
            Find = Scancode.Find | ScanCodeMask,
            Mute = Scancode.Mute | ScanCodeMask,
            VolumeUp = Scancode.VolumeUp | ScanCodeMask,
            VolumeDown = Scancode.VolumeDown | ScanCodeMask,
            NumComma = Scancode.NumComma | ScanCodeMask,
            NumEqualsAs400 = Scancode.NumEqualsAs400 | ScanCodeMask,

            AltErase = Scancode.AltErase | ScanCodeMask,
            SysReq = Scancode.SysReq | ScanCodeMask,
            Cancel = Scancode.Cancel | ScanCodeMask,
            Clear = Scancode.Clear | ScanCodeMask,
            Prior = Scancode.Prior | ScanCodeMask,
            Return2 = Scancode.Return2 | ScanCodeMask,
            Separator = Scancode.Separator | ScanCodeMask,
            Out = Scancode.Out | ScanCodeMask,
            Oper = Scancode.Oper | ScanCodeMask,
            ClearAgain = Scancode.ClearAgain | ScanCodeMask,
            CRSel = Scancode.CRSel | ScanCodeMask,
            EXSel = Scancode.EXSel | ScanCodeMask,

            Num00 = Scancode.Num00 | ScanCodeMask,
            Num000 = Scancode.Num000 | ScanCodeMask,
            ThousandsSeparator = Scancode.ThousandsSeparator | ScanCodeMask,
            DecimalSeparator = Scancode.DecimalSeparator | ScanCodeMask,
            CurrencyUnit = Scancode.CurrencyUnit | ScanCodeMask,
            CurrencySubUnit = Scancode.CurrencySubUnit | ScanCodeMask,
            NumLeftParen = Scancode.NumLeftParen | ScanCodeMask,
            NumRightParen = Scancode.NumRightParen | ScanCodeMask,
            NumLeftBrace = Scancode.NumLeftBrace | ScanCodeMask,
            NumRightBrace = Scancode.NumRightBrace | ScanCodeMask,
            NumTab = Scancode.NumTab | ScanCodeMask,
            NumBackspace = Scancode.NumBackspace | ScanCodeMask,
            NumA = Scancode.NumA | ScanCodeMask,
            NumB = Scancode.NumB | ScanCodeMask,
            NumC = Scancode.NumC | ScanCodeMask,
            NumD = Scancode.NumD | ScanCodeMask,
            NumE = Scancode.NumE | ScanCodeMask,
            NumF = Scancode.NumF | ScanCodeMask,
            NumXor = Scancode.NumXor | ScanCodeMask,
            NumPower = Scancode.NumPower | ScanCodeMask,
            NumPercent = Scancode.NumPercent | ScanCodeMask,
            NumLess = Scancode.NumLess | ScanCodeMask,
            NumGreater = Scancode.NumGreater | ScanCodeMask,
            NumAmpersand = Scancode.NumAmpersand | ScanCodeMask,
            NumDoubleAmpersand = Scancode.NumDoubleAmpersand | ScanCodeMask,
            NumVerticalBar = Scancode.NumVerticalBar | ScanCodeMask,
            NumDoubleVerticalBar = Scancode.NumDoubleVerticalBar | ScanCodeMask,
            NumColon = Scancode.NumColon | ScanCodeMask,
            NumHash = Scancode.NumHash | ScanCodeMask,
            NumSpace = Scancode.NumSpace | ScanCodeMask,
            NumAt = Scancode.NumAt | ScanCodeMask,
            NumExclam = Scancode.NumExclam | ScanCodeMask,
            NumMemStore = Scancode.NumMemStore | ScanCodeMask,
            NumMemRecall = Scancode.NumMemRecall | ScanCodeMask,
            NumMemClear = Scancode.NumMemClear | ScanCodeMask,
            NumMemAdd = Scancode.NumMemAdd | ScanCodeMask,
            NumMemSubtract = Scancode.NumMemSubtract | ScanCodeMask,
            NumMemMultiply = Scancode.NumMemMultiply | ScanCodeMask,
            NumMemDivide = Scancode.NumMemDivide | ScanCodeMask,
            NumPlusMinus = Scancode.NumPlusMinus | ScanCodeMask,
            NumClear = Scancode.NumClear | ScanCodeMask,
            NumClearEntry = Scancode.NumClearEntry | ScanCodeMask,
            NumBinary = Scancode.NumBinary | ScanCodeMask,
            NumOctal = Scancode.NumOctal | ScanCodeMask,
            NumDecimal = Scancode.NumDecimal | ScanCodeMask,
            NumHexadecimal = Scancode.NumHexadecimal | ScanCodeMask,

            LeftControl = Scancode.LeftControl | ScanCodeMask,
            LeftShift = Scancode.LeftShift | ScanCodeMask,
            LeftAlt = Scancode.LeftAlt | ScanCodeMask,
            LeftGUI = Scancode.LeftGUI | ScanCodeMask,
            RightControl = Scancode.RightControl | ScanCodeMask,
            RightShift = Scancode.RightShift | ScanCodeMask,
            RightAlt = Scancode.RightAlt | ScanCodeMask,
            RightGUI = Scancode.RightGUI | ScanCodeMask,

            Mode = Scancode.Mode | ScanCodeMask,

            AudioNext = Scancode.AudioNext | ScanCodeMask,
            AudioPrevious = Scancode.AudioPrevious | ScanCodeMask,
            AudioStop = Scancode.AudioStop | ScanCodeMask,
            AudioPlay = Scancode.AudioPlay | ScanCodeMask,
            AudioMute = Scancode.AudioMute | ScanCodeMask,
            MediaSelect = Scancode.MediaSelect | ScanCodeMask,
            WWW = Scancode.WWW | ScanCodeMask,
            Mail = Scancode.Mail | ScanCodeMask,
            Calculator = Scancode.Calculator | ScanCodeMask,
            Computer = Scancode.Computer | ScanCodeMask,
            ACSearch = Scancode.ACSearch | ScanCodeMask,
            ACHome = Scancode.ACHome | ScanCodeMask,
            ACBack = Scancode.ACBack | ScanCodeMask,
            ACForward = Scancode.ACForward | ScanCodeMask,
            ACStop = Scancode.ACStop | ScanCodeMask,
            ACRefresh = Scancode.ACRefresh | ScanCodeMask,
            ACBookmark = Scancode.ACBookmark | ScanCodeMask,

            BrightnessDown = Scancode.BrightnessDown | ScanCodeMask,
            BrightnessUp = Scancode.BrightnessUp | ScanCodeMask,
            DisplaySwitch = Scancode.DisplaySwitch | ScanCodeMask,
            KeyboardLightToggle = Scancode.KeyboardLightToggle | ScanCodeMask,
            KeyboardLightDown = Scancode.KeyboardLightDown | ScanCodeMask,
            KeyboardLightUp = Scancode.KeyboardLightUp | ScanCodeMask,
            Eject = Scancode.Eject | ScanCodeMask,
            Sleep = Scancode.Sleep | ScanCodeMask
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
        public enum Scancode
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

        public enum WindowEventID : byte
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
}