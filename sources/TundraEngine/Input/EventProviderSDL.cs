using System;
using System.Collections.Generic;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Input
{
    internal class EventProviderSDL : LibrarySystem<LibSDL>, IEventProvider
    {
        private IntPtr[] _gameControllers = new IntPtr[Constants.MaxPlayerCount];

        private static Dictionary<SDL_KeyCode, Button> _keyMap = new Dictionary<SDL_KeyCode, Button>(200)
        {
            { SDL_KeyCode.Unknown, Button.None },
        };

        public uint ConnectedControllerCount { get; private set; }
        
        public void PumpEvents(out InputEvent inputEvent)
        {
            inputEvent = new InputEvent
            {
                Type = InputEventType.None
            };

            while (SDL_PollEvent(out SDL_Event sdlEvent) == 1)
            {
                if (sdlEvent.Type == SDL_EventType.KeyDown ||
                    sdlEvent.Type == SDL_EventType.KeyUp)
                {
                    ButtonEvent buttonEvent = new ButtonEvent
                    {
                        Button = Button.None,
                        PlayerId = 0
                    };

                    // TODO: Add the missing keys
                    // TODO: Put these in a static dictionary
                    switch (sdlEvent.Key.KeySym.Sym)
                    {
                        case SDL_KeyCode.a:
                            buttonEvent.Button = Button.A;
                            break;
                        case SDL_KeyCode.b:
                            buttonEvent.Button = Button.B;
                            break;
                        case SDL_KeyCode.c:
                            buttonEvent.Button = Button.C;
                            break;
                        case SDL_KeyCode.d:
                            buttonEvent.Button = Button.D;
                            break;
                        case SDL_KeyCode.e:
                            buttonEvent.Button = Button.E;
                            break;
                        case SDL_KeyCode.f:
                            buttonEvent.Button = Button.F;
                            break;
                        case SDL_KeyCode.g:
                            buttonEvent.Button = Button.G;
                            break;
                        case SDL_KeyCode.h:
                            buttonEvent.Button = Button.H;
                            break;
                        case SDL_KeyCode.i:
                            buttonEvent.Button = Button.I;
                            break;
                        case SDL_KeyCode.j:
                            buttonEvent.Button = Button.J;
                            break;
                        case SDL_KeyCode.k:
                            buttonEvent.Button = Button.K;
                            break;
                        case SDL_KeyCode.l:
                            buttonEvent.Button = Button.L;
                            break;
                        case SDL_KeyCode.m:
                            buttonEvent.Button = Button.M;
                            break;
                        case SDL_KeyCode.n:
                            buttonEvent.Button = Button.N;
                            break;
                        case SDL_KeyCode.o:
                            buttonEvent.Button = Button.O;
                            break;
                        case SDL_KeyCode.p:
                            buttonEvent.Button = Button.P;
                            break;
                        case SDL_KeyCode.q:
                            buttonEvent.Button = Button.Q;
                            break;
                        case SDL_KeyCode.r:
                            buttonEvent.Button = Button.R;
                            break;
                        case SDL_KeyCode.s:
                            buttonEvent.Button = Button.S;
                            break;
                        case SDL_KeyCode.t:
                            buttonEvent.Button = Button.T;
                            break;
                        case SDL_KeyCode.u:
                            buttonEvent.Button = Button.U;
                            break;
                        case SDL_KeyCode.v:
                            buttonEvent.Button = Button.V;
                            break;
                        case SDL_KeyCode.w:
                            buttonEvent.Button = Button.W;
                            break;
                        case SDL_KeyCode.x:
                            buttonEvent.Button = Button.X;
                            break;
                        case SDL_KeyCode.y:
                            buttonEvent.Button = Button.Y;
                            break;
                        case SDL_KeyCode.z:
                            buttonEvent.Button = Button.Z;
                            break;
                        case SDL_KeyCode.Alpha0:
                            buttonEvent.Button = Button.Alpha0;
                            break;
                        case SDL_KeyCode.Alpha1:
                            buttonEvent.Button = Button.Alpha1;
                            break;
                        case SDL_KeyCode.Alpha2:
                            buttonEvent.Button = Button.Alpha2;
                            break;
                        case SDL_KeyCode.Alpha3:
                            buttonEvent.Button = Button.Alpha3;
                            break;
                        case SDL_KeyCode.Alpha4:
                            buttonEvent.Button = Button.Alpha4;
                            break;
                        case SDL_KeyCode.Alpha5:
                            buttonEvent.Button = Button.Alpha5;
                            break;
                        case SDL_KeyCode.Alpha6:
                            buttonEvent.Button = Button.Alpha6;
                            break;
                        case SDL_KeyCode.Alpha7:
                            buttonEvent.Button = Button.Alpha7;
                            break;
                        case SDL_KeyCode.Alpha8:
                            buttonEvent.Button = Button.Alpha8;
                            break;
                        case SDL_KeyCode.Alpha9:
                            buttonEvent.Button = Button.Alpha9;
                            break;
                        case SDL_KeyCode.Num0:
                            buttonEvent.Button = Button.Keypad0;
                            break;
                        case SDL_KeyCode.Num1:
                            buttonEvent.Button = Button.Keypad1;
                            break;
                        case SDL_KeyCode.Num2:
                            buttonEvent.Button = Button.Keypad2;
                            break;
                        case SDL_KeyCode.Num3:
                            buttonEvent.Button = Button.Keypad3;
                            break;
                        case SDL_KeyCode.Num4:
                            buttonEvent.Button = Button.Keypad4;
                            break;
                        case SDL_KeyCode.Num5:
                            buttonEvent.Button = Button.Keypad5;
                            break;
                        case SDL_KeyCode.Num6:
                            buttonEvent.Button = Button.Keypad6;
                            break;
                        case SDL_KeyCode.Num7:
                            buttonEvent.Button = Button.Keypad7;
                            break;
                        case SDL_KeyCode.Num8:
                            buttonEvent.Button = Button.Keypad8;
                            break;
                        case SDL_KeyCode.Num9:
                            buttonEvent.Button = Button.Keypad9;
                            break;
                        case SDL_KeyCode.Up:
                            buttonEvent.Button = Button.UpArrow;
                            break;
                        case SDL_KeyCode.Down:
                            buttonEvent.Button = Button.DownArrow;
                            break;
                        case SDL_KeyCode.Left:
                            buttonEvent.Button = Button.LeftArrow;
                            break;
                        case SDL_KeyCode.Right:
                            buttonEvent.Button = Button.RightArrow;
                            break;
                        default:
                            return;
                    }

                    buttonEvent.State = sdlEvent.Type == SDL_EventType.KeyDown ? ButtonState.Pressed : ButtonState.Released;
                    inputEvent.Type = InputEventType.Button;
                    inputEvent.ButtonEvent = buttonEvent;
                    return;
                }
                else if (sdlEvent.Type == SDL_EventType.MouseButtonDown ||
                         sdlEvent.Type == SDL_EventType.MouseButtonUp)
                {

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