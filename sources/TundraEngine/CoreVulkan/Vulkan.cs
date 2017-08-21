using System;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;

using CoreVulkan.Handle;

namespace CoreVulkan
{
    public static class Vulkan
    {
        public readonly static GetInstanceProcAddr GetInstanceProcAddr;

        private static IntPtr _library;
        
        static Vulkan()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                _library = Win32.LoadLibrary("vulkan-1.dll");
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Win32.GetProcAddress(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                Linux.dlerror();
                _library = Linux.dlopen("libvulkan.so.1", Linux.RTLD_NOW);
                if (_library == IntPtr.Zero && !Path.IsPathRooted("libvulkan.so.1"))
                {
                    string localPath = Path.Combine(AppContext.BaseDirectory, "libvulkan.so.1");
                    _library = Linux.dlopen(localPath, Linux.RTLD_NOW);
                }
                if (_library == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan library");
                }
                IntPtr getProcAddr = Linux.dlsym(_library, "PFN_vkGetInstanceProcAddr");
                if (getProcAddr == IntPtr.Zero)
                {
                    throw new InvalidOperationException("Could not load Vulkan loading function");
                }
                GetInstanceProcAddr = Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(getProcAddr);
            }
            else
            {
                throw new PlatformNotSupportedException();
            }
        }

        public unsafe static T LoadGlobalFunction<T>() => LoadInstanceFunction<T>(Instance.Null);

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            string name = "vk" + typeof(T).Name;
            IntPtr function = GetInstanceProcAddr(instance, name);
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        [SuppressUnmanagedCodeSecurity]
        private static class Win32
        {
            [DllImport("kernel32")]
            public static extern IntPtr LoadLibrary(string fileName);

            [DllImport("kernel32")]
            public static extern IntPtr GetProcAddress(IntPtr module, string procName);

            [DllImport("kernel32")]
            public static extern int FreeLibrary(IntPtr module);
        }

        [SuppressUnmanagedCodeSecurity]
        private static class Linux
        {
            public const int RTLD_NOW = 0x002;

            [DllImport("libdl.so")]
            public static extern IntPtr dlopen(string fileName, int flags);

            [DllImport("libdl.so")]
            public static extern IntPtr dlsym(IntPtr handle, string name);

            [DllImport("libdl.so")]
            public static extern int dlclose(IntPtr handle);

            [DllImport("libdl.so")]
            public static extern string dlerror();
        }
    }
}