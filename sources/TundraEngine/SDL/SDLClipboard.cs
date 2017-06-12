using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        [DllImport (LibName, EntryPoint = "SDL_SetClipboardText", CallingConvention = CallingConvention.Cdecl)]
        private extern static int SetClipboardTextInternal (IntPtr text);

        /// <summary>
        /// Put UTF-8 text into the clipboard
        /// </summary>
        /// <seealso cref="GetClipboardText"/>
        public static int SetClipboardText (string text)
        {
            return SetClipboardTextInternal (text.ToIntPtr ());
        }

        [DllImport (LibName, EntryPoint = "SDL_GetClipboardText", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetClipboardTextInternal ();

        /// <summary>
        /// Get UTF-8 text from the clipboard, which must be freed with SDL_free()
        /// </summary>
        /// <seealso cref="SetClipboardText"/>
        public static string GetClipboardText ()
        {
            return GetClipboardTextInternal ().ToStr ();
        }

        /// <summary>
        /// Returns a flag indicating whether the clipboard exists and contains a text string that is non-empty
        /// </summary>
        /// <seealso cref="GetClipboardText"/>
        [DllImport (LibName, EntryPoint = "SDL_HasClipboardText", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasClipboardText ();
    }
}