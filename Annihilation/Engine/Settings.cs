using System.Diagnostics;

namespace Engine
{
    // TODO: Need to handle beign able to set some of these as options
    public struct InputSettings
    {
        public float RepeatDelay;
        public float RepeatInterval;
        public float GamepadDeadZone;

        public const float DefaultRepeatDelay = 0.5f;
        public const float DefaultRepeatInterval = 0.2f;
    }
    
    public struct GraphicsSettings
    {
        public int DeviceIndex;
    }

    public struct ApplicationSettings
    {

    }
}