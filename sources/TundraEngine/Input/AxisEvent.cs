using System.Runtime.InteropServices;

using TundraEngine.Mathematics;

namespace TundraEngine.Input
{
    public enum Axis : byte
    {
        // Mouse
        MouseMove,
        MouseWheel,

        // Gamepad
        LeftThumb,
        RightThumb,
        LeftTrigger,
        RightTrigger,
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisEvent
    {
        public Axis Axis;
        public Vector2 Value;
        public byte PlayerId;
    }
}