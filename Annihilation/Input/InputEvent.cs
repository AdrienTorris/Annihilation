using System.Runtime.InteropServices;

namespace Engine.Input
{
    public enum ButtonState : byte
    {
        Released = 0,
        Pressed = 1
    }

    public enum InputEventType : byte
    {
        None,
        Key,
        MouseButton,
        MouseMove,
        MouseWheel,
        Text
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct InputEvent
    {
        [FieldOffset(0)] public InputEventType Type;
        [FieldOffset(1)] public KeyEvent KeyEvent;
        [FieldOffset(1)] public MouseButtonEvent MouseButtonEvent;
        [FieldOffset(1)] public MouseMoveEvent MouseMoveEvent;
        [FieldOffset(1)] public MouseWheelEvent MouseWheelEvent;
        [FieldOffset(1)] public TextEvent TextEvent;
    }
}