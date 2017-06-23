namespace TundraEngine
{
    public static class StringExtensions
    {
        public static StringId64 ToHash64(this string str)
        {
            return new StringId64(str);
        }

        public static String8 ToString8(this string str)
        {
            return new String8(str);
        }

        public static String16 ToString16(this string str)
        {
            return new String16(str);
        }

        public static String32 ToString32(this string str)
        {
            return new String32(str);
        }

        public static String64 ToString64(this string str)
        {
            return new String64(str);
        }
    }
}