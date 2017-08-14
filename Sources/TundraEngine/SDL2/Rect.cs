using System.Runtime.InteropServices;

namespace SDL2
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