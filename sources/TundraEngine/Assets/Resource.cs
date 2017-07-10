using System;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    /// <summary>
    /// Represents a resource stored on disk.
    /// </summary>
    public unsafe struct Resource
    {
        public readonly int NumBytes;
        public readonly byte* Address;

        public Resource(byte[] bytes)
        {
            NumBytes = bytes.Length;
            // TODO: Use a stack? allocator to prevent allocs on each resource.
            IntPtr intPtr = Marshal.AllocHGlobal(bytes.Length);
            Address = (byte*)intPtr.ToPointer();
        }
    }
}