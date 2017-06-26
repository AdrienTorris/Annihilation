using System.Text;

namespace TundraEngine
{
    /// <summary>
    /// A 32 byte UTF8 string.
    /// </summary>
    unsafe public struct String32
    {
        public fixed byte Bytes[NumBytes];

        public const int NumBytes = 32;

        public String32(string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            fixed (byte* byteAddress = Bytes)
            {
                for (int i = 0; i < NumBytes; ++i)
                {
                    *(byteAddress + i) = bytes[i];
                }
            }
        }

        public String32(byte[] utf8Bytes)
        {
            fixed (byte* byteAddress = Bytes)
            {
                for (int i = 0; i < NumBytes; ++i)
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