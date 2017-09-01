using SDL2;

namespace Engine.Input
{
    public struct KeyEvent
    {
        public SDL.KeyCode Key;
        public ButtonState State;
        public int RepeatCount;

        public override string ToString()
        {
            return $"{nameof(Key)}: {Key}, {nameof(State)}: {State}, {nameof(RepeatCount)}: {RepeatCount}";
        }
    }
}