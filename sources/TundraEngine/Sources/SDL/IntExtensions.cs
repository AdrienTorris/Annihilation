namespace TundraEngine.SDL
{

    internal static class IntExtensions
    {
        public static void CheckError(this int result, string message)
        {
            Assert.IsTrue(result == 0, "[SDL] " + message + ": " + SDL.GetError());
        }
    }
}