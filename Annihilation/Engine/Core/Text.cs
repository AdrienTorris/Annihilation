using System;
using System.Text;
using System.Runtime.InteropServices;

namespace Engine.Collections
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Text
    {
        public byte* ByteArray;

        public Text(byte* nativeHandle)
        {
            ByteArray = nativeHandle;
        }

        public Text(IntPtr nativeHandle)
        {
            ByteArray = (byte*)nativeHandle;
        }

        public Text(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            fixed (byte* ptr = &bytes[0])
            {
                ByteArray = ptr;
            }
        }

        public string ToString(int size = 32)
        {
            byte[] sourceBytes = new byte[size];
            int length = 0;

            for (int i = 0; i < size; i++)
            {
                if (ByteArray[i] == 0)
                    break;

                sourceBytes[i] = ByteArray[i];
                length++;
            }
            return Encoding.UTF8.GetString(sourceBytes, 0, length);
        }

        public override string ToString()
        {
            return ToString(32);
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