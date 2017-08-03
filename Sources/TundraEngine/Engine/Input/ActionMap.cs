using System.Runtime.InteropServices;

namespace Engine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ActionMap
    {
        public StringHash32 Context;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;
    }
}