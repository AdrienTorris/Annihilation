using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisBinding // Size: 5 bytes
    {
        public Axis Axis;
        public StringHash32 Action;

        public AxisBinding(Axis axis, StringHash32 action)
        {
            Axis = axis;
            Action = action;
        }
    }
}