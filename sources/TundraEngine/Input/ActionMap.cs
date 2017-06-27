using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ActionMap
    {
        public StringId32 Context;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;
    }
}