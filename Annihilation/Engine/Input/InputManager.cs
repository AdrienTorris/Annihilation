using System;
using System.Numerics;
using System.Collections.Generic;
using SDL2;

namespace Engine.Input
{
    public static class InputManager
    {
        public static IInputHandler InputHandler;

        public static IReadOnlyList<KeyEvent> KeyEvents => _keyEvents;
        public static IReadOnlyList<MouseButtonEvent> MouseButtonEvents => _mouseButtonEvents;
        public static IReadOnlyList<MouseWheelEvent> MouseWheelEvents => _mouseWheelEvents;
        public static float MouseWheelDelta { get; private set; }
        public static IReadOnlyList<MouseMoveEvent> MouseMoveEvents => _mouseMoveEvents;
        public static Vector2 MousePosition { get; private set; }
        public static Vector2 MouseDelta { get; private set; }

        private static readonly List<KeyEvent> _keyEvents = new List<KeyEvent>();
        private static readonly HashSet<SDL.KeyCode> _pressedKeys = new HashSet<SDL.KeyCode>();
        private static readonly HashSet<SDL.KeyCode> _releasedKeys = new HashSet<SDL.KeyCode>();
        private static readonly HashSet<SDL.KeyCode> _downKeys = new HashSet<SDL.KeyCode>();
        private static readonly Dictionary<SDL.KeyCode, int> _keyRepeats = new Dictionary<SDL.KeyCode, int>();

        private static readonly List<MouseButtonEvent> _mouseButtonEvents = new List<MouseButtonEvent>();
        // PERF: Check if enum : byte as key is slow
        private static readonly HashSet<SDL.MouseButton> _pressedMouseButtons = new HashSet<SDL.MouseButton>();
        private static readonly HashSet<SDL.MouseButton> _releasedMouseButtons = new HashSet<SDL.MouseButton>();
        private static readonly HashSet<SDL.MouseButton> _downMouseButtons = new HashSet<SDL.MouseButton>();
        private static readonly Dictionary<SDL.MouseButton, int> _mouseButtonRepeats = new Dictionary<SDL.MouseButton, int>();

        private static readonly List<MouseWheelEvent> _mouseWheelEvents = new List<MouseWheelEvent>();

        private static readonly List<MouseMoveEvent> _mouseMoveEvents = new List<MouseMoveEvent>();

        public static void Init()
        {
            SDL.InitSubSystem(SDL.InitFlags.Events);
        }

        public static void Shutdown()
        {
            SDL.QuitSubSystem(SDL.InitFlags.Events);
        }

        public static bool WasKeyPressed(SDL.KeyCode key) => _pressedKeys.Contains(key);
        public static bool WasKeyReleased(SDL.KeyCode key) => _releasedKeys.Contains(key);
        public static bool IsKeyDown(SDL.KeyCode key) => _downKeys.Contains(key);

        public static bool WasMouseButtonPressed(SDL.MouseButton mouseButton) => _pressedMouseButtons.Contains(mouseButton);
        public static bool WasMouseButtonReleased(SDL.MouseButton mouseButton) => _releasedMouseButtons.Contains(mouseButton);
        public static bool IsMouseButtonDown(SDL.MouseButton mouseButton) => _downMouseButtons.Contains(mouseButton);

        public static void EnableTextInput() => SDL.StartTextInput();
        public static void DisableTextInput() => SDL.StopTextInput();

