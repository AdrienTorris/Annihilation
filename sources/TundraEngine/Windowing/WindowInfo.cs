namespace TundraEngine.Windowing
{
    public enum WindowMode
    {
        Windowed,
        BorderlessWindow,
        Fullscreen,
        FullscreenDesktop
    }

    public struct WindowInfo
    {
        public string Name;
        public int PositionX;
        public int PositionY;
        public int Width;
        public int Height;
        public WindowMode Mode;
        public bool AllowHighDPI;
        public bool AlwaysOnTop;
        
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;
    }
}