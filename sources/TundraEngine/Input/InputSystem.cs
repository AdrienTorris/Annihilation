using System;
using System.Collections.Generic;
using System.Numerics;

namespace TundraEngine.Input
{
    public class InputSystem
    {
        private IEventProvider _eventProvider;
        static InputSystem instance;
        /*
        private bool[][] _buttons = new bool[2][]
        {
            new bool[(int)Button.NumButtons],
            new bool[(int)Button.NumButtons]
        };
        private float[] _holdDurations = new float[(int)Button.NumButtons];

        private Vector2 _lastMousePos;
        private Vector2 _lastMousePosViewport;
        private Vector2 _lastMousePosRelative;

        private float[][] _actionStates = new float[Constants.MaxPlayerCount][];
        */

        List<Button> ButtonDown = new List<Button>();
        List<Button> ButtonPressed = new List<Button>();

        internal InputSystem(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
            instance = this;
        }


        public static bool GetKeyDown(Button button)
        {
            if (instance.ButtonDown.Contains(button))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static bool GetKey(Button button)
        {
            if (instance.ButtonPressed.Contains(button))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        internal void Update()
        {
            CleanUp();
            _eventProvider.PollEvents(out List<InputEvent> inputEvents);

            for (int i = 0; i < inputEvents.Count; i++)
            {
                if (inputEvents[i].Type == InputEventType.Button)
                {
                    if (inputEvents[i].ButtonEvent.State == ButtonState.Pressed)
                    {
                        ButtonDown.Add(inputEvents[i].ButtonEvent.Button);
                    }
                    if (inputEvents[i].ButtonEvent.State == ButtonState.Released)
                    {
                      //  ButtonPressed.Remove(inputEvents[i].ButtonEvent.Button);
                        ButtonPressed.RemoveAll(item => item == inputEvents[i].ButtonEvent.Button);
                    }
                }
            }


            ButtonPressed.AddRange(ButtonDown);
        }

        private void CleanUp()
        {
            ButtonDown.Clear();
        }
    }
}