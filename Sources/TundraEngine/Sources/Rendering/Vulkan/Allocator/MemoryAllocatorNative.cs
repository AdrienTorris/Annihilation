using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.Rendering.Vulkan.Allocator
{
    [SuppressUnmanagedCodeSecurity]
    internal class MemoryAllocatorNative
    {
        public const string LibName = "VulkanMemoryAllocator.dll";

        [DllImport(LibName, EntryPoint = "vmaCreateAllocator")]
        public static extern Result CreateAllocator(AllocatorCreateInfo createInfo, out UIntPtr allocator);

        [DllImport(LibName, EntryPoint = "vmaDestroyAllocator")]
        public static extern void DestroyAllocator(UIntPtr allocator);

        [DllImport(LibName, EntryPoint = "vmaGetPhysicalDeviceProperties")]
        public static extern void GetPhysicalDeviceProperties(UIntPtr allocator, out PhysicalDeviceProperties[] physicalDeviceProperties);

        [DllImport(LibName, EntryPoint = "vmaGetMemoryProperties")]
        public static extern void GetMemoryProperties(UIntPtr allocator, out PhysicalDeviceMemoryProperties[] physicalDeviceMemoryProperties);

        [DllImport(LibName, EntryPoint = "vmaGetMemoryTypeProperties")]
        public static extern void GetMemoryTypeProperties(UIntPtr allocator, uint memoryTypeIndex, out MemoryPropertyFlags flags);

        [DllImport(LibName, EntryPoint = "vmaCalculateStats")]
        public static extern void CalculateStats(UIntPtr allocator, out AllocatorStats stats);

        [DllImport(LibName, EntryPoint = "vmaBuildStatsString")]
        public static extern void BuildStatsString(UIntPtr allocator, out string[] statsString, bool detailedMap);

        [DllImport(LibName, EntryPoint = "vmaFreeStatsString")]
        public static extern void FreeStatsString(UIntPtr allocator, string[] statsString);

        [DllImport(LibName, EntryPoint = "vmaFindMemoryTypeIndex")]
        public static extern Result FindMemoryTypeIndex(UIntPtr allocator, uint memoryTypeBits, MemoryRequirements memoryRequirements, out uint memoryTypeIndex);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemory")]
        public static extern Result AllocateMemory(UIntPtr allocator, MemoryRequirements vulkanMemoryRequirements, MemoryRequirements memoryRequirements, out UIntPtr allocation, out AllocationInfo allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemory")]
        public static extern Result AllocateMemory(UIntPtr allocator, MemoryRequirements vulkanMemoryRequirements, MemoryRequirements memoryRequirements, out UIntPtr allocation, out UIntPtr allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemoryForBuffer")]
        public static extern Result AllocateMemoryForBuffer(UIntPtr allocator, Buffer buffer, MemoryRequirements memoryRequirements, out UIntPtr allocation, out AllocationInfo allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemoryForBuffer")]
        public static extern Result AllocateMemoryForBuffer(UIntPtr allocator, Buffer buffer, MemoryRequirements memoryRequirements, out UIntPtr allocation, out UIntPtr allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemoryForImage")]
        public static extern Result AllocateMemoryForImage(UIntPtr allocator, Buffer buffer, MemoryRequirements memoryRequirements, out UIntPtr allocation, out AllocationInfo allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaAllocateMemoryForImage")]
        public static extern Result AllocateMemoryForImage(UIntPtr allocator, Buffer buffer, MemoryRequirements memoryRequirements, out UIntPtr allocation, out UIntPtr allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaFreeMemory")]
        public static extern void FreeMemory(UIntPtr allocator, UIntPtr allocation);

        [DllImport(LibName, EntryPoint = "vmaGetAllocationInfo")]
        public static extern void GetAllocationInfo(UIntPtr allocator, UIntPtr allocation, out AllocationInfo allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaSetAllocationUserData")]
        public static extern void SetAllocationUserData(UIntPtr allocator, UIntPtr allocation, UIntPtr userData);

        [DllImport(LibName, EntryPoint = "vmaMapMemory")]
        public static extern Result MapMemory(UIntPtr allocator, UIntPtr allocation, UIntPtr data);

        [DllImport(LibName, EntryPoint = "vmaUnmapMemory")]
        public static extern void UnmapMemory(UIntPtr allocator, UIntPtr allocation);

        [DllImport(LibName, EntryPoint = "vmaUnmapPersistentlyMappedMemory")]
        public static extern void UnmapPersistentlyMappedMemory(UIntPtr allocator);

        [DllImport(LibName, EntryPoint = "vmaMapPersistentlyMappedMemory")]
        public static extern Result MapPersistentlyMappedMemory(UIntPtr allocator);

        [DllImport(LibName, EntryPoint = "vmaDefragment")]
        public static extern Result Defragment(
            UIntPtr allocator,
            UIntPtr allocations,
            ulong allocationCount,
            out UIntPtr allocationsChanged,
            UIntPtr defragmentationInfo,
            out UIntPtr defragmentationStats);

        [DllImport(LibName, EntryPoint = "vmaCreateBuffer")]
        public static extern Result CreateBuffer(
            UIntPtr allocator,
            BufferCreateInfo createInfo,
            MemoryRequirements memoryRequirements,
            out Buffer buffer,
            out UIntPtr allocation,
            out UIntPtr allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaDestroyBuffer")]
        public static extern void DestroyBuffer(UIntPtr allocator, Buffer buffer, UIntPtr allocation);

        [DllImport(LibName, EntryPoint = "vmaCreateImage")]
        public static extern Result CreateImage(
            UIntPtr allocator,
            ImageCreateInfo createInfo,
            MemoryRequirements memoryRequirements,
            out Image image,
            UIntPtr allocation,
            UIntPtr allocationInfo);

        [DllImport(LibName, EntryPoint = "vmaDestroyImage")]
        public static extern void DestroyImage(UIntPtr allocator, Image image, UIntPtr allocation);
    }
}