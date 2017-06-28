using TundraEngine.Mathematics;

namespace TundraEngine.IMGUI
{
    public static class DebugUI
    {
        private static IDebugUI _debugUI;
        
        public static void Clear()
        {
            _debugUI.Clear();
        }

        public static void Text(int x, int y, string text)
        {
            _debugUI.Text(x, y, text);
        }

        public static void Text(int x, int y, Color foreColor, string text)
        {
            _debugUI.Text(x, y, foreColor, text);
        }

        public static void Text(int x, int y, Color foreColor, Color backColor, string text)
        {
            _debugUI.Text(x, y, foreColor, backColor, text);
        }

        public static void Image(int x, int y, int width, int height, byte[] pixels, int stride)
        {
            _debugUI.Image(x, y, width, height, pixels, stride);
        }
    }
}