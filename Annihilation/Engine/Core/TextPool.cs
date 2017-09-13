using System;
using System.Runtime.InteropServices;

namespace Engine.Collections
{
    public unsafe class TextPool : IDisposable
    {
        private byte* _buffer;
        private int _capacity;
        private int _itemSize;
        private int _count = 0;

        public TextPool(int capacity, int itemSize)
        {
            Assert.IsTrue(capacity > 0);
            Assert.IsTrue(itemSize > 0);

            _capacity = capacity;
            _itemSize = itemSize + 1;
            _count = 0;

            _buffer = (byte*)Marshal.AllocHGlobal(capacity * _itemSize);
        }
        
        public byte* GetEmpty()
        {
            Assert.IsTrue(_count < _capacity, "Pool is full.");

            return _buffer + _count * _itemSize;
        }

        public byte* Get(string str)
        {
            Assert.IsTrue(!string.IsNullOrEmpty(str));
            Assert.IsTrue(_count < _capacity, "Pool is full.");

            for (int i = 0; i < str.Length; ++i)
            {
                _buffer[i + _count * _itemSize] = (byte)str[i];
            }
            _buffer[str.Length] = 0;

            return _buffer + _count * _itemSize;
        }
        
        private void Dispose(bool disposing)
        {
            if (_buffer == null) return;

            Marshal.FreeHGlobal(new IntPtr(_buffer));
        }
        
        ~TextPool()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}