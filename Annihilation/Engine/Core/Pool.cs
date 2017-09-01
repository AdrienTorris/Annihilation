using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Collections
{
    public struct Pool<T> : IEnumerable<T> where T : class, new()
    {
        private Array<T> _allocated;

        public int Count;

        public Pool(int capacity)
        {
            _allocated = new Array<T>(capacity);
            Count = 0;
        }

        public T this[int index]
        {
            get { return _allocated.Elements[index]; }
            set
            {
                _allocated.Elements[index] = value;
            }
        }

        public void Clear()
        {
            Count = 0;
        }

        public void Reset()
        {
            Clear();
            for (var i = 0; i < _allocated.Count; i++)
            {
                _allocated[i] = null;
            }
            _allocated.Clear();
        }

        public T Add()
        {
            T result;
            if (Count < _allocated.Count)
            {
                result = _allocated[Count];
            }
            else
            {
                result = new T();
                _allocated.Add(result);
            }
            Count++;
            return result;
        }

        public T Add(Func<T> allocateFunction)
        {

            T result;
            if (Count < _allocated.Count)
            {
                result = _allocated[Count];
            }
            else
            {
                result = allocateFunction();
                _allocated.Add(result);
            }
            Count++;
            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new Array<T>.Enumerator(_allocated.Elements, Count);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Array<T>.Enumerator(_allocated.Elements, Count);
        }
    }
}