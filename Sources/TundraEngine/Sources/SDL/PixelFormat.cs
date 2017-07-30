using System;

namespace TundraEngine.SDL
{
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    public unsafe struct Palette
    {
        public int NumColors;
        public Color* Colors;
        public uint Version;
        public int RefCount;
    }

    public unsafe struct PixelFormat
    {
        public readonly uint Format;
        public readonly Palette* Palette;
        public readonly byte BitsPerPixel;
        public readonly byte BytesPerPixel;
        public fixed byte Padding[2];
        public readonly uint RMask;
        public readonly uint GMask;
        public readonly uint BMask;
        public readonly uint AMask;
        public readonly byte RLoss;
        public readonly byte GLoss;
        public readonly byte BLoss;
        public readonly byte ALoss;
        public readonly byte RShift;
        public readonly byte GShift;
        public readonly byte BShift;
        public readonly byte AShift;
        public readonly int RefCount;
        public readonly PixelFormat* Next;
    }
}