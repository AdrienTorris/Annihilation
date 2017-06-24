using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine.Input
{
    public class InputSystem
    {
        private static IEventProvider _eventProvider;

        private static ButtonState[] _buttonStates = new ButtonState[(int)Button.Count];
        private static float[] _axisStates = new float[(int)Axis.Count];

        private static Vector2 _lastMousePos;
        private static Vector2 _lastMousePosViewport;
        private static Vector2 _lastMousePosRelative;

        private static float[][] _actionStates = new float[Constants.MaxPlayerCount][];

        internal InputSystem(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        public static void ProcessEvents()
        {
            _eventProvider.PumpEvents(out InputEvent inputEvent);

            switch(inputEvent.Type)
            {
                case InputEventType.Button:

                    break;
                case InputEventType.MouseMove:

                    break;
                case InputEventType.Axis:

                    break;
            }
        }
    }
}