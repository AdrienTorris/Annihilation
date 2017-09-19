using static SDL2.SDL;

namespace Engine.Input
{
    public static unsafe class InputSystem
    {
        private static byte* _buttons;
        private static byte* _previousButtons;
        private static float* _holdDurations;
        private static float* _analogs;
        private static float* _analogsTimeCorrected;
        private static byte* _keyboardState;

        public static void Initialize()
        {
            _buttons = Memory.AllocateAndClearBytes((int)Button.Count);
            _previousButtons = Memory.AllocateAndClearBytes((int)Button.Count);
            _holdDurations = Memory.AllocateAndClearFloats((int)Button.Count);
            _analogs = Memory.AllocateAndClearFloats((int)Analog.Count);
            _analogsTimeCorrected = Memory.AllocateAndClearFloats((int)Analog.Count);
            
            _keyboardState = GetKeyboardState(null);
        }

        public static void Shutdown()
        {
            Memory.Free(_buttons);
            Memory.Free(_previousButtons);
            Memory.Free(_holdDurations);
            Memory.Free(_analogs);
            Memory.Free(_analogsTimeCorrected);
        }

        public static void EnableTextInput() => StartTextInput();
        public static void DisableTextInput() => StopTextInput();

        public static void Update(float deltaTime, Event evt)
        {
            Memory.Copy(_previousButtons, _buttons, (int)Button.Count);
            Memory.Clear(_buttons, 0, (int)Button.Count);
            Memory.Clear(_analogs, 0f, (int)Analog.Count);

            // Keyboard
            for (int i = 0; i < (int)Button.KeyCount; ++i)
            {
                ScanCode scanCode = GetSdlScanCode((Button)i);
                _buttons[i] = _keyboardState[(int)scanCode];
            }

            // Mouse buttons
            uint mouseButtonState = GetMouseState(out int mouseX, out int mouseY);
            for (int i = 0; i < 5; ++i)
            {
                _buttons[i + (int)Button.MouseLeft] = (mouseButtonState & (1 << i)) != 0 ? (byte)1 : (byte)0;
            }

            // Mouse move
            _analogs[(int)Analog.MouseX] = mouseX * 0.0018f;
            _analogs[(int)Analog.MouseY] = mouseY * -0.0018f;

            // Hold durations
            for (int i = 0; i < (int)Button.KeyCount; ++i)
            {
                if (_buttons[i] != 0)
                {
                    if (_previousButtons[i] == 0)
                    {
                        _holdDurations[i] = 0f;
                    }
                    else
                    {
                        _holdDurations[i] += deltaTime;
                    }
                }
            }

            // Axis
            switch (evt.Type)
            {
                case EventType.MouseWheel:
                {
                    _analogs[(int)Analog.MouseWheel] = evt.MouseWheelEvent.Y;
                    break;
                }
            }

            // Time corrected analogs
            for (int i = 0; i < (int)Analog.Count; ++i)
            {
                _analogsTimeCorrected[i] = _analogs[i] * deltaTime;
            }
        }

        public static bool IsAnyPressed()
        {
            return !Memory.IsZeroed(_buttons, (int)Button.Count);
        }

        public static bool IsPressed(Button button)
        {
            return _buttons[(byte)button] != 0;
        }

        public static bool WasPressed(Button button)
        {
            return (_buttons[(byte)button] != 0) && (_previousButtons[(byte)button] == 0);
        }

        public static bool IsReleased(Button button)
        {
            return _buttons[(byte)button] == 0;
        }

        public static bool WasReleased(Button button)
        {
            return (_buttons[(byte)button] == 0) && (_previousButtons[(byte)button] != 0);
        }

        public static float GetDurationPressed(Button button)
        {
            return _holdDurations[(byte)button];
        }

        public static float GetAnalogInput(Analog analog)
        {
            return _analogs[(byte)analog];
        }

        public static float GetTimeCorrectedAnalogInput(Analog analog)
        {
            return _analogsTimeCorrected[(byte)analog];
        }

