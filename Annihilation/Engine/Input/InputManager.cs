using System.Collections.Generic;
using SDL2;

namespace Engine.Input
{
    public static class InputManager
    {
        public static IInputHandler InputHandler;

        public static readonly HashSet<SDL.KeyCode> PressedKeys = new HashSet<SDL.KeyCode>();
        public static readonly HashSet<SDL.KeyCode> ReleasedKeys = new HashSet<SDL.KeyCode>();
        public static readonly HashSet<SDL.KeyCode> DownKeys = new HashSet<SDL.KeyCode>();
        public static readonly Dictionary<SDL.KeyCode, int> KeyRepeats = new Dictionary<SDL.KeyCode, int>();

        private static readonly List<InputEvent> _events = new List<InputEvent>();

        public static void Init()
        {
            SDL.InitSubSystem(SDL.InitFlags.Events);
        }

        public static void Update()
        {
            _events.Clear();

            SDL.KeyMod keyMod = SDL.GetModState();
            float scrollDelta = 0f;

            while (SDL.PollEvent(out SDL.Event @event) != 0)
            {
                switch (@event.Type)
                {
                    case SDL.EventType.Quit:

                        Game.Quit();
                        break;

                    case SDL.EventType.WindowEvent:
                        switch (@event.Window.Event)
                        {
                            case SDL.WindowEventID.FocusLost:

                                Game.Window.HasFocus = false;
                                break;

                            case SDL.WindowEventID.FocusGained:

                                Game.Window.HasFocus = true;
                                break;

                            case SDL.WindowEventID.SizeChanged:

                                break;
                        }
                        break;

                    case SDL.EventType.MouseButtonDown:

                        break;

                    case SDL.EventType.MouseButtonUp:

                        break;

                    case SDL.EventType.MouseMotion:

                        break;

                    case SDL.EventType.MouseWheel:

                        SDL.GetMouseState(out int x, out int y);
                        scrollDelta = @event.MouseWheel.Y;
                        break;

                    case SDL.EventType.TextInput:

                        break;

                    case SDL.EventType.KeyDown:

                        SDL.KeyCode key = @event.Key.KeySym.Sym;
                        if (KeyRepeats.TryGetValue(key, out int repeatCount))
                        {
                            KeyRepeats[key] = ++repeatCount;
                        }
                        else
                        {
                            KeyRepeats.Add(key, repeatCount);
                            DownKeys.Add(key);
                        }

                        InputHandler.OnKeyInput();

                        break;

                    case SDL.EventType.KeyUp:


                        break;
                }
            }
        }

        public static void EnableTextInput() => SDL.StartTextInput();

        public static void DisableTextInput() => SDL.StopTextInput();



        public static void Shutdown()
        {
            SDL.QuitSubSystem(SDL.InitFlags.Events);
        }
    }
}