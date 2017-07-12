using System;
using System.Collections.Generic;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Input
{
    internal class EventProviderSDL : LibrarySystem<LibSDL>, IEventProvider
    {
        private IntPtr[] _gameControllers = new IntPtr[Constants.MaxPlayerCount];

        private static readonly Dictionary<int, Button> _keyboardButtonMap = new Dictionary<int, Button>(200)
        {
            { (int)SDL_KeyCode.Space, Button.Space },
            { (int)SDL_KeyCode.Return, Button.Return },
            { (int)SDL_KeyCode.Backspace, Button.Backspace },
            { (int)SDL_KeyCode.Tab, Button.Tab },
            { (int)SDL_KeyCode.Clear, Button.Clear },
            { (int)SDL_KeyCode.Pause, Button.Pause },
            { (int)SDL_KeyCode.Escape, Button.Escape },
            { (int)SDL_KeyCode.Exclaim, Button.Exclaim },
            { (int)SDL_KeyCode.QuoteDouble, Button.DoubleQuote },
            { (int)SDL_KeyCode.Hash, Button.Hash },
            { (int)SDL_KeyCode.Dollar, Button.Dollar },
            { (int)SDL_KeyCode.Ampersand, Button.Ampersand },
            { (int)SDL_KeyCode.Quote, Button.Quote },
            { (int)SDL_KeyCode.LeftParenthesis, Button.LeftParen },
            { (int)SDL_KeyCode.RightParenthesis, Button.RightParen },
            { (int)SDL_KeyCode.Asterisk, Button.Asterisk },
            { (int)SDL_KeyCode.Plus, Button.Plus },
            { (int)SDL_KeyCode.Comma, Button.Comma },
            { (int)SDL_KeyCode.Minus, Button.Minus },
            { (int)SDL_KeyCode.Period, Button.Period },
            { (int)SDL_KeyCode.Slash, Button.Slash },
            { (int)SDL_KeyCode.Colon, Button.Colon },
            { (int)SDL_KeyCode.Semicolon, Button.Semicolon },
            { (int)SDL_KeyCode.Less, Button.Less },
            { (int)SDL_KeyCode.Equals, Button.Equals },
            { (int)SDL_KeyCode.Greater, Button.Greater },
            { (int)SDL_KeyCode.Question, Button.Question },
            { (int)SDL_KeyCode.At, Button.At },
            { (int)SDL_KeyCode.LeftBracket, Button.LeftBracket },
            { (int)SDL_KeyCode.RightBracket, Button.RightBracket },
            { (int)SDL_KeyCode.Backslash, Button.Backslash },
            { (int)SDL_KeyCode.Caret, Button.Caret },
            { (int)SDL_KeyCode.Underscore, Button.Underscore },
            { (int)SDL_KeyCode.Backquote, Button.BackQuote },
            { (int)SDL_KeyCode.Delete, Button.Delete },
            { (int)SDL_KeyCode.Up, Button.UpArrow },
            { (int)SDL_KeyCode.Down, Button.DownArrow },
            { (int)SDL_KeyCode.Left, Button.LeftArrow },
            { (int)SDL_KeyCode.Right, Button.RightArrow },
            { (int)SDL_KeyCode.Insert, Button.Insert },
            { (int)SDL_KeyCode.Home, Button.Home },
            { (int)SDL_KeyCode.End, Button.End },
            { (int)SDL_KeyCode.PageUp, Button.PageUp },
            { (int)SDL_KeyCode.PageDown, Button.PageDown },
            { (int)SDL_KeyCode.NumlockClear, Button.NumLock },
            { (int)SDL_KeyCode.Capslock, Button.CapsLock },
            { (int)SDL_KeyCode.ScrollLock, Button.ScrollLock },
            { (int)SDL_KeyCode.RightShift, Button.RightShift },
            { (int)SDL_KeyCode.LeftShift, Button.LeftShift },
            { (int)SDL_KeyCode.RightControl, Button.RightControl },
            { (int)SDL_KeyCode.LeftControl, Button.LeftControl },
            { (int)SDL_KeyCode.RightAlt, Button.RightAlt },
            { (int)SDL_KeyCode.LeftAlt, Button.LeftAlt },
            { (int)SDL_KeyCode.RightGUI, Button.RightWindowsCommand },
            { (int)SDL_KeyCode.LeftGUI, Button.LeftWindowsCommand },
            { (int)SDL_KeyCode.Help, Button.Help },
            { (int)SDL_KeyCode.Printscreen, Button.Print },
            { (int)SDL_KeyCode.SysReq, Button.SysReq },
            { (int)SDL_KeyCode.Menu, Button.Menu },
            { (int)SDL_KeyCode.a, Button.A },
            { (int)SDL_KeyCode.b, Button.B },
            { (int)SDL_KeyCode.c, Button.C },
            { (int)SDL_KeyCode.d, Button.D },
            { (int)SDL_KeyCode.e, Button.E },
            { (int)SDL_KeyCode.f, Button.F },
            { (int)SDL_KeyCode.g, Button.G },
            { (int)SDL_KeyCode.h, Button.H },
            { (int)SDL_KeyCode.i, Button.I },
            { (int)SDL_KeyCode.j, Button.J },
            { (int)SDL_KeyCode.k, Button.K },
            { (int)SDL_KeyCode.l, Button.L },
            { (int)SDL_KeyCode.m, Button.M },
            { (int)SDL_KeyCode.n, Button.N },
            { (int)SDL_KeyCode.o, Button.O },
            { (int)SDL_KeyCode.p, Button.P },
            { (int)SDL_KeyCode.q, Button.Q },
            { (int)SDL_KeyCode.r, Button.R },
            { (int)SDL_KeyCode.s, Button.S },
            { (int)SDL_KeyCode.t, Button.T },
            { (int)SDL_KeyCode.u, Button.U },
            { (int)SDL_KeyCode.v, Button.V },
            { (int)SDL_KeyCode.w, Button.W },
            { (int)SDL_KeyCode.x, Button.X },
            { (int)SDL_KeyCode.y, Button.Y },
            { (int)SDL_KeyCode.z, Button.Z },
            { (int)SDL_KeyCode.Alpha0, Button.Alpha0 },
            { (int)SDL_KeyCode.Alpha1, Button.Alpha1 },
            { (int)SDL_KeyCode.Alpha2, Button.Alpha2 },
            { (int)SDL_KeyCode.Alpha3, Button.Alpha3 },
            { (int)SDL_KeyCode.Alpha4, Button.Alpha4 },
            { (int)SDL_KeyCode.Alpha5, Button.Alpha5 },
            { (int)SDL_KeyCode.Alpha6, Button.Alpha6 },
            { (int)SDL_KeyCode.Alpha7, Button.Alpha7 },
            { (int)SDL_KeyCode.Alpha8, Button.Alpha8 },
            { (int)SDL_KeyCode.Alpha9, Button.Alpha9 },
            { (int)SDL_KeyCode.Num0, Button.Keypad0 },
            { (int)SDL_KeyCode.Num1, Button.Keypad1 },
            { (int)SDL_KeyCode.Num2, Button.Keypad2 },
            { (int)SDL_KeyCode.Num3, Button.Keypad3 },
            { (int)SDL_KeyCode.Num4, Button.Keypad4 },
            { (int)SDL_KeyCode.Num5, Button.Keypad5 },
            { (int)SDL_KeyCode.Num6, Button.Keypad6 },
            { (int)SDL_KeyCode.Num7, Button.Keypad7 },
            { (int)SDL_KeyCode.Num8, Button.Keypad8 },
            { (int)SDL_KeyCode.Num9, Button.Keypad9 },
            { (int)SDL_KeyCode.NumPeriod, Button.KeypadPeriod },
            { (int)SDL_KeyCode.NumDivide, Button.KeypadDivide },
            { (int)SDL_KeyCode.NumMultiply, Button.KeypadMultiply },
            { (int)SDL_KeyCode.NumMinus, Button.KeypadMinus },
            { (int)SDL_KeyCode.NumPlus, Button.KeypadPlus },
            { (int)SDL_KeyCode.NumEnter, Button.KeypadEnter },
            { (int)SDL_KeyCode.NumEquals, Button.KeypadEquals },
            { (int)SDL_KeyCode.F1, Button.F1 },
            { (int)SDL_KeyCode.F2, Button.F2 },
            { (int)SDL_KeyCode.F3, Button.F3 },
            { (int)SDL_KeyCode.F4, Button.F4 },
            { (int)SDL_KeyCode.F5, Button.F5 },
            { (int)SDL_KeyCode.F6, Button.F6 },
            { (int)SDL_KeyCode.F7, Button.F7 },
            { (int)SDL_KeyCode.F8, Button.F8 },
            { (int)SDL_KeyCode.F9, Button.F9 },
            { (int)SDL_KeyCode.F10, Button.F10 },
            { (int)SDL_KeyCode.F11, Button.F11 },
            { (int)SDL_KeyCode.F12, Button.F12 },
            { (int)SDL_KeyCode.F13, Button.F13 },
            { (int)SDL_KeyCode.F14, Button.F14 },
            { (int)SDL_KeyCode.F15, Button.F15 },
        };
        private static readonly Dictionary<byte, Button> _mouseButtonMap = new Dictionary<byte, Button>(5)
        {
            { (byte)SDL_MouseButton.Left, Button.MouseLeft },
            { (byte)SDL_MouseButton.Middle, Button.MouseMiddle },
            { (byte)SDL_MouseButton.Right, Button.MouseRight },
            { (byte)SDL_MouseButton.X1, Button.MouseExtra1 },
            { (byte)SDL_MouseButton.X2, Button.MouseExtra2 },
        };
        private static readonly Dictionary<int, Button> _gamepadButtonMap = new Dictionary<int, Button>(15)
        {
            { (int)SDL_GameControllerButton.A, Button.GamepadA },
            { (int)SDL_GameControllerButton.B, Button.GamepadB },
            { (int)SDL_GameControllerButton.X, Button.GamepadX },
            { (int)SDL_GameControllerButton.Y, Button.GamepadY },
            { (int)SDL_GameControllerButton.Back, Button.GamepadBack },
            { (int)SDL_GameControllerButton.Guide, Button.GamepadGuide },
            { (int)SDL_GameControllerButton.Start, Button.GamepadStart },
            { (int)SDL_GameControllerButton.LeftStick, Button.GamepadThumbLeft },
            { (int)SDL_GameControllerButton.RightStick, Button.GamepadThumbRight },
            { (int)SDL_GameControllerButton.LeftShoulder, Button.GamepadTriggerLeft },
            { (int)SDL_GameControllerButton.RightShoulder, Button.GamepadTriggerRight },
            { (int)SDL_GameControllerButton.DPadUp, Button.GamepadUp },
            { (int)SDL_GameControllerButton.DPadDown, Button.GamepadDown },
            { (int)SDL_GameControllerButton.DPadLeft, Button.GamepadLeft },
            { (int)SDL_GameControllerButton.DPadRight, Button.GamepadRight },
        };

        public uint ConnectedControllerCount { get; private set; }

        public void PollEvents(out List<InputEvent> inputEvents)
        {
            inputEvents = new List<InputEvent>();
            //  inputEvent = new InputEvent();
            
            while (SDL_PollEvent(out SDL_Event sdlEvent) == 1)
            {
                switch (sdlEvent.Type)
                {
                    case SDL_EventType.Quit:
                        Game.Instance.Quit();
                        break;

                    case SDL_EventType.KeyDown:
                    case SDL_EventType.KeyUp:
                        {
                            // Get the pressed or released button
                            if (_keyboardButtonMap.TryGetValue((int)sdlEvent.Key.KeySym.Sym, out Button button) == false)
                            {
                                break;
                            }

                            // Create the event
                            InputEvent inputEvent = new InputEvent
                            {
                                Type = InputEventType.Button,
                                ButtonEvent = new ButtonEvent
                                {
                                    PlayerId = 0,
                                    Button = button,
                                    State = (ButtonState)sdlEvent.Key.State
                                }
                            };

                            inputEvents.Add(inputEvent);
                        }
                        break;

                    case SDL_EventType.MouseButtonDown:
                    case SDL_EventType.MouseButtonUp:
                        {
                            // Get the pressed or released button
                            if (_mouseButtonMap.TryGetValue((byte)sdlEvent.MouseButton.Button, out Button button) == false)
                            {
                                break;
                            }
                            button += (byte)(sdlEvent.MouseButton.Clicks - 1);

                            // Create the evet
                            InputEvent inputEvent = new InputEvent
                            {
                                Type = InputEventType.Button,
                                ButtonEvent = new ButtonEvent
                                {
                                    PlayerId = 0,
                                    Button = button,
                                    State = (ButtonState)sdlEvent.MouseButton.State
                                }
                            };
                            inputEvents.Add(inputEvent);
                        }
                        break;
                }
            }
        }

        protected override void InitializeLibrary()
        {
            LibraryUtility.InitializeSDL();
        }

        protected override void ShutdownLibrary()
        {
            SDL_Quit();
        }

        protected override void DisposeUnmanaged()
        {
            // TODO: Dispose of controllers and other SDL handles
        }
    }
}