using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    /// <summary>
    /// The SDL keysym structure, used in key events.
    /// </summary>
    /// <remarks>
    /// If you are looking for translated character input, see the TextInput event.
    /// </remarks>
    [StructLayout (LayoutKind.Sequential)]
    public struct KeySym
    {
        /// <summary>
        /// SDL physical key code - see ScanCode for details
        /// </summary>
        public readonly ScanCode ScanCode;
        /// <summary>
        /// SDL virtual key code - see KeyCode for details
        /// </summary>
        public readonly KeyCode Sym;
        /// <summary>
        /// Current key modifiers
        /// </summary>
        public readonly KeyMod Mod;
        public readonly uint Unused;
    }

    public static partial class SDL
    {
        /// <summary>
        /// Get the window which currently has keyboard focus.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetKeyboardFocus", CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr GetKeyboardFocus ();

        [DllImport (LibName, EntryPoint = "SDL_GetKeyboardState", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetKeyboardStateInternal (out int numkeys);

        /// <summary>
        /// Get a snapshot of the current state of the keyboard.
        /// </summary>
        /// <paramref name="numkeys"/> Receives the length of the returned array.
        /// <returns/> An array of key states. Indexes into this array are obtained by using <see cref="ScanCode"/> values.
        public unsafe static void GetKeyboardState (out byte[] keyStates, out int numKeys)
        {
            IntPtr ptr = GetKeyboardStateInternal (out numKeys);
            byte* bytesPtr = (byte*) ptr.ToPointer ();
            keyStates = new byte[numKeys];
            for (int i = 0; i < numKeys; ++i)
            {
                keyStates[i] = bytesPtr[i];
            }
        }

        /// <summary>
        /// Get the current key modifier state for the keyboard.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_GetModState", CallingConvention = CallingConvention.Cdecl)]
        public extern static KeyMod GetModState ();

        /// <summary>
        /// Set the current key modifier state for the keyboard.
        /// </summary>
        /// <remarks> This does not change the keyboard state, only the key modifier flags. </remarks>
        [DllImport (LibName, EntryPoint = "SDL_SetModState", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetModState (KeyMod modstate);

        /// <summary>
        /// Get the key code corresponding to the given scancode according to the current keyboard layout. See <see cref="KeyCode"/> for details.
        /// </summary>
        /// <seealso cref="GetKeyName(KeyCode)"/>
        [DllImport (LibName, EntryPoint = "SDL_GetKeyFromScancode", CallingConvention = CallingConvention.Cdecl)]
        public extern static KeyCode GetKeyFromScanCode (ScanCode scanCode);

        /// <summary>
        /// Get the scancode corresponding to the given key code according to the current keyboard layout. See <see cref="ScanCode"/> for details.
        /// </summary>
        /// <seealso cref="GetScancodeName(ScanCode)"/>
        [DllImport (LibName, EntryPoint = "SDL_GetScancodeFromKey", CallingConvention = CallingConvention.Cdecl)]
        public extern static ScanCode GetScanCodeFromKey (KeyCode key);

        [DllImport (LibName, EntryPoint = "SDL_GetScancodeName", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetScanCodeNameInternal (ScanCode scanCode);

        /// <summary>
        /// Get a human-readable name for a scancode.
        /// </summary>
        /// <seealso cref="ScanCode"/>
        /// <returns> A pointer to the name for the scancode. If the scancode doesn't have a name, this function returns an empty string (""). </returns>
        public static string GetScanCodeName (ScanCode scanCode)
        {
            return GetScanCodeNameInternal (scanCode).ToStr ();
        }

        [DllImport (LibName, EntryPoint = "SDL_GetScancodeFromName", CallingConvention = CallingConvention.Cdecl)]
        private extern static ScanCode GetScanCodeFromNameInternal (IntPtr name);

        /// <summary>
        /// Get a scancode from a human-readable name.
        /// </summary>
        /// <returns> ScanCsode, or <see cref="ScanCode.Unknown"/> if the name wasn't recognized </returns>
        /// <seealso cref="ScanCode"/>
        public static ScanCode GetScanCodeFromName (string name)
        {
            return GetScanCodeFromNameInternal (name.ToIntPtr ());
        }

        [DllImport (LibName, EntryPoint = "SDL_GetKeyName", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetKeyNameInternal (KeyCode key);

        /// <summary>
        /// Get a human-readable name for a key.
        /// </summary>
        /// <returns> A pointer to a UTF-8 string that stays valid at least until the next
        ///         call to this function. If you need it around any longer, you must
        ///         copy it.  If the key doesn't have a name, this function returns an
        ///         empty string (""). </returns>
        /// <seealso cref="KeyCode"/>
        public static string GetKeyName (KeyCode key)
        {
            return GetKeyNameInternal (key).ToStr ();
        }

        [DllImport (LibName, EntryPoint = "SDL_GetKeyFromName", CallingConvention = CallingConvention.Cdecl)]
        private extern static KeyCode GetKeyFromNameInternal (IntPtr name);

        /// <summary>
        /// Get a key code from a human-readable name
        /// </summary>
        /// <returns/> key code, or <see cref="KeyCode.Unknown"/> if the name wasn't recognized
        /// <seealso cref="KeyCode"/>
        public static KeyCode GetKeyFromName (string name)
        {
            return GetKeyFromNameInternal (name.ToIntPtr ());
        }

        /// <summary>
        /// Start accepting Unicode text input events. This function will show the on-screen keyboard if supported.
        /// </summary>
        /// <seealso cref="StopTextInput"/>
        /// <seealso cref="SetTextInputRect(out Rect)"/>
        /// <seealso cref="HasScreenKeyboardSupport"/>
        [DllImport (LibName, EntryPoint = "SDL_StartTextInput", CallingConvention = CallingConvention.Cdecl)]
        public extern static void StartTextInput ();

        /// <summary>
        /// Return whether or not Unicode text input events are enabled.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        /// <seealso cref="StopTextInput"/> 
        [DllImport (LibName, EntryPoint = "SDL_IsTextInputActive", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool IsTextInputActive ();

        /// <summary>
        /// Stop receiving any text input events. This function will hide the on-screen keyboard if supported.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        /// <seealso cref="HasScreenKeyboardSupport"/>
        [DllImport (LibName, EntryPoint = "SDL_StopTextInput", CallingConvention = CallingConvention.Cdecl)]
        public extern static void StopTextInput ();

        /// <summary>
        /// Set the rectangle used to type Unicode text inputs. This is used as a hint for IME and on-screen keyboard placement.
        /// </summary>
        /// <seealso cref="StartTextInput"/>
        [DllImport (LibName, EntryPoint = "SDL_SetTextInputRect", CallingConvention = CallingConvention.Cdecl)]
        public extern static void SetTextInputRect (out Rect rect);

        /// <summary>
        /// Returns whether the platform has some screen keyboard support.
        /// </summary>
        /// <returns/> True if some keyboard support is available else false.
        /// <remarks/> Not all screen keyboard functions are supported on all platforms.
        /// <seealso cref="IsScreenKeyboardShown(IntPtr)"/>
        [DllImport (LibName, EntryPoint = "SDL_HasScreenKeyboardSupport", CallingConvention = CallingConvention.Cdecl)]
        public extern static bool HasScreenKeyboardSupport ();

        /// <summary>
        /// Returns whether the screen keyboard is shown for given window.
        /// </summary>
        /// <param name="window"/> window The window for which screen keyboard should be queried.
        /// <returns/> True if screen keyboard is shown else false.
        /// <seealso cref="HasScreenKeyboardSupport"/> 
        [DllImport (LibName, EntryPoint = "SDL_IsScreenKeyboardShown", CallingConvention = CallingConvention.Cdecl)]
        private extern static bool IsScreenKeyboardShownInternal (IntPtr window);
    }
}