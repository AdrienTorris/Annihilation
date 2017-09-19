using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Annihilation
{
    public unsafe class Text : IDisposable
    {
        public byte* Buffer;

        public byte** BufferPtr
        {
            get
            {
                fixed (byte** ptr = &Buffer)
                {
                    return ptr;
                }
            }
        }

        public Text()
        {
            Buffer = null;
        }

        public Text(string str)
        {
            Debug.Assert(str != null);

            Buffer = (byte*)Marshal.AllocHGlobal(str.Length + 1);
            for (int i = 0; i < str.Length; ++i)
            {
                Buffer[i] = (byte)str[i];
            }
            Buffer[str.Length] = 0;
        }
        
        private void Dispose(bool disposing)
        {
            if (Buffer == null) return;

            Marshal.FreeHGlobal(new IntPtr(Buffer));
        }
        
        ~Text()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public static implicit operator byte*(Text text) => text.Buffer;
    }
}