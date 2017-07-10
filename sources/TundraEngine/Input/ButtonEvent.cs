using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ButtonEvent
    {
        public Button Button;
        public ButtonState State;
        public byte PlayerId;
    }
}