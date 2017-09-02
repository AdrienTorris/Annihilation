using Engine.Config;

namespace Annihilation
{
    public enum DisplayMode : byte
    {
        Fullscreen,
        Windowed,
        BorderlessWindowed
    }

    public class GraphicsOptions
    {
        public DisplayMode DisplayMode = DisplayMode.Fullscreen;
        public int ResolutionX = 1920;
        public int ResolutionY = 1080;
        public bool VerticalSync = false;
        public int RefreshRate = 144;
        public float Gamma = 1.0f;

        /*public void GetConfigFields(out string category, out ConfigField[] fields)
        {
            category = nameof(GraphicsOptions);
            fields = new ConfigField[]
            {
                new ConfigField(nameof(DisplayMode), ConfigFile.TypeUint8, ((int)DisplayMode).ToString()),
                new ConfigField(nameof(ResolutionX), ConfigFile.TypeInt32, ResolutionX.ToString()),
                new ConfigField(nameof(ResolutionY), ConfigFile.TypeInt32, ResolutionY.ToString()),
                new ConfigField(nameof(VerticalSync), ConfigFile.TypeBool, VerticalSync.ToString()),
                new ConfigField(nameof(RefreshRate), ConfigFile.TypeInt32, RefreshRate.ToString()),
                new ConfigField(nameof(Gamma), ConfigFile.TypeFloat, Gamma.ToString())
            };
        }*/
    }
}