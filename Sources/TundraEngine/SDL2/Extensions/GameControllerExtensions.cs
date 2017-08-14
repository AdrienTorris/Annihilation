using System;
using System.Runtime.CompilerServices;

namespace SDL2
{
    public static class GameControllerExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this GameController gameController, string message)
        {
            Assert.IsTrue(gameController.NativeHandle != IntPtr.Zero, "[SDL] " + message + ": " + SDL.GetError());
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static GameController CheckErrorAndReturn(this GameController gameController, string message)
        {
            Assert.IsTrue(gameController.NativeHandle != IntPtr.Zero, "[SDL] " + message + ": " + SDL.GetError());
            return gameController;
        }
    }
}