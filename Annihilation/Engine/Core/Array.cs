using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Engine.Core
{
    public unsafe struct Array<T> 
        where T : struct
    {
        public int Count { get; private set; }
        public int Capacity { get; private set; }
        public int ElementSizeInBytes { get; private set; }

        private byte* _data;

        public bool IsEmpty => Count == 0;
        
        public T this[int index]
        {
            get
            {
                return Unsafe.Read<T>(_data + index * ElementSizeInBytes);
            }
        }

        public Array(int capacity)
        {
            Count = 0;
            Capacity = capacity;
            ElementSizeInBytes = Unsafe.SizeOf<T>();
            // PERF: Align memory
            _data = (byte*)Marshal.AllocHGlobal(capacity * ElementSizeInBytes);
        }

        public Array(uint capacity) : this((int)capacity) { }

        public int Add(T element)
        {
            if (Count == Capacity)
            {
                Grow(0);
            }
            
            Unsafe.Write(_data + Count * ElementSizeInBytes, element);

            return Count++;
        }

        public void Remove(int index)
        {
            Assert.IsTrue(Count > 0);
            Assert.IsTrue(index < Count);

            T last = Unsafe.Read<T>(_data + (Count - 1) * ElementSizeInBytes);
            Unsafe.Write(_data + index * ElementSizeInBytes, last);

            --Count;
        }
        
        public void RemoveLast()
        {
            Assert.IsTrue(Count > 0);

            --Count;
        }

        public void Clear()
        {
            Count = 0;
        }

        public void Resize(int count)
        {
            if (count > Capacity)
            {
                SetCapacity(count);
            }

            Count = count;
        }

        public void SetCapacity(int capacity)
        {
            if (capacity < Count)
            {
                Resize(capacity);
            }

            if (capacity > 0)
            {
                void* temp = _data;
                Capacity = capacity;
                _data = (byte*)Marshal.AllocHGlobal(capacity * ElementSizeInBytes);

                Unsafe.CopyBlock(_data, temp, (uint)Count * (uint)ElementSizeInBytes);

                Marshal.FreeHGlobal(new IntPtr(temp));
            }
        }

        public void Grow(int minCapacity)
        {
            int newCapacity = Capacity * 2 + 1;

            if (newCapacity < minCapacity)
            {
                newCapacity = minCapacity;
            }

            SetCapacity(newCapacity);
        }

        public void Condense() => Resize(Count);
    }
}