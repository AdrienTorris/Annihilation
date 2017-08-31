using System;
using System.Threading;
using System.Collections.Generic;

namespace Vulkan.MemoryAllocator
{
    public static partial class Vma
    {
        public class MutexLock
        {
            private object _locker;

            public MutexLock(bool useMutex)
            {
                _locker = useMutex ? new object() : null;
                if (_locker != null)
                {
                    Monitor.Enter(_locker);
                }
            }

            public void Unlock()
            {
                Monitor.Exit(_locker);
            }
        }

        public class BlockVector
        {
            private Allocator _allocator;

            public List<Block> Blocks;

            public bool IsEmpty => Blocks.Count == 0;

            public BlockVector(Allocator allocator)
            {
                _allocator = allocator;
            }

            public void Destroy()
            {
                foreach (Block block in Blocks)
                {
                    Blocks.Remove(block);
                }
            }

            public void Remove(Block block)
            {
                Blocks.Remove(block);
            }

            public void IncrementallySortBlocks()
            {

            }

            public void AddStats(ref Stats stats, uint memTypeIndex, uint memHeapIndex)
            {

            }

#if ENABLE_STATS
            public void PrintDetailedMap(StringBuilder sb)
            {

            }
#endif

            public void UnmapPersistentlyMappedMemory()
            {

            }

            public Vk.Result MapPersistentlyMappedMemory()
            {

            }
        }
    }
}