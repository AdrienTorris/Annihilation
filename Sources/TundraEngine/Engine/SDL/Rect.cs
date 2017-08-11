using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }
}