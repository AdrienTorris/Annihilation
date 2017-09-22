using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Annihilation.Core
{
    /// <summary>
    /// Reprensents an UTF-8 encoded Ascii string.
    /// </summary>
    public unsafe struct Text
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
        
        public void Free()
        {
            if (Buffer == null) return;

            Marshal.FreeHGlobal(new IntPtr(Buffer));
        }
        
        public static implicit operator byte*(Text text) => text.Buffer;
    }
}