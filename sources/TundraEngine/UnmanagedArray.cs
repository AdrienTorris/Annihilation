using System;
using System.Runtime.InteropServices;
using ZeroFormatter;

namespace TundraEngine
{
    [ZeroFormattable]
    public unsafe struct UnmanagedArray
    {
        [Index(0)] public int Length;
        [Index(1)] public int ElementSize;
        [Index(2)] public byte* Data;

        public UnmanagedArray(int length, int elementSize)
        {
            Data = (byte*)Marshal.AllocHGlobal(length * elementSize);
            Length = length;
            ElementSize = elementSize;
        }

        public byte* this[int index]
        {
            get
            {
                Assert.IsTrue(index < Length, "Index is out of bounds.");
                return Data + ElementSize * index;
            }
        }

        public void Free()
        {
            Marshal.FreeHGlobal((IntPtr)Data);
            Data = null;
            Length = 0;
        }
    }
}