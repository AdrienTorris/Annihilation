using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential)]
    public struct ActionMap
    {
        public StringId32 Name;
        public ButtonBinding[] ButtonBindings;
        public AxisBinding[] AxisBindings;
    }
}