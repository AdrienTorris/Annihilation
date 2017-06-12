using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        public const int MajorVersion = 2;
        public const int MinorVersion = 0;
        public const int PatchLevel = 5;

        /// <summary>
        /// Information the version of SDL in use.
        /// <para/> Represents the library's version as three levels: major revision (increments with massive changes, additions, and enhancements), minor revision (increments with backwards-compatible changes to the major revision), and patchlevel (increments with fixes to the minor revision).
        /// </summary>
        [StructLayout (LayoutKind.Sequential)]
        public struct Version
        {
            public readonly byte Major;
            public readonly byte Minor;
            public readonly byte Patch;

            public Version (byte major, byte minor, byte patch)
            {
                Major = major;
                Minor = minor;
                Patch = patch;
            }
        }

        public static void FillVersion (out Version version)
        {
            version = new Version (MajorVersion, MinorVersion, PatchLevel);
        }

        /// <summary>
        /// Get the version of SDL that is linked against your program.
        /// <para/> This function may be called safely at any time, even before SDL_Init().
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetVersion", CallingConvention = CallingConvention.Cdecl)]
        public extern static void GetVersion (out Version version);

        [DllImport (LibName, EntryPoint = "SDL_GetRevision", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetRevisionInternal ();

        /// <summary>
        /// Get the code revision of SDL that is linked against your program.
        /// <para /> Returns an arbitrary string (a hash value) uniquely identifying the exact revision of the SDL library in use, and is only useful in comparing against other revisions.It is NOT an incrementing number.
        /// </summary>
        /// <returns></returns>
        public static string GetRevision ()
        {
            return GetRevisionInternal ().ToStr ();
        }

        /// <summary>
        /// Get the revision number of SDL that is linked against your program.
        /// <para /> Returns a number uniquely identifying the exact revision of the SDL library in use.It is an incrementing number based on commits to hg.libsdl.org
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetRevisionNumber", CallingConvention = CallingConvention.Cdecl)]
        public extern static int GetRevisionNumber ();
    }
}