        private static ScanCode GetSdlScanCode(Button button)
        {
            switch (button)
            {
                case Button.A: return ScanCode.A;
                case Button.B: return ScanCode.B;
                case Button.C: return ScanCode.C;
                case Button.D: return ScanCode.D;
                case Button.E: return ScanCode.E;
                case Button.F: return ScanCode.F;
                case Button.G: return ScanCode.G;
                case Button.H: return ScanCode.H;
                case Button.I: return ScanCode.I;
                case Button.J: return ScanCode.J;
                case Button.K: return ScanCode.K;
                case Button.L: return ScanCode.L;
                case Button.M: return ScanCode.M;
                case Button.N: return ScanCode.N;
                case Button.O: return ScanCode.O;
                case Button.P: return ScanCode.P;
                case Button.Q: return ScanCode.Q;
                case Button.R: return ScanCode.R;
                case Button.S: return ScanCode.S;
                case Button.T: return ScanCode.T;
                case Button.U: return ScanCode.U;
                case Button.V: return ScanCode.V;
                case Button.W: return ScanCode.W;
                case Button.X: return ScanCode.X;
                case Button.Y: return ScanCode.Y;
                case Button.Z: return ScanCode.Z;
                case Button.Alpha1: return ScanCode.Alpha1;
                case Button.Alpha2: return ScanCode.Alpha2;
                case Button.Alpha3: return ScanCode.Alpha3;
                case Button.Alpha4: return ScanCode.Alpha4;
                case Button.Alpha5: return ScanCode.Alpha5;
                case Button.Alpha6: return ScanCode.Alpha6;
                case Button.Alpha7: return ScanCode.Alpha7;
                case Button.Alpha8: return ScanCode.Alpha8;
                case Button.Alpha9: return ScanCode.Alpha9;
                case Button.Alpha0: return ScanCode.Alpha0;
                case Button.Return: return ScanCode.Return;
                case Button.Escape: return ScanCode.Escape;
                case Button.Backspace: return ScanCode.Backspace;
                case Button.Tab: return ScanCode.Tab;
                case Button.Space: return ScanCode.Space;
                case Button.Minus: return ScanCode.Minus;
                case Button.Equals: return ScanCode.Equals;
                case Button.LeftBracket: return ScanCode.LeftBracket;
                case Button.RightBracket: return ScanCode.RightBracket;
                case Button.Backslash: return ScanCode.Backslash;
                case Button.Semicolon: return ScanCode.Semicolon;
                case Button.Apostrophe: return ScanCode.Apostrophe;
                case Button.Grave: return ScanCode.Grave;
                case Button.Comma: return ScanCode.Comma;
                case Button.Period: return ScanCode.Period;
                case Button.Slash: return ScanCode.Slash;
                case Button.Capslock: return ScanCode.Capslock;
                case Button.F1: return ScanCode.F1;
                case Button.F2: return ScanCode.F2;
                case Button.F3: return ScanCode.F3;
                case Button.F4: return ScanCode.F4;
                case Button.F5: return ScanCode.F5;
                case Button.F6: return ScanCode.F6;
                case Button.F7: return ScanCode.F7;
                case Button.F8: return ScanCode.F8;
                case Button.F9: return ScanCode.F9;
                case Button.F10: return ScanCode.F10;
                case Button.F11: return ScanCode.F11;
                case Button.F12: return ScanCode.F12;
                case Button.PrintScreen: return ScanCode.PrintScreen;
                case Button.ScrollLock: return ScanCode.ScrollLock;
                case Button.Pause: return ScanCode.Pause;
                case Button.Insert: return ScanCode.Insert;
                case Button.Home: return ScanCode.Home;
                case Button.PageUp: return ScanCode.PageUp;
                case Button.Delete: return ScanCode.Delete;
                case Button.End: return ScanCode.End;
                case Button.PageDown: return ScanCode.PageDown;
                case Button.Right: return ScanCode.Right;
                case Button.Left: return ScanCode.Left;
                case Button.Down: return ScanCode.Down;
                case Button.Up: return ScanCode.Up;
                case Button.Numlock: return ScanCode.NumlockClear;
                case Button.Divide: return ScanCode.NumDivide;
                case Button.Multiply: return ScanCode.NumMultiply;
                case Button.Add: return ScanCode.NumPlus;
                case Button.NumpadEnter: return ScanCode.NumEnter;
                case Button.Numpad1: return ScanCode.Num1;
                case Button.Numpad2: return ScanCode.Num2;
                case Button.Numpad3: return ScanCode.Num3;
                case Button.Numpad4: return ScanCode.Num4;
                case Button.Numpad5: return ScanCode.Num5;
                case Button.Numpad6: return ScanCode.Num6;
                case Button.Numpad7: return ScanCode.Num7;
                case Button.Numpad8: return ScanCode.Num8;
                case Button.Numpad9: return ScanCode.Num9;
                case Button.Numpad0: return ScanCode.Num0;
                case Button.NumpadPeriod: return ScanCode.NumPeriod;
                case Button.LeftControl: return ScanCode.LeftControl;
                case Button.RightControl: return ScanCode.RightControl;
                case Button.LeftWindows: return ScanCode.LeftGUI;
                case Button.RightWindows: return ScanCode.RightGUI;
                case Button.LeftAlt: return ScanCode.LeftAlt;
                case Button.RightAlt: return ScanCode.RightAlt;
                case Button.Application: return ScanCode.Application;
                default: return ScanCode.Unknown;
            }
        }
    }
}