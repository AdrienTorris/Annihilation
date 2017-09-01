using SDL2;

namespace Engine.Input
{
    public struct MouseButtonEvent
    {
        public readonly SDL.MouseButton Button;
        public readonly ButtonState State;
        public readonly bool IsDoubleClick;

        public MouseButtonEvent(SDL.MouseButton button, ButtonState state, bool isDoubleClick)
        {
            Button = button;
            State = state;
            IsDoubleClick = isDoubleClick;
        }

        public override string ToString()
        {
            return $"{nameof(Button)}: {Button}, {nameof(State)}: {State}, {nameof(IsDoubleClick)}: {IsDoubleClick}";
        }
    }
}