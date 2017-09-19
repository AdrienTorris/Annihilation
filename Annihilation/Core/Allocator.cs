namespace Annihilation.Core
{
    /// <summary>
    /// Base interface for memory allocators.
    /// </summary>
    public unsafe interface IAllocator
    {
        void* Allocate(int sizeInBytes, int alignmentInBytes = 16);

        void Free(void* data);
    }
} 