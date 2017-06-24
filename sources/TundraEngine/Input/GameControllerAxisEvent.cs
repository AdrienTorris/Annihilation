using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct GameControllerAxisEvent
    {
        public GameControllerAxis Axis;
        public float Value;
        public byte PlayerId;
    }
}