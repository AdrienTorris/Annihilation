using System.Numerics;

namespace TundraEngine.Input
{
    public class InputSystem
    {
        private IEventProvider _eventProvider;

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

        internal InputSystem(IEventProvider eventProvider)
        {
            _eventProvider = eventProvider;
        }

        internal void Update()
        {
            _eventProvider.PollEvents();
        }
    }
}