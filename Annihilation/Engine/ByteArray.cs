using System;
using System.Runtime.InteropServices;

namespace Engine.Memory
{
    unsafe public struct ByteArray
    {
        public int Length;
        public byte* Data;

        public ByteArray(int length)
        {
            Data = (byte*)Marshal.AllocHGlobal(length);
            Length = length;
        }

        public byte* this[int index]
        {
            get
            {
                Assert.IsTrue(index < Length, "Index is out of bounds.");
                return Data + index;
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