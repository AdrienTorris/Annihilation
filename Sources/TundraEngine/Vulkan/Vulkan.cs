using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

using Vulkan.Handle;

namespace Vulkan
{
    [SuppressUnmanagedCodeSecurity]
    public static class Vulkan
    {
        public const string LibraryName = "vulkan-1.dll";

        // Global functions
        public static readonly EnumerateInstanceExtensionPropertiesDelegate EnumerateInstanceExtensionProperties = LoadGlobalFunction<EnumerateInstanceExtensionPropertiesDelegate>();
        public static readonly EnumerateInstanceLayerPropertiesDelegate EnumerateInstanceLayerProperties = LoadGlobalFunction<EnumerateInstanceLayerPropertiesDelegate>();
        public static readonly CreateInstanceDelegate CreateInstance = LoadGlobalFunction<CreateInstanceDelegate>();

        // Instance functions
        public static readonly DestroyInstanceDelegate DestroyInstance = LoadInstanceFunction<DestroyInstanceDelegate>(Windowing.Window);
        
        public unsafe static T LoadGlobalFunction<T>()
        {
            IntPtr function = IntPtr.Zero;
            string name = typeof(T).Name;
            name = ("vk" + name).Replace("Delegate", string.Empty);
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* namePtr = &nameBytes[0])
            {
                function = GetInstanceProcAddr(IntPtr.Zero, namePtr);
                if (function == IntPtr.Zero)
                    throw new NullReferenceException();
            }

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }

        public unsafe static T LoadInstanceFunction<T>(Instance instance)
        {
            IntPtr function = IntPtr.Zero;
            string name = typeof(T).Name;
            name = ("vk" + name).Replace("Delegate", string.Empty);
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* namePtr = &nameBytes[0])
            {
                function = GetInstanceProcAddr(instance, namePtr);
                if (function == IntPtr.Zero)
                    throw new NullReferenceException();
            }

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }
    }
}