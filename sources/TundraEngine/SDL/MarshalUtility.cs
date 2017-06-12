using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SDL2
{
    unsafe public static class MarshalUtility
    {
        public static string UTF8ToString (IntPtr textPtr)
        {
            return Marshal.PtrToStringUTF8 (textPtr);
        }

        public static IntPtr StringToUTF8 (string text)
        {
            if (text == null)
            {
                return IntPtr.Zero;
            }
            else
            {
                byte[] bytes = Encoding.UTF8.GetBytes (text);
                IntPtr ptr = Marshal.AllocHGlobal (bytes.Length + 1);
                Marshal.Copy (bytes, 0, ptr, bytes.Length);
                Marshal.WriteByte (ptr, bytes.Length, 0);
                return ptr;
            }
        }
    }
}