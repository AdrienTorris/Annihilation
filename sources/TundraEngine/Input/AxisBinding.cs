using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AxisBinding // Size: 5 bytes
    {
        public Axis Axis;
        public StringId32 Action;

        public AxisBinding(Axis axis, StringId32 action)
        {
            Axis = axis;
            Action = action;
        }
    }
}