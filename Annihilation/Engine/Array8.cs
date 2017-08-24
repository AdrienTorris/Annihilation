using System;
using System.Runtime.InteropServices;

namespace Engine
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Array8<T>
    {
        public int Length;
        public IntPtr Data;

        public const int ElementSize = 8;

        public Array8(int length)
        {
            Data = Marshal.AllocHGlobal(length * ElementSize);
            Length = length;
        }

        unsafe public T this[int index]
        {
            get
            {
                Assert.IsTrue(index < Length, "Index is out of bounds.");
                return Marshal.PtrToStructure<T>(Data + ElementSize * index);
            }
        }

        public void Free()
        {
            Marshal.FreeHGlobal(Data);
            Data = IntPtr.Zero;
            Length = 0;
        }
    }
}