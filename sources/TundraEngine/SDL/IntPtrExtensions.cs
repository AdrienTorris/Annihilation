using System;

namespace SDL2
{
    public static class IntPtrExtensions
    {
        public static string ToStr (this IntPtr ptr)
        {
            return MarshalUtility.UTF8ToString (ptr);
        }
    }
}