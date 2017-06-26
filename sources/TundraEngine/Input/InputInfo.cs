using TundraEngine.Input;

namespace TundraEngine
{
    public struct InputInfo
    {
        public float RepeatDelay;
        public float RepeatInterval;
        public float GamepadDeadZone;
        public StringId32 Gamepads;
        public ActionMap ActionMap;

        public const float DefaultRepeatDelay = 0.5f;
        public const float DefaultRepeatInterval = 0.2f;
    }
}