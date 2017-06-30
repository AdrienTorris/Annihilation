using System;
using System.Collections.Generic;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Input
{
    internal class EventProviderSDL : LibrarySystem<LibSDL>, IEventProvider
    {
        private IntPtr[] _gameControllers = new IntPtr[Constants.MaxPlayerCount];

        private static readonly Dictionary<SDL_KeyCode, Button> _keyboardButtonMap = new Dictionary<SDL_KeyCode, Button>(200)
        {
            { SDL_KeyCode.Unknown, Button.None },
            { SDL_KeyCode.Space, Button.Space },
            { SDL_KeyCode.Return, Button.Return },
            { SDL_KeyCode.Backspace, Button.Backspace },
            { SDL_KeyCode.Tab, Button.Tab },
            { SDL_KeyCode.Clear, Button.Clear },
            { SDL_KeyCode.Pause, Button.Pause },
            { SDL_KeyCode.Escape, Button.Escape },
            { SDL_KeyCode.Exclaim, Button.Exclaim },
            { SDL_KeyCode.QuoteDouble, Button.DoubleQuote },
            { SDL_KeyCode.Hash, Button.Hash },
            { SDL_KeyCode.Dollar, Button.Dollar },
            { SDL_KeyCode.Ampersand, Button.Ampersand },
            { SDL_KeyCode.Quote, Button.Quote },
            { SDL_KeyCode.LeftParenthesis, Button.LeftParen },
            { SDL_KeyCode.RightParenthesis, Button.RightParen },
            { SDL_KeyCode.Asterisk, Button.Asterisk },
            { SDL_KeyCode.Plus, Button.Plus },
            { SDL_KeyCode.Comma, Button.Comma },
            { SDL_KeyCode.Minus, Button.Minus },
            { SDL_KeyCode.Period, Button.Period },
            { SDL_KeyCode.Slash, Button.Slash },
            { SDL_KeyCode.Colon, Button.Colon },
            { SDL_KeyCode.Semicolon, Button.Semicolon },
            { SDL_KeyCode.Less, Button.Less },
            { SDL_KeyCode.Equals, Button.Equals },
            { SDL_KeyCode.Greater, Button.Greater },
            { SDL_KeyCode.Question, Button.Question },
            { SDL_KeyCode.At, Button.At },
            { SDL_KeyCode.LeftBracket, Button.LeftBracket },
            { SDL_KeyCode.RightBracket, Button.RightBracket },
            { SDL_KeyCode.Backslash, Button.Backslash },
            { SDL_KeyCode.Caret, Button.Caret },
            { SDL_KeyCode.Underscore, Button.Underscore },
            { SDL_KeyCode.Backquote, Button.BackQuote },
            { SDL_KeyCode.Delete, Button.Delete },
            { SDL_KeyCode.Up, Button.UpArrow },
            { SDL_KeyCode.Down, Button.DownArrow },
            { SDL_KeyCode.Left, Button.LeftArrow },
            { SDL_KeyCode.Right, Button.RightArrow },
            { SDL_KeyCode.Insert, Button.Insert },
            { SDL_KeyCode.Home, Button.Home },
            { SDL_KeyCode.End, Button.End },
            { SDL_KeyCode.PageUp, Button.PageUp },
            { SDL_KeyCode.PageDown, Button.PageDown },
            { SDL_KeyCode.NumlockClear, Button.NumLock },
            { SDL_KeyCode.Capslock, Button.CapsLock },
            { SDL_KeyCode.ScrollLock, Button.ScrollLock },
            { SDL_KeyCode.RightShift, Button.RightShift },
            { SDL_KeyCode.LeftShift, Button.LeftShift },
            { SDL_KeyCode.RightControl, Button.RightControl },
            { SDL_KeyCode.LeftControl, Button.LeftControl },
            { SDL_KeyCode.RightAlt, Button.RightAlt },
            { SDL_KeyCode.LeftAlt, Button.LeftAlt },
            { SDL_KeyCode.RightGUI, Button.RightWindowsCommand },
            { SDL_KeyCode.LeftGUI, Button.LeftWindowsCommand },
            { SDL_KeyCode.Help, Button.Help },
            { SDL_KeyCode.Printscreen, Button.Print },
            { SDL_KeyCode.SysReq, Button.SysReq },
            { SDL_KeyCode.Pause, Button.Break },
            { SDL_KeyCode.Menu, Button.Menu },
            { SDL_KeyCode.a, Button.A },
            { SDL_KeyCode.b, Button.B },
            { SDL_KeyCode.c, Button.C },
            { SDL_KeyCode.d, Button.D },
            { SDL_KeyCode.e, Button.E },
            { SDL_KeyCode.f, Button.F },
            { SDL_KeyCode.g, Button.G },
            { SDL_KeyCode.h, Button.H },
            { SDL_KeyCode.i, Button.I },
            { SDL_KeyCode.j, Button.J },
            { SDL_KeyCode.k, Button.K },
            { SDL_KeyCode.l, Button.L },
            { SDL_KeyCode.m, Button.M },
            { SDL_KeyCode.n, Button.N },
            { SDL_KeyCode.o, Button.O },
            { SDL_KeyCode.p, Button.P },
            { SDL_KeyCode.q, Button.Q },
            { SDL_KeyCode.r, Button.R },
            { SDL_KeyCode.s, Button.S },
            { SDL_KeyCode.t, Button.T },
            { SDL_KeyCode.u, Button.U },
            { SDL_KeyCode.v, Button.V },
            { SDL_KeyCode.w, Button.W },
            { SDL_KeyCode.x, Button.X },
            { SDL_KeyCode.y, Button.Y },
            { SDL_KeyCode.z, Button.Z },
            { SDL_KeyCode.Alpha0, Button.Alpha0 },
            { SDL_KeyCode.Alpha1, Button.Alpha1 },
            { SDL_KeyCode.Alpha2, Button.Alpha2 },
            { SDL_KeyCode.Alpha3, Button.Alpha3 },
            { SDL_KeyCode.Alpha4, Button.Alpha4 },
            { SDL_KeyCode.Alpha5, Button.Alpha5 },
            { SDL_KeyCode.Alpha6, Button.Alpha6 },
            { SDL_KeyCode.Alpha7, Button.Alpha7 },
            { SDL_KeyCode.Alpha8, Button.Alpha8 },
            { SDL_KeyCode.Alpha9, Button.Alpha9 },
            { SDL_KeyCode.Num0, Button.Keypad0 },
            { SDL_KeyCode.Num1, Button.Keypad1 },
            { SDL_KeyCode.Num2, Button.Keypad2 },
            { SDL_KeyCode.Num3, Button.Keypad3 },
            { SDL_KeyCode.Num4, Button.Keypad4 },
            { SDL_KeyCode.Num5, Button.Keypad5 },
            { SDL_KeyCode.Num6, Button.Keypad6 },
            { SDL_KeyCode.Num7, Button.Keypad7 },
            { SDL_KeyCode.Num8, Button.Keypad8 },
            { SDL_KeyCode.Num9, Button.Keypad9 },
            { SDL_KeyCode.NumPeriod, Button.KeypadPeriod },
            { SDL_KeyCode.NumDivide, Button.KeypadDivide },
            { SDL_KeyCode.NumMultiply, Button.KeypadMultiply },
            { SDL_KeyCode.NumMinus, Button.KeypadMinus },
            { SDL_KeyCode.NumPlus, Button.KeypadPlus },
            { SDL_KeyCode.NumEnter, Button.KeypadEnter },
            { SDL_KeyCode.NumEquals, Button.KeypadEquals },
            { SDL_KeyCode.F1, Button.F1 },
            { SDL_KeyCode.F2, Button.F2 },
            { SDL_KeyCode.F3, Button.F3 },
            { SDL_KeyCode.F4, Button.F4 },
            { SDL_KeyCode.F5, Button.F5 },
            { SDL_KeyCode.F6, Button.F6 },
            { SDL_KeyCode.F7, Button.F7 },
            { SDL_KeyCode.F8, Button.F8 },
            { SDL_KeyCode.F9, Button.F9 },
            { SDL_KeyCode.F10, Button.F10 },
            { SDL_KeyCode.F11, Button.F11 },
            { SDL_KeyCode.F12, Button.F12 },
            { SDL_KeyCode.F13, Button.F13 },
            { SDL_KeyCode.F14, Button.F14 },
            { SDL_KeyCode.F15, Button.F15 },
        };

        private static readonly Dictionary<SDL_MouseButton, Button> _mouseButtonMap = new Dictionary<SDL_MouseButton, Button>(8)
        {
            { SDL_MouseButton.Left, Button.MouseLeft },
            { SDL_MouseButton.Middle, Button.MouseMiddle },
            { SDL_MouseButton.Right, Button.MouseRight },
            { SDL_MouseButton.X1, Button.MouseExtra1 },
            { SDL_MouseButton.X2, Button.MouseExtra2 },
            { SDL_MouseButton.LeftDouble, Button.MouseLeftDouble },
            { SDL_MouseButton.MiddleDouble, Button.MouseMiddleDouble },
            { SDL_MouseButton.RightDouble, Button.MouseRightDouble }
        };

        public uint ConnectedControllerCount { get; private set; }

        public void PollEvents()
        {
            while (SDL_PollEvent(out SDL_Event sdlEvent) == 1)
            {
                switch (sdlEvent.Type)
                {
                    case SDL_EventType.Quit:
                    Application.Quit();
                    break;

                    case SDL_EventType.KeyDown:
                    case SDL_EventType.KeyUp:
                    {
                        // Get the pressed or released button
                        if (_keyboardButtonMap.TryGetValue(sdlEvent.Key.KeySym.Sym, out Button button) == false)
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
                    }
                    break;

                    case SDL_EventType.MouseButtonDown:
                    case SDL_EventType.MouseButtonUp:
                    {
                        // Get the pressed or released button
                        if (_mouseButtonMap.TryGetValue(sdlEvent.MouseButton.Button, out Button button) == false)
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
                    }
                    break;
                }
            }
        }

        protected override void InitializeLibrary()
        {
            Application.InitializeSDL();
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