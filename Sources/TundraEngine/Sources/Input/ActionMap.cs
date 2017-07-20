using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ActionMap
    {
        public StringHash32 Context;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;
    }
}