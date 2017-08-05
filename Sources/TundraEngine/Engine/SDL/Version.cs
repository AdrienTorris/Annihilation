using System.Runtime.InteropServices;

namespace Engine.SDL
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Version
    {
        public readonly byte Major;
        public readonly byte Minor;
        public readonly byte Patch;

        public const int MajorVersion = 2;
        public const int MinorVersion = 0;
        public const int PatchLevel = 5;

        public static Version Current => new Version(MajorVersion, MinorVersion, PatchLevel);

        public Version(byte major, byte minor, byte patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }
    }
}