        public static void Update()
        {
            _keyEvents.Clear();
            _pressedKeys.Clear();
            _releasedKeys.Clear();

            _mouseButtonEvents.Clear();
            _pressedMouseButtons.Clear();
            _releasedMouseButtons.Clear();

            _mouseWheelEvents.Clear();

            _mouseMoveEvents.Clear();

            while (SDL.PollEvent(out SDL.Event @event) != 0)
            {
                switch (@event.Type)
                {
                    case SDL.EventType.Quit:
                    {
                        Game.Quit();
                        break;
                    }
                    case SDL.EventType.WindowEvent:
                    {
                        switch (@event.Window.Event)
                        {
                            case SDL.WindowEventID.FocusLost:
                            {
                                Game.Window.HasFocus = false;
                                break;
                            }
                            case SDL.WindowEventID.FocusGained:
                            {
                                Game.Window.HasFocus = true;
                                break;
                            }
                            case SDL.WindowEventID.SizeChanged:
                            {
                                break;
                            }
                        }
                        break;
                    }
                    case SDL.EventType.MouseButtonDown:
                    {
                        SDL.MouseButton button = @event.MouseButton.Button;

                        if (_downMouseButtons.Contains(button))
                        {
                            return;
                        }

                        _pressedMouseButtons.Add(button);
                        _downMouseButtons.Add(button);

                        MouseButtonEvent mouseButtonEvent = new MouseButtonEvent(button, ButtonState.Pressed, @event.MouseButton.Clicks == 2);
                        _mouseButtonEvents.Add(mouseButtonEvent);

                        InputHandler?.OnMouseButtonInput(ref mouseButtonEvent);

                        break;
                    }
                    case SDL.EventType.MouseButtonUp:
                    {
                        SDL.MouseButton button = @event.MouseButton.Button;

                        if (_downMouseButtons.Contains(button) == false)
                        {
                            return;
                        }

                        _releasedMouseButtons.Add(button);
                        _downMouseButtons.Remove(button);

                        MouseButtonEvent mouseButtonEvent = new MouseButtonEvent(button, ButtonState.Released, false);
                        _mouseButtonEvents.Add(mouseButtonEvent);

                        InputHandler?.OnMouseButtonInput(ref mouseButtonEvent);

                        break;
                    }
                    case SDL.EventType.MouseMotion:
                    {
                        Vector2 deltaPosition = new Vector2(@event.MouseMotion.XRel, @event.MouseMotion.YRel);

                        if (deltaPosition == Vector2.Zero)
                        {
                            return;
                        }

                        Vector2 newPosition = new Vector2(@event.MouseMotion.X, @event.MouseMotion.Y);

                        // TODO: Get proper delta time
                        MouseMoveEvent mouseMoveEvent = new MouseMoveEvent(newPosition, deltaPosition, TimeSpan.Zero);
                        _mouseMoveEvents.Add(mouseMoveEvent);

                        InputHandler?.OnMouseMoveInput(ref mouseMoveEvent);

                        if (newPosition != MousePosition)
                        {
                            MousePosition = newPosition;
                        }

                        break;
                    }
                    case SDL.EventType.MouseWheel:
                    {
                        float delta = @event.MouseWheel.Y;
                        MouseWheelEvent mouseWheelEvent = new MouseWheelEvent(delta);
                        _mouseWheelEvents.Add(mouseWheelEvent);

                        if (Math.Abs(delta) > Math.Abs(MouseWheelDelta))
                        {
                            MouseWheelDelta = delta;
                        }

                        InputHandler?.OnMouseWheelInput(ref mouseWheelEvent);

                        break;
                    }
                    case SDL.EventType.TextInput:
                    {
                        break;
                    }
                    case SDL.EventType.KeyDown:
                    {
                        SDL.KeyCode key = @event.Key.KeySym.Sym;

                        if (_keyRepeats.TryGetValue(key, out int repeatCount))
                        {
                            _keyRepeats[key] = ++repeatCount;
                        }
                        else
                        {
                            _keyRepeats.Add(key, repeatCount);
                            _pressedKeys.Add(key);
                            _downKeys.Add(key);
                        }

                        // PERF: Pool these events instead of creating them
                        KeyEvent keyEvent = new KeyEvent(key, ButtonState.Pressed, repeatCount);
                        _keyEvents.Add(keyEvent);

                        InputHandler?.OnKeyInput(ref keyEvent);

                        break;
                    }
                    case SDL.EventType.KeyUp:
                    {
                        SDL.KeyCode key = @event.Key.KeySym.Sym;

                        if (_keyRepeats.ContainsKey(key) == false)
                        {
                            return;
                        }

                        _keyRepeats.Remove(key);
                        _releasedKeys.Add(key);
                        _downKeys.Remove(key);

                        KeyEvent keyEvent = new KeyEvent(key, ButtonState.Released, 0);
                        _keyEvents.Add(keyEvent);

                        InputHandler?.OnKeyInput(ref keyEvent);

                        break;
                    }
                }
            }
        }
    }
}