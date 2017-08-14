using System;

namespace SDL2
{
    public static class BoolExtensions
    {
        public static void CheckError(this bool value, string message)
        {
            Assert.IsTrue(value, "[SDL] " + message + ": " + SDL.GetError());
        }
    }
}