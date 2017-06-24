using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    public enum Axis
    {
        LeftX,
        LeftY,
        RightX,
        RightY,
        TriggerLeft,
        TriggerRight,

        Count
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisEvent
    {
        public Axis Axis;
        public float Value;
        public byte PlayerId;
    }
}