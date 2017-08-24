using System;
using System.Runtime.InteropServices;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct DeviceMemoryCallbacks
        {
            public AllocateDeviceMemoryFunction Allocate;
            public FreeDeviceMemoryFunction Free;
        }

        /// <summary>
        /// Description of a <see cref="Allocator"/> to be created.
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public unsafe struct AllocatorCreateInfo
        {
            /// <summary>
            /// Flags for created allocator.
            /// </summary>
            public AllocatorFlags Flags;
            /// <summary>
            /// Vulkan physical device.
            /// <para/> It must be valid throughout whole lifetime of created Allocator.
            /// </summary>
            public Vk.PhysicalDevice PhysicalDevice;
            /// <summary>
            /// Vulkan device.
            /// <para/> It must be valid throughout whole lifetime of created Allocator.
            /// </summary>
            public Vk.Device Device;
            /// <summary>
            /// Size of a single memory block to allocate for resources.
            /// <para/> Set to 0 to use default, which is currently 256 MB.
            /// </summary>
            public Vk.DeviceSize PreferredLargeHeapBlockSize;
            /// <summary>
            /// Size of a single memory block to allocate for resources from a small heap &lt 512 MB.
            /// <para/> Set to 0 to use default, which is currently 64 MB.
            /// </summary>
            public Vk.DeviceSize PreferredSmallHeapBlockSize;
            /// <summary>
            /// Custom allocation callbacks.
            /// <para/> Optional, can be null. When specified, will also be used for all CPU-side memory allocations.
            /// </summary>
            public IntPtr* AllocationCallbacks;
            /// <summary>
            /// Informative callbacks for <see cref="AllocateMemory"/>, <see cref="FreeMemory"/>
            /// <para/> Optional, can be null.
            /// </summary>
            public IntPtr* DeviceMemoryCallbacks;
        }
    }
}