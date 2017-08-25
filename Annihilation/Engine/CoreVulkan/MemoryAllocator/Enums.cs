using System;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        /// <summary>
        /// Flags for created Allocator
        /// </summary>
        [Flags]
        public enum AllocatorFlags : uint
        {
            /// <summary>
            /// Allocator and all objects created from it will not be synchronized internally, so you must guarantee they are used from only one thread at a time or synchronized externally by you.
            /// <para/> Using this flag may increase performance because internal mutexes are not used.
            /// </summary>
            ExternallySynchronized = 0x00000001
        }

        public enum MemoryUsage
        {
            /// <summary>
            /// No intended memory usage specified.
            /// </summary>
            Unknown = 0,
            /// <summary>
            /// Memory will be used on device only, no need to be mapped on host.
            /// </summary>
            GpuOnly = 1,
            /// <summary>
            /// Memory will be mapped on host. Could be used for transfer to device. Guarantees to be <see cref="Vk.MemoryPropertyFlags.HostVisible"/> and <see cref="Vk.MemoryPropertyFlags.HostCoherent"/>.
            /// </summary>
            CpuOnly = 2,
            /// <summary>
            /// Memory will be used for frequent (dynamic) updates from host and reads on device. Guarantees to be <see cref="Vk.MemoryPropertyFlags.HostVisible"/>.
            /// </summary>
            CpuToGpu = 3,
            /// <summary>
            /// Memory will be used for writing on device and readback on host. Guarantees to be <see cref="Vk.MemoryPropertyFlags.HostVisible"/>.
            /// </summary>
            GpuToCpu = 4
        }

        [Flags]
        public enum MemoryRequirementFlags : uint
        {
            OwnMemory = 1 << 0,
            NeverAllocate = 1 << 1,
            PersistentMap = 1 << 2
        }

        public enum SuballocationType
        {
            Free = 0,
            Unknown = 1,
            Buffer = 2,
            ImageUnknown = 3,
            ImageLinear = 4,
            ImageOptimal = 5
        }

        public enum BlockVectorType
        {
            Unmapped,
            Mapped
        }
    }
}