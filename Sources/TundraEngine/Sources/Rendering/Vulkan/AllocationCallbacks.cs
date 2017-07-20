using System;

namespace TundraEngine.Rendering
{
    public struct AllocationCallbacks
    {
        public IntPtr UserData;
        public IntPtr Allocation;
        public IntPtr Reallocation;
        public IntPtr InternalAllocation;
        public IntPtr InternalFree;
    }
}