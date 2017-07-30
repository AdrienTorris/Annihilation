using System.Text;

namespace TundraEngine.SDL
{
    public static class StringExtensions
    {
        public static unsafe byte* ToAddress(this string str)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            fixed (byte* ptr = &bytes[0])
            {
                return ptr;
            }
        }
    }
}