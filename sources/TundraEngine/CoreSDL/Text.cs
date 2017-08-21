using System;
using System.Text;
using System.Runtime.InteropServices;

namespace SDL2
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Text
    {
        internal byte* NativeHandle;

        public Text(byte* nativeHandle)
        {
            NativeHandle = nativeHandle;
        }

        public Text(IntPtr nativeHandle)
        {
            NativeHandle = (byte*)nativeHandle;
        }

        public Text(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            fixed(byte* ptr = &bytes[0])
            {
                NativeHandle = ptr;
            }
        }

        public override string ToString()
        {
            // Count the length of the string
            byte* counter = NativeHandle;
            while (*counter != 0)
            {
                counter++;
            }
            int count = (int)(counter - NativeHandle);

            return Encoding.UTF8.GetString(NativeHandle, count);
        }

        public static implicit operator string(Text text)
        {
            return text.ToString();
        }

        public static implicit operator Text(string text)
        {
            return new Text(text);
        }
    }
}