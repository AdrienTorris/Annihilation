using System;
using System.Runtime.InteropServices;

namespace Engine
{
    unsafe public static class PoolAllocator
    {
        public static MemoryPool CreatePool(int chunkSize, int numChunks)
        {
            MemoryPool pool = new MemoryPool();
            pool.Next = null;
            pool.NumChunks = numChunks;
            pool.ChunkSize = chunkSize;
            pool.Data = (byte*)Marshal.AllocHGlobal(chunkSize * numChunks);

            FreeAllChunks(&pool);

            return pool;
        }

        public static void FreePool(MemoryPool* pool)
        {
            IntPtr ptr = (IntPtr)pool->Data;
            Assert.IsFalse(ptr == IntPtr.Zero, "Pointer is null.");
            Marshal.FreeHGlobal(ptr);
            pool->Data = null;
            pool->Next = null;
        }

        public static byte* GetChunk(MemoryPool* pool)
        {
            byte* result = pool->Next;
            pool->Next = *((byte**)pool->Next);
            return result;
        }

        public static byte* GetClearedChunk(MemoryPool* pool)
        {
            byte* chunk = GetChunk(pool);
            SetBytes(chunk, 0, pool->NumChunks);
            return chunk;
        }

        public static void SetBytes(byte* chunk, byte value, int count)
        {
            Assert.IsTrue(chunk != null, "Chunk address is null.");
            Assert.IsTrue(count >= 0, "Count must be positive.");

            byte* current = chunk;
            for (int i = 0; i < count; ++i)
            {
                *current++ = value;
            }
        }

        public static void FreeChunk(MemoryPool* pool, byte* chunk)
        {
            Assert.IsTrue(pool != null, "Pool is null.");
            Assert.IsTrue(pool->Data != null, "Pool was not allocated or was freed.");

            if (chunk == null) return;
            Assert.IsTrue(
                chunk >= pool->Data &&
                chunk < pool->Data + pool->ChunkSize * pool->NumChunks &&
                (((uint)(chunk - pool->Data)) % pool->ChunkSize) == 0,
                "Chunk pointer does not point to chunk in pool.");

            // TODO: This is weird...
            byte** head = (byte**)chunk;
            *head = pool->Next;
            pool->Next = *head;
        }

        public static void FreeAllChunks(MemoryPool* pool)
        {
            void** current = (void**)pool->Data;
            byte* next = pool->Data + pool->ChunkSize;

            // Point each block except the last one to the next block
            for (int i = 0, count = pool->NumChunks - 1; i < count; ++i)
            {
                *current = next;
                current = (void**)next;
                next += pool->ChunkSize;
            }

            *current = default(void*);

            // The first block is now the head of the free list
            pool->Next = pool->Data;
        }
    }
}