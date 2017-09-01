using SDL2;

namespace Engine.Input
{
    public struct MouseButtonEvent
    {
        public SDL.MouseButton Button;
        public ButtonState State;
        public bool IsDoubleClick;

        public override string ToString()
        {
            return $"{nameof(Button)}: {Button}, {nameof(State)}: {State}, {nameof(IsDoubleClick)}: {IsDoubleClick}";
        }
    }
}