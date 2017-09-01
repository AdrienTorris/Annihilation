using Engine.Input;

namespace Engine
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
    
    public class Settings
    {
        public string Title;
        public string Version;
        public string[] CommandLineArgs;
        
        public StringHash32 InitialContext;
        
        public string ResourcePath;
        public int MaxResources;
        
        public int MaxEntitiesPerPrefab;
        
        public WindowSettings WindowSettings;
        public RendererSettings RendererSettings;
        public InputSettings InputSettings;

        public const string DefaultResourcePath = "/Resources/";
        public const int DefaultMaxResources = 1024;
        public const int DefaultMaxEntitiesPerPrefab = 1024;
    }
}