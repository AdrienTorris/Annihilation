using System.Diagnostics;

namespace Engine
{
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
    }

    public struct ApplicationSettings
    {
        public Text Title;
        public Text Organization;
        public Text Version;

        [Conditional("DEBUG")]
        public void CheckError()
        {
            if (Title == null) Log.Error($"Settings must have non-null {nameof(Title)}");
            if (Organization == null) Log.Error($"Settings must have non-null {nameof(Organization)}");
            if (Version == null) Log.Error($"Settings must have non-null {nameof(Version)}");
        }
    }
}