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
    }
}