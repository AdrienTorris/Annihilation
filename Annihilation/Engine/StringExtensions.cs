using System.Text;
using System.Runtime.InteropServices;
using Engine.Mathematics;

namespace Engine
{
    public static class StringExtensions
    {
        public static unsafe StringUtf8 ToUtf8(this string str) => new StringUtf8(str);

        public static unsafe char* ToCharPtr(this string text)
        {
            fixed (char* charPtr = text)
            {
                return charPtr;
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