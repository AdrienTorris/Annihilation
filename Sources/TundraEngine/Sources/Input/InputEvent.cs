using System.Runtime.InteropServices;

namespace TundraEngine.Input
{
    public enum InputEventType : byte
    {
        None,
        Button,
        Axis,
        MouseMove,
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputEvent
    {
        [FieldOffset(0)] public InputEventType Type;
        [FieldOffset(1)] public ButtonEvent ButtonEvent;
        [FieldOffset(1)] public AxisEvent AxisEvent;
        [FieldOffset(1)] public MouseMoveEvent MouseMoveEvent;
    }
}