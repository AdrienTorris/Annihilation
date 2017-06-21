using System;
using System.Runtime.InteropServices;
using ZeroFormatter;

namespace TundraEngine
{
    [ZeroFormattable]
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ObjectHandle<T> : IEquatable<ObjectHandle<T>>
        where T : class
    {
        [Index(0)] public readonly int Index;

        private static T[] _objects;
        private static int[] _handles;
        private static int _nextIndex;

        private static string NotAllocatedMessage => "You must call Allocate(int size) on ObjectHandle<" + typeof(T).Name + ">";

        public ObjectHandle(int index)
        {
            Index = index;
        }

        public T Value
        {
            get
            {
                Assert.IsNotNull(_handles, NotAllocatedMessage);
                return _objects[Index];
            }
            set
            {
                Assert.IsNotNull(_handles, NotAllocatedMessage);
                lock (_objects)
                {
                    int index = _handles[_nextIndex--];
                    _objects[index] = value;
                }
            }
        }

        /// <summary>
        /// Mark the managed object for garbage collection and free the index in the internal array.
        /// </summary>
        public void Free()
        {
            Assert.IsNotNull(_handles, NotAllocatedMessage);
            lock (_objects)
            {
                _objects[Index] = null;
                ++_nextIndex;
                _handles[_nextIndex] = Index;
            }
        }

        public static void Allocate(int size)
        {
            _objects = new T[size + 1];
            _handles = new int[size];
            for (int i = 0, handle = size; i < size; ++i, --handle)
            {
                _handles[i] = handle;
            }
            _nextIndex = size - 1;
        }

        public bool Equals(ObjectHandle<T> other)
        {
            return Index != other.Index;
        }

        public override bool Equals(object obj)
        {
            return obj is ObjectHandle<T> ? Equals((ObjectHandle<T>)obj) : false;
        }

        public static bool operator ==(ObjectHandle<T> a, ObjectHandle<T> b)
        {
            return a.Index == b.Index;
        }

        public static bool operator !=(ObjectHandle<T> a, ObjectHandle<T> b)
        {
            return a.Index != b.Index;
        }

        public override int GetHashCode()
        {
            return Index;
        }

        public override string ToString()
        {
            return typeof(T).Name + " handle (" + Index + ")";
        }

        public static implicit operator int(ObjectHandle<T> value)
        {
            return value.Index;
        }

        public static implicit operator ObjectHandle<T>(int value)
        {
            return new ObjectHandle<T>(value);
        }
    }
}