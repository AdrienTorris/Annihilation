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

        private static int[] _sdlKeyMapping = new int[(int)Button.KeyCount];

        private static byte* _keyboardState;

        private static void BuildSdlMappings()
        {
            _sdlKeyMapping[(int)Button.A] = (int)ScanCode.A;
            _sdlKeyMapping[(int)Button.B] = (int)ScanCode.B;
            _sdlKeyMapping[(int)Button.C] = (int)ScanCode.C;
            _sdlKeyMapping[(int)Button.D] = (int)ScanCode.D;
            _sdlKeyMapping[(int)Button.E] = (int)ScanCode.E;
            _sdlKeyMapping[(int)Button.F] = (int)ScanCode.F;
            _sdlKeyMapping[(int)Button.G] = (int)ScanCode.G;
            _sdlKeyMapping[(int)Button.H] = (int)ScanCode.H;
            _sdlKeyMapping[(int)Button.I] = (int)ScanCode.I;
            _sdlKeyMapping[(int)Button.J] = (int)ScanCode.J;
            _sdlKeyMapping[(int)Button.K] = (int)ScanCode.K;
            _sdlKeyMapping[(int)Button.L] = (int)ScanCode.L;
            _sdlKeyMapping[(int)Button.M] = (int)ScanCode.M;
            _sdlKeyMapping[(int)Button.N] = (int)ScanCode.N;
            _sdlKeyMapping[(int)Button.O] = (int)ScanCode.O;
            _sdlKeyMapping[(int)Button.P] = (int)ScanCode.P;
            _sdlKeyMapping[(int)Button.Q] = (int)ScanCode.Q;
            _sdlKeyMapping[(int)Button.R] = (int)ScanCode.R;
            _sdlKeyMapping[(int)Button.S] = (int)ScanCode.S;
            _sdlKeyMapping[(int)Button.T] = (int)ScanCode.T;
            _sdlKeyMapping[(int)Button.U] = (int)ScanCode.U;
            _sdlKeyMapping[(int)Button.V] = (int)ScanCode.V;
            _sdlKeyMapping[(int)Button.W] = (int)ScanCode.W;
            _sdlKeyMapping[(int)Button.X] = (int)ScanCode.X;
            _sdlKeyMapping[(int)Button.Y] = (int)ScanCode.Y;
            _sdlKeyMapping[(int)Button.Z] = (int)ScanCode.Z;
            _sdlKeyMapping[(int)Button.Alpha1] = (int)ScanCode.Alpha1;
            _sdlKeyMapping[(int)Button.Alpha2] = (int)ScanCode.Alpha2;
            _sdlKeyMapping[(int)Button.Alpha3] = (int)ScanCode.Alpha3;
            _sdlKeyMapping[(int)Button.Alpha4] = (int)ScanCode.Alpha4;
            _sdlKeyMapping[(int)Button.Alpha5] = (int)ScanCode.Alpha5;
            _sdlKeyMapping[(int)Button.Alpha6] = (int)ScanCode.Alpha6;
            _sdlKeyMapping[(int)Button.Alpha7] = (int)ScanCode.Alpha7;
            _sdlKeyMapping[(int)Button.Alpha8] = (int)ScanCode.Alpha8;
            _sdlKeyMapping[(int)Button.Alpha9] = (int)ScanCode.Alpha9;
            _sdlKeyMapping[(int)Button.Alpha0] = (int)ScanCode.Alpha0;
            _sdlKeyMapping[(int)Button.Return] = (int)ScanCode.Return;
            _sdlKeyMapping[(int)Button.Escape] = (int)ScanCode.Escape;
            _sdlKeyMapping[(int)Button.Backspace] = (int)ScanCode.Backspace;
            _sdlKeyMapping[(int)Button.Tab] = (int)ScanCode.Tab;
            _sdlKeyMapping[(int)Button.Space] = (int)ScanCode.Space;
            _sdlKeyMapping[(int)Button.Minus] = (int)ScanCode.Minus;
            _sdlKeyMapping[(int)Button.Equals] = (int)ScanCode.Equals;
            _sdlKeyMapping[(int)Button.LeftBracket] = (int)ScanCode.LeftBracket;
            _sdlKeyMapping[(int)Button.RightBracket] = (int)ScanCode.RightBracket;
            _sdlKeyMapping[(int)Button.Backslash] = (int)ScanCode.Backslash;
            _sdlKeyMapping[(int)Button.Semicolon] = (int)ScanCode.Semicolon;
            _sdlKeyMapping[(int)Button.Apostrophe] = (int)ScanCode.Apostrophe;
            _sdlKeyMapping[(int)Button.Grave] = (int)ScanCode.Grave;
            _sdlKeyMapping[(int)Button.Comma] = (int)ScanCode.Comma;
            _sdlKeyMapping[(int)Button.Period] = (int)ScanCode.Period;
            _sdlKeyMapping[(int)Button.Slash] = (int)ScanCode.Slash;
            _sdlKeyMapping[(int)Button.Capslock] = (int)ScanCode.Capslock;
            _sdlKeyMapping[(int)Button.F1] = (int)ScanCode.F1;
            _sdlKeyMapping[(int)Button.F2] = (int)ScanCode.F2;
            _sdlKeyMapping[(int)Button.F3] = (int)ScanCode.F3;
            _sdlKeyMapping[(int)Button.F4] = (int)ScanCode.F4;
            _sdlKeyMapping[(int)Button.F5] = (int)ScanCode.F5;
            _sdlKeyMapping[(int)Button.F6] = (int)ScanCode.F6;
            _sdlKeyMapping[(int)Button.F7] = (int)ScanCode.F7;
            _sdlKeyMapping[(int)Button.F8] = (int)ScanCode.F8;
            _sdlKeyMapping[(int)Button.F9] = (int)ScanCode.F9;
            _sdlKeyMapping[(int)Button.F10] = (int)ScanCode.F10;
            _sdlKeyMapping[(int)Button.F11] = (int)ScanCode.F11;
            _sdlKeyMapping[(int)Button.F12] = (int)ScanCode.F12;
            _sdlKeyMapping[(int)Button.PrintScreen] = (int)ScanCode.PrintScreen;
            _sdlKeyMapping[(int)Button.ScrollLock] = (int)ScanCode.ScrollLock;
            _sdlKeyMapping[(int)Button.Pause] = (int)ScanCode.Pause;
            _sdlKeyMapping[(int)Button.Insert] = (int)ScanCode.Insert;
            _sdlKeyMapping[(int)Button.Home] = (int)ScanCode.Home;
            _sdlKeyMapping[(int)Button.PageUp] = (int)ScanCode.PageUp;
            _sdlKeyMapping[(int)Button.Delete] = (int)ScanCode.Delete;
            _sdlKeyMapping[(int)Button.End] = (int)ScanCode.End;
            _sdlKeyMapping[(int)Button.PageDown] = (int)ScanCode.PageDown;
            _sdlKeyMapping[(int)Button.Right] = (int)ScanCode.Right;
            _sdlKeyMapping[(int)Button.Left] = (int)ScanCode.Left;
            _sdlKeyMapping[(int)Button.Down] = (int)ScanCode.Down;
            _sdlKeyMapping[(int)Button.Up] = (int)ScanCode.Up;
            _sdlKeyMapping[(int)Button.Numlock] = (int)ScanCode.NumlockClear;
            _sdlKeyMapping[(int)Button.Divide] = (int)ScanCode.NumDivide;
            _sdlKeyMapping[(int)Button.Multiply] = (int)ScanCode.NumMultiply;
            _sdlKeyMapping[(int)Button.Minus] = (int)ScanCode.Minus;
            _sdlKeyMapping[(int)Button.Add] = (int)ScanCode.NumPlus;
            _sdlKeyMapping[(int)Button.NumpadEnter] = (int)ScanCode.NumEnter;
            _sdlKeyMapping[(int)Button.Numpad1] = (int)ScanCode.Num1;
            _sdlKeyMapping[(int)Button.Numpad2] = (int)ScanCode.Num2;
            _sdlKeyMapping[(int)Button.Numpad3] = (int)ScanCode.Num3;
            _sdlKeyMapping[(int)Button.Numpad4] = (int)ScanCode.Num4;
            _sdlKeyMapping[(int)Button.Numpad5] = (int)ScanCode.Num5;
            _sdlKeyMapping[(int)Button.Numpad6] = (int)ScanCode.Num6;
            _sdlKeyMapping[(int)Button.Numpad7] = (int)ScanCode.Num7;
            _sdlKeyMapping[(int)Button.Numpad8] = (int)ScanCode.Num8;
            _sdlKeyMapping[(int)Button.Numpad9] = (int)ScanCode.Num9;
            _sdlKeyMapping[(int)Button.Numpad0] = (int)ScanCode.Num0;
            _sdlKeyMapping[(int)Button.NumpadPeriod] = (int)ScanCode.NumPeriod;
            _sdlKeyMapping[(int)Button.LeftControl] = (int)ScanCode.LeftControl;
            _sdlKeyMapping[(int)Button.RightControl] = (int)ScanCode.RightControl;
            _sdlKeyMapping[(int)Button.LeftWindows] = (int)ScanCode.LeftGUI;
            _sdlKeyMapping[(int)Button.RightWindows] = (int)ScanCode.RightGUI;
            _sdlKeyMapping[(int)Button.LeftAlt] = (int)ScanCode.LeftAlt;
            _sdlKeyMapping[(int)Button.RightAlt] = (int)ScanCode.RightAlt;
            _sdlKeyMapping[(int)Button.Application] = (int)ScanCode.Application;
        }
        
        public static void Initialize()
        {
            _buttons = Memory.AllocateBytes((int)Button.Count);
            _previousButtons = Memory.AllocateBytes((int)Button.Count);
            _holdDurations = Memory.AllocateFloats((int)Button.Count);
            _analogs = Memory.AllocateFloats((int)Analog.Count);
            _analogsTimeCorrected = Memory.AllocateFloats((int)Analog.Count);

            BuildSdlMappings();

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
        
        public static void Update(float deltaTime, int mouseWheel)
        {
            Memory.Copy(_previousButtons, _buttons, (int)Button.Count);
            Memory.Clear(_buttons, 0, (int)Button.Count);
            Memory.Clear((byte*)_analogs, 0, (int)Analog.Count * sizeof(float));

            // Keyboard
            for (int i = 0; i < (int)Button.KeyCount; ++i)
            {
                int scanCode = _sdlKeyMapping[i];
                _buttons[i] = _keyboardState[scanCode];
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

            // Mouse wheel
            _analogs[(int)Analog.MouseWheel] = mouseWheel > 0 ? 1f : mouseWheel < 0f ? -1f : 0f;

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
    }
}