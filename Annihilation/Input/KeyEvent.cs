using SDL2;

namespace Engine.Input
{
    public struct KeyEvent
    {
        public readonly SDL.KeyCode Key;
        public readonly ButtonState State;
        public readonly int RepeatCount;

        public KeyEvent(SDL.KeyCode key, ButtonState state, int repeatCount)
        {
            Key = key;
            State = state;
            RepeatCount = repeatCount;
        }

        public override string ToString()
        {
            return $"{nameof(Key)}: {Key}, {nameof(State)}: {State}, {nameof(RepeatCount)}: {RepeatCount}";
        }
    }
}