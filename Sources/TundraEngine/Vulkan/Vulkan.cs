using System;
using System.IO;
using System.Security;
using System.Runtime.InteropServices;

using Vulkan.Handle;

namespace Vulkan
{
    [SuppressUnmanagedCodeSecurity]
    public static class Vulkan
    {
        private static IntPtr _library;

        static Vulkan()
        {
            _library = LoadLibrary();
        }

        //
        // Kernel32
        //
        [DllImport("kernel32")]
        private static extern IntPtr LoadLibrary(string fileName);

        [DllImport("kernel32")]
        private static extern IntPtr GetProcAddress(IntPtr module, string procName);

        [DllImport("kernel32")]
        private static extern int FreeLibrary(IntPtr module);

        //
        // Libdl
        //
        [DllImport("libdl.so")]
        private static extern IntPtr dlopen(string fileName, int flags);

        [DllImport("libdl.so")]
        private static extern IntPtr dlsym(IntPtr handle, string name);

        [DllImport("libdl.so")]
        private static extern int dlclose(IntPtr handle);

        [DllImport("libdl.so")]
        private static extern string dlerror();

        private const int RTLD_NOW = 0x002;

        //
        // Global functions
        //
        public static readonly GetInstanceProcAddr GetInstanceProcAddr = LoadLoadingFunction();
        public static readonly EnumerateInstanceExtensionProperties EnumerateInstanceExtensionProperties = LoadGlobalFunction<EnumerateInstanceExtensionProperties>();
        public static readonly EnumerateInstanceLayerProperties EnumerateInstanceLayerProperties = LoadGlobalFunction<EnumerateInstanceLayerProperties>();
        public static readonly CreateInstance CreateInstance = LoadGlobalFunction<CreateInstance>();

        //
        // Instance functions
        //
        public static readonly DestroyInstance DestroyInstance = LoadInstanceFunction<DestroyInstance>();

        private static IntPtr LoadLibrary()
        {
            IntPtr handle;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                handle = LoadLibrary("vulkan-1.dll");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                dlerror();
                handle = dlopen("libvulkan.so.1", RTLD_NOW);
                if (handle == IntPtr.Zero && !Path.IsPathRooted("libvulkan.so.1"))
                {
                    string localPath = Path.Combine(AppContext.BaseDirectory, "libvulkan.so.1");
                    handle = dlopen(localPath, RTLD_NOW);
                }
            }
            else
            {
                throw new PlatformNotSupportedException();
            }

            if (handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not load Vulkan library");
            }
            return handle;
        }

        private unsafe static GetInstanceProcAddr LoadLoadingFunction()
        {
            IntPtr handle = IntPtr.Zero;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                handle = GetProcAddress(_library, "PFN_vkGetInstanceProcAddr");
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                handle = dlsym(_library, "PFN_vkGetInstanceProcAddr");
            }
            if (handle == IntPtr.Zero)
            {
                throw new InvalidOperationException("Could not load Vulkan loading function");
            }
            return Marshal.GetDelegateForFunctionPointer<GetInstanceProcAddr>(handle);
        }

        public unsafe static T LoadGlobalFunction<T>()
        {
            string name = "vk" + typeof(T).Name;
            IntPtr function = GetInstanceProcAddr(Instance.Null, name);
            if (function == IntPtr.Zero)
            {
                throw new NullReferenceException();
            }
            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

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
    }
}