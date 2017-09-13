using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine.Collections
{
    public unsafe struct Array<T> where T : struct
    {
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public int ElementSize { get; private set; }
        public byte* Buffer { get; private set; }
        
        public ref T this[int index]
        {
            get
            {
                return ref Unsafe.AsRef<T>(Buffer + index * ElementSize);
            }
        }

        public T this[uint index]
        {
            set
            {
                Unsafe.Write(Buffer + index * ElementSize, value);
            }
        }
        
        public Array(int capacity)
        {
            Count = 0;
            Capacity = capacity;
            ElementSize = Unsafe.SizeOf<T>();
            Buffer = (byte*)Marshal.AllocHGlobal(capacity * ElementSize);
        }

        public Array(uint capacity) : this((int)capacity) { }

        public void Add(ref T element)
        {
            if (Count == Capacity)
            {
                Grow();
            }

            Buffer = (byte*)Unsafe.AsPointer(ref element);
        }
        
        public void Clear()
        {

        }

        public void Grow()
        {
            Capacity *= 2;

        }
    }
}