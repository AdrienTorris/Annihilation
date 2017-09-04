using System.Text;
using Engine.Mathematics;

namespace Engine
{
    public static class StringExtensions
    {
        public static unsafe byte* ToBytes(this string text)
        {
            byte* bytes = null;
            int maxBytes = Encoding.UTF8.GetMaxByteCount(text.Length);
            fixed (char* chars = text)
            {
                Encoding.UTF8.GetBytes(chars, text.Length, bytes, maxBytes);
                return bytes;
            }
        }

        public static uint ToHash32(this string str)
        {
            return MetroHash.Hash32(str);
        }

        public static ulong ToHash64(this string str)
        {
            return MetroHash.Hash64(str);
        }
    }
}