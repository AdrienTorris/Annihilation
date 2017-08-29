using System;
using System.Runtime.InteropServices;

namespace Engine.System
{
    public static class Libdl
    {
        private const string LibName = "libdl";

        public const int RTLD_NOW = 0x002;

        [DllImport(LibName)]
#pragma warning disable IDE1006 // Naming Styles
        public static extern IntPtr dlopen(string fileName, int flags);
#pragma warning restore IDE1006 // Naming Styles

        [DllImport(LibName)]
#pragma warning disable IDE1006 // Naming Styles
        public static extern IntPtr dlsym(IntPtr handle, string name);
#pragma warning restore IDE1006 // Naming Styles

        [DllImport(LibName)]
#pragma warning disable IDE1006 // Naming Styles
        public static extern int dlclose(IntPtr handle);
#pragma warning restore IDE1006 // Naming Styles

        [DllImport(LibName)]
#pragma warning disable IDE1006 // Naming Styles
        public static extern string dlerror();
#pragma warning restore IDE1006 // Naming Styles
    }
}