using System;
using System.Text;
using System.Security;
using System.Runtime.InteropServices;

namespace Vulkan
{
    [SuppressUnmanagedCodeSecurity]
    public static class Vulkan
    {
        //
        // Constants
        //
        public const string LibraryName = "vulkan-1.dll";
        public const float LodClampNone = 1000f;
        public const uint RemainingMipLevels = ~0U;
        public const uint RemainingArrayLayers = ~0U;
        public const ulong WholeSize = ~0UL;
        public const uint AttachmentUnused = ~0U;
        public const uint QueueFamilyIgnored = ~0U;
        public const uint SubpassExternal = ~0U;
        public const uint MaxPhysicalDeviceNameSize = 256;
        public const uint UuidSize = 16;
        public const uint MaxMemoryTypes = 32;
        public const uint MaxMemoryHeaps = 16;
        public const uint MaxExtensionNameSize = 256;
        public const uint MaxDescriptionSize = 256;
        public const int LuidSize = 8;
        public const string DebugReportExtensionName = "VK_EXT_debug_report";
        public const int MaxDeviceGroupSize = 32;
        public const string DeviceGroupExtensionName = "VK_KHX_device_group";
        public const string DeviceGeneratedCommandsExtensionName = "VK_NVX_device_generated_commands";

        // Global functions
        public static readonly EnumerateInstanceExtensionPropertiesDelegate EnumerateInstanceExtensionProperties = LoadGlobalFunction<EnumerateInstanceExtensionPropertiesDelegate>();
        public static readonly EnumerateInstanceLayerPropertiesDelegate EnumerateInstanceLayerProperties = LoadGlobalFunction<EnumerateInstanceLayerPropertiesDelegate>();
        public static readonly CreateInstanceDelegate CreateInstance = LoadGlobalFunction<CreateInstanceDelegate>();

        // Instance functions
        public static readonly DestroyInstanceDelegate DestroyInstance = LoadInstanceFunction<DestroyInstanceDelegate>(Windowing.Window);
        
        public static unsafe void Initialize()
        {
            // Instance functions
            DestroyInstance = LoadInstanceFunction<DestroyInstanceDelegate>();
        }

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        private static extern unsafe IntPtr vkGetInstanceProcAddr(Instance instance, byte* name);

        [DllImport(LibraryName, CallingConvention = CallingConvention.StdCall)]
        private static extern unsafe IntPtr vkGetInstanceProcAddr(IntPtr instance, byte* name);

        public unsafe static T LoadGlobalFunction<T>()
        {
            IntPtr function = IntPtr.Zero;
            string name = typeof(T).Name;
            name = ("vk" + name).Replace("Delegate", string.Empty);
            byte[] nameBytes = Encoding.UTF8.GetBytes(name);
            fixed (byte* namePtr = &nameBytes[0])
            {
                function = vkGetInstanceProcAddr(IntPtr.Zero, namePtr);
            }
            Assert.IsTrue(function != IntPtr.Zero, "Could not load function " + name);

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
                function = vkGetInstanceProcAddr(instance, namePtr);
            }
            Assert.IsTrue(function != IntPtr.Zero, "Could not load function " + name);

            return Marshal.GetDelegateForFunctionPointer<T>(function);
        }
    }
}