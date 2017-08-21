using System;
using System.Text;
using System.Runtime.InteropServices;

namespace CoreVulkan
{
    public static unsafe class MarshalUtility
    {
        public static string ToString(byte* pointer, int length)
        {
            int actualLength = 0;
            while (actualLength < length && pointer[actualLength] != 0)
            {
                ++actualLength;
            }
            return Marshal.PtrToStringAnsi(new IntPtr(pointer), actualLength);
        }
    }
}