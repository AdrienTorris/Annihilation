using System;
using System.Security;
using System.Runtime.InteropServices;

namespace Engine.Vulkan
{
    public struct Device
    {
        internal IntPtr NativeHandle;

        [DllImport(Vulkan.LibraryName), SuppressUnmanagedCodeSecurity]
        internal static extern unsafe Result vkCreateGraphicsPipelines(
            Device device,
            PipelineCache pipelineCache,
            uint createInfoCount,
            GraphicsPipelineCreateInfo* createInfos,
            AllocationCallbacks* allocator,
            Pipeline* pipelines);
    }
}
