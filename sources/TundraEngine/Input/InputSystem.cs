using System;

namespace TundraEngine.Input
{
    public class InputSystem
    {
        private static IEventProvider _eventProvider;
        

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