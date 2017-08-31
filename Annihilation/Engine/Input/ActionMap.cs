using System.Runtime.InteropServices;

namespace Engine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ActionMap
    {
        public int Context;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;
    }
}