using System.Text;
using System.Runtime.InteropServices;

namespace Engine
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct Text
    {
        public byte* ByteArray;
        public int Length;

        public const int DefaultLength = 32;

        public Text(byte* nativeHandle, int length)
        {
            ByteArray = nativeHandle;
            Length = length;
        }
        
        public Text(string text)
        {
            if (text == null)
            {
                ByteArray = null;
                Length = 0;
                return;
            }

            byte[] bytes = Encoding.UTF8.GetBytes(text);
            fixed (byte* ptr = &bytes[0])
            {
                ByteArray = ptr;
            }
            Length = text.Length;
        }
        
        public override string ToString()
        {
            byte[] sourceBytes = new byte[Length];
            int length = 0;

            for (int i = 0; i < Length; i++)
            {
                if (ByteArray[i] == 0)
                    break;

                sourceBytes[i] = ByteArray[i];
                length++;
            }
            return Encoding.UTF8.GetString(sourceBytes, 0, length);
        }

        public static implicit operator string(Text text)
        {
            return text.ToString();
        }

        public static implicit operator Text(string text)
        {
            return new Text(text);
        }

        public static implicit operator byte*(Text text)
        {
            return text.ByteArray;
        }

        public static implicit operator Text(byte* pointer)
        {
            return new Text(pointer, DefaultLength);
        }
    }
}