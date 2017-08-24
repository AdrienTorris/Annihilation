using System.Text;

namespace Engine
{
    /// <summary>
    /// A 16 byte UTF8 string.
    /// </summary>
    unsafe public struct String16
    {
        public fixed byte Bytes[NumBytes];

        public const int NumBytes = 16;

        public String16(string str)
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

        public String16(byte[] utf8Bytes)
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