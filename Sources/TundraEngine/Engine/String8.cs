using System.Text;

namespace Engine
{
    /// <summary>
    /// An 8 byte UTF8 string.
    /// </summary>
    unsafe public struct String8
    {
        public fixed byte Bytes[NumBytes];

        public const int NumBytes = 8;

        public String8(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            fixed (byte* byteAddress = Bytes)
            {
                for (int i = 0; i < bytes.Length; ++i)
                {
                    *(byteAddress + i) = bytes[i];
                }
            }
        }

        public String8(byte[] utf8Bytes)
        {
            fixed (byte* byteAddress = Bytes)
            {
                for (int i = 0; i < utf8Bytes.Length; ++i)
                {
                    *(byteAddress + i) = utf8Bytes[i];
                }
            }
        }

        public override string ToString()
        {
            byte[] bytes = new byte[NumBytes];
            fixed (byte* byteAddress = Bytes)
            {
                for (int i = 0; i < NumBytes; ++i)
                {
                    bytes[i] = *byteAddress;
                }
            }

            return Encoding.UTF8.GetString(bytes, 0, NumBytes);
        }
    }
}