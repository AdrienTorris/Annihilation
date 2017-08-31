using SDL2;

namespace Engine.Input
{
    public static class InputManager
    {
        public static bool HasFocus = false;

        public static void Update()
        {
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
                                HasFocus = false;
                                break;

                            case SDL.WindowEventID.FocusGained:
                                HasFocus = true;
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
                    case SDL.EventType.KeyUp:

                        break;
                }
            }
        }
    }
}