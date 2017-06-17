using static SDL.SDL;

namespace TundraEngine
{
    public struct WindowInfo
    {
        public string Name;
        public int PositionX;
        public int PositionY;
        public int Width;
        public int Height;

        public const int DefaultPosition = SDL_WindowPositionUndefined;
        public const int DefaultWidth = 1280;
        public const int DefaultHeight = 720;

        public static WindowInfo Default => new WindowInfo
        {
            Name = "Tundra Game",
            PositionX = DefaultPosition,
            PositionY = DefaultPosition,
            Width = DefaultWidth,
            Height = DefaultHeight
        };

        public static WindowInfo DefaultWithName (string name) => new WindowInfo
        {
            Name = name,
            PositionX = DefaultPosition,
            PositionY = DefaultPosition,
            Width = DefaultWidth,
            Height = DefaultHeight
        };
    }
}