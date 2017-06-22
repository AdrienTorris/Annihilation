using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        /// <summary>
        /// The SDL keysym structure, used in key events.
        /// </summary>
        /// <remarks>
        /// If you are looking for translated character input, see the TextInput event.
        /// </remarks>
        [StructLayout (LayoutKind.Sequential)]
        public struct SDL_KeySym
        {
            /// <summary>
            /// SDL physical key code - see ScanCode for details
            /// </summary>
            public readonly SDL_ScanCode ScanCode;
            /// <summary>
            /// SDL virtual key code - see KeyCode for details
            /// </summary>
            public readonly SDL_KeyCode Sym;
            /// <summary>
            /// Current key modifiers
            /// </summary>
            public readonly SDL_KeyMod Mod;
            public readonly uint Unused;
        }

        /// <summary>
        /// Get the window which currently has keyboard focus.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr SDL_GetKeyboardFocus ();

        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr SDL_GetKeyboardState (out int numkeys);

        /// <summary>
        /// Get a snapshot of the current state of the keyboard.
        /// </summary>
        /// <paramref name="numkeys"/> Receives the length of the returned array.
        /// <returns/> An array of key states. Indexes into this array are obtained by using <see cref="ScanCode"/> values.
        unsafe public static void SDL_GetKeyboardState (out byte[] keyStates, out int numKeys)
        {
            IntPtr ptr = SDL_GetKeyboardState (out numKeys);
            byte* bytesPtr = (byte*)ptr.ToPointer ();
            keyStates = new byte[numKeys];
            for (int i = 0; i < numKeys; ++i)
            {
                keyStates[i] = bytesPtr[i];
            }
        }

        /// <summary>
        /// Get the current key modifier state for the keyboard.
        /// </summary>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_KeyMod SDL_GetModState ();

        /// <summary>
        /// Set the current key modifier state for the keyboard.
        /// </summary>
        /// <remarks> This does not change the keyboard state, only the key modifier flags. </remarks>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_SetModState (SDL_KeyMod modstate);

        /// <summary>
        /// Get the key code corresponding to the given scancode according to the current keyboard layout. See <see cref="KeyCode"/> for details.
        /// </summary>
        /// <seealso cref="GetKeyName(KeyCode)"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_KeyCode SDL_GetKeyFromScancode (SDL_ScanCode scanCode);

        /// <summary>
        /// Get the scancode corresponding to the given key code according to the current keyboard layout. See <see cref="ScanCode"/> for details.
        /// </summary>
        /// <seealso cref="GetScancodeName(ScanCode)"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_ScanCode SDL_GetScancodeFromKey (SDL_KeyCode key);

        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        unsafe private extern static IntPtr SDL_GetScancodeName (SDL_ScanCode scanCode);

        /// <summary>
        /// Get a human-readable name for a scancode.
        /// </summary>
        /// <seealso cref="ScanCode"/>
        /// <returns> A pointer to the name for the scancode. If the scancode doesn't have a name, this function returns an empty string (""). </returns>
        unsafe public static string SDL_GetScancodeNameString (SDL_ScanCode scanCode)
        {
            return GetString (SDL_GetScancodeName (scanCode));
        }

        /// <summary>
        /// Get a scancode from a human-readable name.
        /// </summary>
        /// <returns> ScanCsode, or <see cref="ScanCode.Unknown"/> if the name wasn't recognized </returns>
        /// <seealso cref="ScanCode"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_ScanCode SDL_GetScancodeFromName (string name);

        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        unsafe private extern static IntPtr SDL_GetKeyName (SDL_KeyCode key);

        /// <summary>
        /// Get a human-readable name for a key.
        /// </summary>
        /// <returns> A pointer to a UTF-8 string that stays valid at least until the next
        ///         call to this function. If you need it around any longer, you must
        ///         copy it.  If the key doesn't have a name, this function returns an
        ///         empty string (""). </returns>
        /// <seealso cref="KeyCode"/>
        unsafe public static string SDL_GetKeyNameString (SDL_KeyCode key)
        {
            return GetString (SDL_GetKeyName (key));
        }

        /// <summary>
        /// Get a key code from a human-readable name
        /// </summary>
        /// <returns/> key code, or <see cref="KeyCode.Unknown"/> if the name wasn't recognized
        /// <seealso cref="KeyCode"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static SDL_KeyCode SDL_GetKeyFromName (string name);

        /// <summary>
        /// Start accepting Unicode text input events. This function will show the on-screen keyboard if supported.
        /// </summary>
        /// <seealso cref="StopTextInput"/>
        /// <seealso cref="SetTextInputRect(out Rect)"/>
        /// <seealso cref="HasScreenKeyboardSupport"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_StartTextInput ();

        /// <summary>
        /// Return whether or not Unicode text input events are enabled.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        /// <seealso cref="StopTextInput"/> 
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_IsTextInputActive ();

        /// <summary>
        /// Stop receiving any text input events. This function will hide the on-screen keyboard if supported.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        /// <seealso cref="HasScreenKeyboardSupport"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_StopTextInput ();

        /// <summary>
        /// Set the rectangle used to type Unicode text inputs. This is used as a hint for IME and on-screen keyboard placement.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void SDL_SetTextInputRect (out SDL_Rect rectangle);

        /// <summary>
        /// Returns whether the platform has some screen keyboard support.
        /// </summary>
        /// <returns/> True if some keyboard support is available else false.
        /// <remarks/> Not all screen keyboard functions are supported on all platforms.
        /// <seealso cref="IsScreenKeyboardShown(IntPtr)"/>
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        public extern static bool SDL_HasScreenKeyboardSupport ();

        /// <summary>
        /// Returns whether the screen keyboard is shown for given window.
        /// </summary>
        /// <param name="window"/> window The window for which screen keyboard should be queried.
        /// <returns/> True if screen keyboard is shown else false.
        /// <seealso cref="HasScreenKeyboardSupport"/> 
        [DllImport (LibName, CallingConvention = CallingConvention.Cdecl)]
        private extern static bool SDL_IsScreenKeyboardShown (IntPtr window);
    }
}