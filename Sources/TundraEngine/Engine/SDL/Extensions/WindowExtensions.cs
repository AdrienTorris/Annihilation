﻿using System;

namespace Engine.SDL
{
    public static class WindowExtensions
    {
        public static void CheckError(this Window window, string message)
        {
            Assert.IsTrue(window.NativeHandle != IntPtr.Zero, "[SDL] " + message + ": " + SDL.GetError());
        }
    }
}