using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace Engine.Collections
{
    public struct Array<T> : IEnumerable<T>
    {
        private static readonly T[] _empty = new T[0];

        public int Count;
        public T[] Elements;

        public T this[int index]
        {
            get { return Elements[index]; }
            set { Elements[index] = value; }
        }

        public Array(Array<T> array)
        {
            Count = array.Count;
            Elements = array.Elements;
        }

        public Array(T[] elements)
        {
            Count = elements.Length;
            Elements = elements;
        }

        public Array(int capacity)
        {
            Count = 0;
            Elements = capacity == 0 ? _empty : new T[capacity];
        }

        public void Add(T element)
        {
            if (Count == Elements.Length)
            {
                Grow();
            }

            Elements[Count++] = element;
        }

        public void AddRange(Array<T> array)
        {
            foreach (T element in array)
            {
                Add(element);
            }
        }

        public void Insert(int index, T element)
        {
            if (Count == Elements.Length)
            {
                Grow();
            }

            if (index < Count)
            {
                for (int i = Count; i > index; --i)
                {
                    Elements[i] = Elements[i - 1];
                }
            }

            Elements[index] = element;
            Count++;
        }

        public void RemoveAt(int index)
        {
            Assert.IsFalse(index < 0 || index >= Count);
            
            Elements[index] = Elements[Count - 1];
            Count--;
        }

        public void Clear()
        {
            Count = 0;
        }

        public void Grow()
        {
            int newCapacity = Elements.Length * 2;
            T[] newElements = new T[newCapacity];
            Array.Copy(Elements, 0, newElements, 0, Count);
            Elements = newElements;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(Elements, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(Elements, Count);
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Enumerator : IEnumerator<T>, IDisposable, IEnumerator
        {
            private readonly T[] _elements;
            private readonly int _count;
            private int _index;
            private T _current;

            public T Current => _current;
            object IEnumerator.Current => Current;

            public Enumerator(T[] elements, int count)
            {
                _elements = elements;
                _count = count;
                _index = 0;
                _current = default(T);
            }

            public bool MoveNext()
            {
                if (_index < _count)
                {
                    _current = _elements[_index];
                    _index++;
                    return true;
                }

                _index = _count + 1;
                _current = default(T);

                return false;
            }
            
            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default(T);
            }

            public void Dispose() { }
        }
    }
}