using System;

using TundraEngine.Mathematics;

namespace TundraEngine.IMGUI
{
    public interface IDebugUI : IDisposable
    {
        void Clear();
        void Text(int x, int y, string text);
        void Text(int x, int y, Color foreColor, string text);
        void Text(int x, int y, Color foreColor, Color backColor, string text);
        void Image(int x, int y, int width, int height, byte[] pixels, int stride);
    }
}