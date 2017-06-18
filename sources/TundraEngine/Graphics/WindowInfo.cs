using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public struct WindowInfo
    {
        public int PositionX;
        public int PositionY;
        public int Width;
        public int Height;
        public SDL_WindowFlags Flags;

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