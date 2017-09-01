using System;
using System.Collections.Generic;
using SDL2;

namespace Engine.Input
{
    internal class EventProviderSDL
    {
        private IntPtr[] _gameControllers = new IntPtr[Constants.MaxPlayerCount];

        private static readonly Dictionary<int, Button> _keyboardButtonMap = new Dictionary<int, Button>(200)
        {
            { (int)SDL.KeyCode.Space, Button.Space },
            { (int)SDL.KeyCode.Return, Button.Return },
            { (int)SDL.KeyCode.Backspace, Button.Backspace },
            { (int)SDL.KeyCode.Tab, Button.Tab },
            { (int)SDL.KeyCode.Clear, Button.Clear },
            { (int)SDL.KeyCode.Pause, Button.Pause },
            { (int)SDL.KeyCode.Escape, Button.Escape },
            { (int)SDL.KeyCode.Exclaim, Button.Exclaim },
            { (int)SDL.KeyCode.QuoteDouble, Button.DoubleQuote },
            { (int)SDL.KeyCode.Hash, Button.Hash },
            { (int)SDL.KeyCode.Dollar, Button.Dollar },
            { (int)SDL.KeyCode.Ampersand, Button.Ampersand },
            { (int)SDL.KeyCode.Quote, Button.Quote },
            { (int)SDL.KeyCode.LeftParenthesis, Button.LeftParen },
            { (int)SDL.KeyCode.RightParenthesis, Button.RightParen },
            { (int)SDL.KeyCode.Asterisk, Button.Asterisk },
            { (int)SDL.KeyCode.Plus, Button.Plus },
            { (int)SDL.KeyCode.Comma, Button.Comma },
            { (int)SDL.KeyCode.Minus, Button.Minus },
            { (int)SDL.KeyCode.Period, Button.Period },
            { (int)SDL.KeyCode.Slash, Button.Slash },
            { (int)SDL.KeyCode.Colon, Button.Colon },
            { (int)SDL.KeyCode.Semicolon, Button.Semicolon },
            { (int)SDL.KeyCode.Less, Button.Less },
            { (int)SDL.KeyCode.Equals, Button.Equals },
            { (int)SDL.KeyCode.Greater, Button.Greater },
            { (int)SDL.KeyCode.Question, Button.Question },
            { (int)SDL.KeyCode.At, Button.At },
            { (int)SDL.KeyCode.LeftBracket, Button.LeftBracket },
            { (int)SDL.KeyCode.RightBracket, Button.RightBracket },
            { (int)SDL.KeyCode.Backslash, Button.Backslash },
            { (int)SDL.KeyCode.Caret, Button.Caret },
            { (int)SDL.KeyCode.Underscore, Button.Underscore },
            { (int)SDL.KeyCode.Backquote, Button.BackQuote },
            { (int)SDL.KeyCode.Delete, Button.Delete },
            { (int)SDL.KeyCode.Up, Button.UpArrow },
            { (int)SDL.KeyCode.Down, Button.DownArrow },
            { (int)SDL.KeyCode.Left, Button.LeftArrow },
            { (int)SDL.KeyCode.Right, Button.RightArrow },
            { (int)SDL.KeyCode.Insert, Button.Insert },
            { (int)SDL.KeyCode.Home, Button.Home },
            { (int)SDL.KeyCode.End, Button.End },
            { (int)SDL.KeyCode.PageUp, Button.PageUp },
            { (int)SDL.KeyCode.PageDown, Button.PageDown },
            { (int)SDL.KeyCode.NumlockClear, Button.NumLock },
            { (int)SDL.KeyCode.Capslock, Button.CapsLock },
            { (int)SDL.KeyCode.ScrollLock, Button.ScrollLock },
            { (int)SDL.KeyCode.RightShift, Button.RightShift },
            { (int)SDL.KeyCode.LeftShift, Button.LeftShift },
            { (int)SDL.KeyCode.RightControl, Button.RightControl },
            { (int)SDL.KeyCode.LeftControl, Button.LeftControl },
            { (int)SDL.KeyCode.RightAlt, Button.RightAlt },
            { (int)SDL.KeyCode.LeftAlt, Button.LeftAlt },
            { (int)SDL.KeyCode.RightGUI, Button.RightWindowsCommand },
            { (int)SDL.KeyCode.LeftGUI, Button.LeftWindowsCommand },
            { (int)SDL.KeyCode.Help, Button.Help },
            { (int)SDL.KeyCode.Printscreen, Button.Print },
            { (int)SDL.KeyCode.SysReq, Button.SysReq },
            { (int)SDL.KeyCode.Menu, Button.Menu },
            { (int)SDL.KeyCode.a, Button.A },
            { (int)SDL.KeyCode.b, Button.B },
            { (int)SDL.KeyCode.c, Button.C },
            { (int)SDL.KeyCode.d, Button.D },
            { (int)SDL.KeyCode.e, Button.E },
            { (int)SDL.KeyCode.f, Button.F },
            { (int)SDL.KeyCode.g, Button.G },
            { (int)SDL.KeyCode.h, Button.H },
            { (int)SDL.KeyCode.i, Button.I },
            { (int)SDL.KeyCode.j, Button.J },
            { (int)SDL.KeyCode.k, Button.K },
            { (int)SDL.KeyCode.l, Button.L },
            { (int)SDL.KeyCode.m, Button.M },
            { (int)SDL.KeyCode.n, Button.N },
            { (int)SDL.KeyCode.o, Button.O },
            { (int)SDL.KeyCode.p, Button.P },
            { (int)SDL.KeyCode.q, Button.Q },
            { (int)SDL.KeyCode.r, Button.R },
            { (int)SDL.KeyCode.s, Button.S },
            { (int)SDL.KeyCode.t, Button.T },
            { (int)SDL.KeyCode.u, Button.U },
            { (int)SDL.KeyCode.v, Button.V },
            { (int)SDL.KeyCode.w, Button.W },
            { (int)SDL.KeyCode.x, Button.X },
            { (int)SDL.KeyCode.y, Button.Y },
            { (int)SDL.KeyCode.z, Button.Z },
            { (int)SDL.KeyCode.Alpha0, Button.Alpha0 },
            { (int)SDL.KeyCode.Alpha1, Button.Alpha1 },
            { (int)SDL.KeyCode.Alpha2, Button.Alpha2 },
            { (int)SDL.KeyCode.Alpha3, Button.Alpha3 },
            { (int)SDL.KeyCode.Alpha4, Button.Alpha4 },
            { (int)SDL.KeyCode.Alpha5, Button.Alpha5 },
            { (int)SDL.KeyCode.Alpha6, Button.Alpha6 },
            { (int)SDL.KeyCode.Alpha7, Button.Alpha7 },
            { (int)SDL.KeyCode.Alpha8, Button.Alpha8 },
            { (int)SDL.KeyCode.Alpha9, Button.Alpha9 },
            { (int)SDL.KeyCode.Num0, Button.Keypad0 },
            { (int)SDL.KeyCode.Num1, Button.Keypad1 },
            { (int)SDL.KeyCode.Num2, Button.Keypad2 },
            { (int)SDL.KeyCode.Num3, Button.Keypad3 },
            { (int)SDL.KeyCode.Num4, Button.Keypad4 },
            { (int)SDL.KeyCode.Num5, Button.Keypad5 },
            { (int)SDL.KeyCode.Num6, Button.Keypad6 },
            { (int)SDL.KeyCode.Num7, Button.Keypad7 },
            { (int)SDL.KeyCode.Num8, Button.Keypad8 },
            { (int)SDL.KeyCode.Num9, Button.Keypad9 },
            { (int)SDL.KeyCode.NumPeriod, Button.KeypadPeriod },
            { (int)SDL.KeyCode.NumDivide, Button.KeypadDivide },
            { (int)SDL.KeyCode.NumMultiply, Button.KeypadMultiply },
            { (int)SDL.KeyCode.NumMinus, Button.KeypadMinus },
            { (int)SDL.KeyCode.NumPlus, Button.KeypadPlus },
            { (int)SDL.KeyCode.NumEnter, Button.KeypadEnter },
            { (int)SDL.KeyCode.NumEquals, Button.KeypadEquals },
            { (int)SDL.KeyCode.F1, Button.F1 },
            { (int)SDL.KeyCode.F2, Button.F2 },
            { (int)SDL.KeyCode.F3, Button.F3 },
            { (int)SDL.KeyCode.F4, Button.F4 },
            { (int)SDL.KeyCode.F5, Button.F5 },
            { (int)SDL.KeyCode.F6, Button.F6 },
            { (int)SDL.KeyCode.F7, Button.F7 },
            { (int)SDL.KeyCode.F8, Button.F8 },
            { (int)SDL.KeyCode.F9, Button.F9 },
            { (int)SDL.KeyCode.F10, Button.F10 },
            { (int)SDL.KeyCode.F11, Button.F11 },
            { (int)SDL.KeyCode.F12, Button.F12 },
            { (int)SDL.KeyCode.F13, Button.F13 },
            { (int)SDL.KeyCode.F14, Button.F14 },
            { (int)SDL.KeyCode.F15, Button.F15 },
        };
        private static readonly Dictionary<byte, Button> _mouseButtonMap = new Dictionary<byte, Button>(5)
        {
            { (byte)SDL.MouseButton.Left, Button.MouseLeft },
            { (byte)SDL.MouseButton.Middle, Button.MouseMiddle },
            { (byte)SDL.MouseButton.Right, Button.MouseRight },
            { (byte)SDL.MouseButton.X1, Button.MouseExtra1 },
            { (byte)SDL.MouseButton.X2, Button.MouseExtra2 },
        };
        private static readonly Dictionary<int, Button> _gamepadButtonMap = new Dictionary<int, Button>(15)
        {
            { (int)SDL.GameControllerButton.A, Button.GamepadA },
            { (int)SDL.GameControllerButton.B, Button.GamepadB },
            { (int)SDL.GameControllerButton.X, Button.GamepadX },
            { (int)SDL.GameControllerButton.Y, Button.GamepadY },
            { (int)SDL.GameControllerButton.Back, Button.GamepadBack },
            { (int)SDL.GameControllerButton.Guide, Button.GamepadGuide },
            { (int)SDL.GameControllerButton.Start, Button.GamepadStart },
            { (int)SDL.GameControllerButton.LeftStick, Button.GamepadThumbLeft },
            { (int)SDL.GameControllerButton.RightStick, Button.GamepadThumbRight },
            { (int)SDL.GameControllerButton.LeftShoulder, Button.GamepadTriggerLeft },
            { (int)SDL.GameControllerButton.RightShoulder, Button.GamepadTriggerRight },
            { (int)SDL.GameControllerButton.DPadUp, Button.GamepadUp },
            { (int)SDL.GameControllerButton.DPadDown, Button.GamepadDown },
            { (int)SDL.GameControllerButton.DPadLeft, Button.GamepadLeft },
            { (int)SDL.GameControllerButton.DPadRight, Button.GamepadRight },
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
                        global::Game.Instance.Quit();
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
                                KeyEvent = new ButtonEvent
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
                                KeyEvent = new ButtonEvent
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
    }
}