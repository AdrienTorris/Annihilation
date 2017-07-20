using System;

namespace TundraEngine.Rendering.Vulkan.Allocator
{
    /// <summary>
    /// Callback function called after successful <see cref="AllocateMemory"/>
    /// </summary>
    public delegate void AllocateDeviceMemoryFunction(
        IntPtr allocator,
        uint memoryType,
        DeviceMemory memory,
        DeviceSize size);

    /// <summary>
    /// Callback function called after successful <see cref="FreeMemory"/>
    /// </summary>
    public delegate void FreeDeviceMemoryFunction(
        IntPtr allocator,
        uint memoryType,
        DeviceMemory memory,
        DeviceSize size);

    /// <summary>
    /// Set of callbacks that the library will call for <see cref="AllocateMemory"/> and <see cref="FreeMemory"/>.
    /// <para/> Provided for informative purpose, e.g. to gather statistics about number of allocations or total amount of memory allocated in Vulkan.
    /// </summary>
    public struct DeviceMemoryCallbacks
    {
        public AllocateDeviceMemoryFunction Allocate;
        public FreeDeviceMemoryFunction Free;
    }
}