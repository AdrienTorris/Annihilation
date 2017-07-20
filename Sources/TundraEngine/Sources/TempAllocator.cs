using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace TundraEngine
{
    public class TempAllocator
    {
        private List<IntPtr> pointers;

        public TempAllocator(int pointerCount)
        {
            pointers = new List<IntPtr>(pointerCount);
        }

        public IntPtr GetString(string str)
        {
            IntPtr strPointer = Marshal.StringToHGlobalAnsi(str);
            pointers.Add(strPointer);
            return strPointer;
        }

        public void Free()
        {
            foreach(IntPtr pointer in pointers)
            {
                Marshal.FreeHGlobal(pointer);
            }
        }
    }
}