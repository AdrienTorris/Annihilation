namespace Engine.Config
{
    public struct Settings
    {
        public StringVar InitialWorld;

        public EnumVar<WindowMode> WindowMode;
        public FloatVar TargetFramerate;
        public IntVar ScreenWidth;
        public IntVar ScreenHeight;

        public static Settings Default = new Settings
        {
            InitialWorld = new StringVar("InitialWorld", "DefaultWorld"),
            WindowMode = new EnumVar<WindowMode>("WindowMode", Config.WindowMode.Windowed),
            TargetFramerate = new FloatVar("TargetFramerate", 144f),
            ScreenWidth = new IntVar("ScreenWidth", 1280),
            ScreenHeight = new IntVar("ScreenHeight", 720)
        };
    }
}