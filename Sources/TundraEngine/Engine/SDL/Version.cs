using System.Runtime.InteropServices;

namespace Engine.SDL
{
    /// <summary>
    /// Information the version of SDL in use.
    /// <para/> Represents the library's version as three levels: major revision (increments with massive changes, additions, and enhancements), minor revision (increments with backwards-compatible changes to the major revision), and patchlevel (increments with fixes to the minor revision).
    /// </summary>
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