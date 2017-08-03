namespace Engine.Rendering
{
    public enum WindowMode : byte
    {
        Windowed,
        BorderlessWindow,
        Fullscreen,
        FullscreenDesktop
    }

    public struct WindowSettings
    {
        public string Name;
        public int Monitor;
        public int PositionX;
        public int PositionY;
        public int Width;
        public int Height;
        public WindowMode Mode;
        public bool AllowHighDPI;
        public bool AlwaysOnTop;

        public const int DefaultPosition = -1;
        public const int DefaultSize = -1;
    }
}