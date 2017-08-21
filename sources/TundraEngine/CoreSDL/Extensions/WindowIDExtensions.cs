using System.Runtime.CompilerServices;

namespace SDL2
{
    public static class WindowIDExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this WindowID windowID, string message)
        {
            Assert.IsTrue(windowID.Handle != 0, "[SDL] " + message + ": " + SDL.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static WindowID CheckErrorAndReturn(this WindowID windowID, string message)
        {
            Assert.IsTrue(windowID.Handle != 0, "[SDL] " + message + ": " + SDL.GetError());
            return windowID;
        }
    }
}