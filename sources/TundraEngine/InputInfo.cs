namespace TundraEngine
{
    public struct InputInfo
    {
        public float RepeatDelay;
        public float RepeatInterval;
        public StringId64 Gamepads;
        public StringId64 Bindings;

        public const float DefaultRepeatDelay = 0.5f;
        public const float DefaultRepeatInterval = 0.2f;

        public static InputInfo Default => new InputInfo
        {
            RepeatDelay = DefaultRepeatDelay,
            RepeatInterval = DefaultRepeatInterval
        };
    }
}