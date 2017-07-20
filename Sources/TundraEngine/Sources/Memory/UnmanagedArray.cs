using System;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    unsafe public struct UnmanagedArray
    {
        public int Length;
        public int ElementSize;
        public byte* Data;

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