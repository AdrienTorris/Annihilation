namespace TundraEngine.Input
{
    public struct InputSettings
    {
        public float RepeatDelay;
        public float RepeatInterval;
        public float GamepadDeadZone;
        public ActionMap[] ActionMaps;

        public const float DefaultRepeatDelay = 0.5f;
        public const float DefaultRepeatInterval = 0.2f;
    }
}