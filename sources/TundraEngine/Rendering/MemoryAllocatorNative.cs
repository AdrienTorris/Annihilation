using System;
using System.Runtime.InteropServices;

using SharpVk;

namespace TundraEngine.Rendering
{
    internal class MemoryAllocatorNative
    {
        public const string LibName = "VulkanMemoryAllocator.dll";

        [DllImport(LibName, EntryPoint = "vmaCreateAllocator")]
        internal static extern void CreateAllocator(AllocatorCreateInfo createInfo, out IntPtr allocator);
    }
}