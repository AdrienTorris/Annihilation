namespace Engine
{
    public static class StringExtensions
    {
        public static Text ToText(this string str)
        {
            return new Text(str);
        }

        public static StringHash32 ToHash32(this string str)
        {
            return new StringHash32(str);
        }

        public static StringHash64 ToHash64(this string str)
        {
            return new StringHash64(str);
        }
    }
}