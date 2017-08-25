using System.Runtime.CompilerServices;

namespace SDL2
{
    public static class IntExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this int result, string message)
        {
            Assert.IsTrue(result == 0, "[SDL] " + message + ": " + SDL.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int CheckErrorAndReturn(this int result, string message)
        {
            Assert.IsTrue(result == 0, "[SDL] " + message + ": " + SDL.GetError());
            return result;
        }
    }
}