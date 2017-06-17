using System;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    public static partial class SDL
    {
        /// <summary>
        /// Put UTF-8 text into the clipboard
        /// </summary>
        /// <seealso cref="GetText"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        unsafe public extern static int SDL_SetClipboardText (string text);
        
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        unsafe private extern static IntPtr SDL_GetClipboardText ();

        /// <summary>
        /// Get UTF-8 text from the clipboard, which must be freed with SDL_free()
        /// </summary>
        /// <seealso cref="SetText"/>
        unsafe public static string SDL_GetClipboardTextString ()
        {
            return GetString (SDL_GetClipboardText ());
        }

        /// <summary>
        /// Returns a flag indicating whether the clipboard exists and contains a text string that is non-empty
        /// </summary>
        /// <seealso cref="GetText"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasClipboardText ();
    }
}