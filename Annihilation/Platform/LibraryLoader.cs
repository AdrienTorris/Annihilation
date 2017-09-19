using System;
using System.IO;
using System.Runtime.InteropServices;

namespace Engine.Platform
{
    /// <summary>
    /// Exposes functionality for loading native libraries and function pointers.
    /// </summary>
    public abstract class LibraryLoader
    {
        /// <summary>
        /// Loads a native library by name and returns an operating system handle to it.
        /// </summary>
        /// <param name="name">The name of the library to open.</param>
        /// <returns>The operating system handle for the shared library.</returns>
        public IntPtr LoadNativeLibrary(string name)
        {
            IntPtr library = CoreLoadNativeLibrary(name);

            if (library == IntPtr.Zero)
            {
                throw new FileNotFoundException("Could not find or load the native library: " + name);
            }

            return library;
        }
        
        /// <summary>
        /// Loads a native library by name and returns an operating system handle to it.
        /// </summary>
        /// <param name="name">The name of the library to open. This parameter must not be null or empty.</param>
        /// <returns>The operating system handle for the shared library.
        /// If the library cannot be loaded, IntPtr.Zero should be returned.</returns>
        protected abstract IntPtr CoreLoadNativeLibrary(string name);

        /// <summary>
        /// Frees the library represented by the given operating system handle.
        /// </summary>
        /// <param name="handle">The handle of the open shared library. This must not be zero.</param>
        public abstract void FreeNativeLibrary(IntPtr handle);

        /// <summary>
        /// Loads a function pointer out of the given library by name.
        /// </summary>
        /// <param name="handle">The operating system handle of the opened shared library. This must not be zero.</param>
        /// <param name="functionName">The name of the exported function to load. This must not be null or empty.</param>
        /// <returns>A pointer to the loaded function.</returns>
        public abstract IntPtr LoadFunctionPointer(IntPtr handle, string functionName);

        /// <summary>
        /// Returns a default library loader for the running operating system.
        /// </summary>
        /// <returns>A LibraryLoader suitable for loading libraries.</returns>
        public static LibraryLoader GetPlatformDefaultLoader()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return new Win32LibraryLoader();
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux) || RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return new UnixLibraryLoader();
            }

            throw new PlatformNotSupportedException("This platform cannot load native libraries.");
        }

        private class Win32LibraryLoader : LibraryLoader
        {
            public override void FreeNativeLibrary(IntPtr handle)
            {
                Kernel32.FreeLibrary(handle);
            }

            public override IntPtr LoadFunctionPointer(IntPtr handle, string functionName)
            {
                return Kernel32.GetProcAddress(handle, functionName);
            }

            protected override IntPtr CoreLoadNativeLibrary(string name)
            {
                return Kernel32.LoadLibrary(name);
            }
        }

        private class UnixLibraryLoader : LibraryLoader
        {
            public override void FreeNativeLibrary(IntPtr handle)
            {
                Libdl.dlclose(handle);
            }

            public override IntPtr LoadFunctionPointer(IntPtr handle, string functionName)
            {
                return Libdl.dlsym(handle, functionName);
            }

            protected override IntPtr CoreLoadNativeLibrary(string name)
            {
                return Libdl.dlopen(name, Libdl.RTLD_NOW);
            }
        }
    }
}