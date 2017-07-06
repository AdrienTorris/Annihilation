using System.Numerics;
using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisEvent
    {
        public Axis Axis;
        public Vector2 Value;
        public byte PlayerId;
    }
}