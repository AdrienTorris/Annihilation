using System;

using static TundraEngine.SDL.SDL;

namespace TundraEngine.Input
{
    public class EventProviderSDL : IEventProvider
    {
        private IntPtr[] _gameControllers = new IntPtr[Constants.MaxPlayerCount];

        public uint ConnectedControllerCount { get; private set; }

        public void PumpEvents(out InputEvent inputEvent)
        {
            inputEvent = new InputEvent
            {
                Type = InputEventType.None
            };

            SDL_Event sdlEvent;
            while (SDL_PollEvent(out sdlEvent) == 1)
            {
                if (sdlEvent.Type == SDL_EventType.KeyDown ||
                    sdlEvent.Type == SDL_EventType.KeyUp)
                {
                    ButtonEvent buttonEvent = new ButtonEvent
                    {
                        Button = Button.None,
                        PlayerId = 0
                    };

                    switch (sdlEvent.Key.KeySym.Sym)
                    {
                        case SDL_KeyCode.Up:
                            buttonEvent.Button = Button.Up;
                            break;
                        case SDL_KeyCode.Down:
                            buttonEvent.Button = Button.Down;
                            break;
                        case SDL_KeyCode.Left:
                            buttonEvent.Button = Button.Left;
                            break;
                        case SDL_KeyCode.Right:
                            buttonEvent.Button = Button.Right;
                            break;
                        default:
                            return;
                    }
                    
                    buttonEvent.State = sdlEvent.Type == SDL_EventType.KeyDown ? ButtonState.Pressed  : ButtonState.Released;
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
    }
}