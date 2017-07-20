namespace TundraEngine
{
    unsafe public struct MemoryPool
    {
        public int ChunkSize;
        public int NumChunks;
        public byte* Data;
        public byte* Next;
    }
}