using System.Runtime.CompilerServices;

using static Engine.SDL.SDL;

namespace Engine.SDL
{
    public static class Clipboard
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe void SetText(string text) => Native.SDL_SetClipboardText(text.ToBytePtr()).CheckError("Could not set clipboard text \"" + text + "\"");

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe string GetText() => GetString(Native.SDL_GetClipboardText());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasText() => Native.SDL_HasClipboardText();
    }
}