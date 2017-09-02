using Engine.IO;

namespace Annihilation
{
    public enum DisplayMode : byte
    {
        Fullscreen,
        Windowed,
        BorderlessWindowed
    }

    public class GraphicsOptions : IInitFileData
    {
        public DisplayMode DisplayMode = DisplayMode.Fullscreen;
        public int ResolutionX = 1920;
        public int ResolutionY = 1080;
        public bool VerticalSync = false;
        public int RefreshRate = 144;
        public float Gamma = 1.0f;

        public void GetInitFields(out string category, out InitField[] fields)
        {
            category = nameof(GraphicsOptions);
            fields = new InitField[]
            {
                new InitField(nameof(DisplayMode), InitFile.TypeUint8, ((int)DisplayMode).ToString()),
                new InitField(nameof(ResolutionX), InitFile.TypeInt32, ResolutionX.ToString()),
                new InitField(nameof(ResolutionY), InitFile.TypeInt32, ResolutionY.ToString()),
                new InitField(nameof(VerticalSync), InitFile.TypeBool, VerticalSync.ToString()),
                new InitField(nameof(RefreshRate), InitFile.TypeInt32, RefreshRate.ToString()),
                new InitField(nameof(Gamma), InitFile.TypeFloat, Gamma.ToString())
            };
        }
    }
}