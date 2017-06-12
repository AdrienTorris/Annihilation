using System;

namespace SDL2
{
    public static class StringExtensions
    {
        public static IntPtr ToIntPtr (this string text)
        {
            return MarshalUtility.StringToUTF8 (text);
        }
    }
}