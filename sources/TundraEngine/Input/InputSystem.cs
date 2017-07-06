using System.Numerics;

namespace TundraEngine.Input
{
    public class InputSystem
    {
        private bool[][] _buttons = new bool[2][]
        {
            new bool[(int)Button.NumButtons],
            new bool[(int)Button.NumButtons]
        };
        private float[] _holdDurations = new float[(int)Button.NumButtons];

        private static Vector2 _lastMousePos;
        private static Vector2 _lastMousePosViewport;
        private static Vector2 _lastMousePosRelative;

        private static float[][] _actionStates = new float[Constants.MaxPlayerCount][];

        internal InputSystem()
        {
        }
    }
}