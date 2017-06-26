using static TundraEngine.SDL.SDL;

namespace TundraEngine
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
        public int PositionX;
        public int PositionY;
        public int Width;
        public int Height;
        public WindowMode Mode;
        public bool AllowHighDPI;
        public bool AlwaysOnTop;

        public const int DefaultPosition = SDL_WindowPositionUndefined;
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;

        public static WindowInfo Default => new WindowInfo
        {
            PositionX = DefaultPosition,
            PositionY = DefaultPosition,
            Width = DefaultWidth,
            Height = DefaultHeight
        };
    }
}