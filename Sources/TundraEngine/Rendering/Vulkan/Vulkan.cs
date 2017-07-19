using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.Rendering
{
    public enum CompareOp
    {
        Never = 0,
        Less = 1,
        Equal = 2,
        LessOrEqual = 3,
        Greater = 4,
        NotEqual = 5,
        GreaterOrEqual = 6,
        Always = 7
    }

    [Flags]
    public enum SampleCountFlags : uint
    {
        Sample1 = 0x00000001,
        Sample2 = 0x00000002,
        Sample4 = 0x00000004,
        Sample8 = 0x00000008,
        Sample16 = 0x00000010,
        Sample32 = 0x00000020,
        Sample64 = 0x00000040
    }

    [SuppressUnmanagedCodeSecurity]
    public static class Vulkan
    {
        internal const string LibraryName = "vulkan-1.dll";

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

        public static unsafe void CreateInstance(ref InstanceCreateInfo createInfo, AllocationCallbacks* allocator, out Instance instance)
        {
            fixed (InstanceCreateInfo* createInfoPointer = &createInfo)
            fixed (Instance* instancePointer = &instance)
            {
                vkCreateInstance(createInfoPointer, allocator, instancePointer).CheckError();
            }
        }

        [DllImport(LibraryName)]
        internal static extern unsafe Result vkCreateInstance(InstanceCreateInfo* createInfo, AllocationCallbacks* allocator, Instance* instance);
    }
}