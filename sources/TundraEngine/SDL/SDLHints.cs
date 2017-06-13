using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    /// <summary>
    /// Type definition of the hint callback function.
    /// </summary>
    [UnmanagedFunctionPointer (CallingConvention.Cdecl)]
    public delegate void HintCallback (IntPtr userData, IntPtr name, IntPtr oldValue, IntPtr newValue);

    /// <summary>
    /// An enumeration of hint priorities
    /// </summary>
    public enum HintPriority
    {
        Default,
        Normal,
        Override
    }

    public static partial class SDL
    {
        /// <summary>
        /// A variable controlling how 3D acceleration is used to accelerate the SDL screen surface.
        /// <para /> SDL can try to accelerate the SDL screen surface by using streaming textures with a 3D rendering engine.This variable controls whether and how this is done.
        /// <para /> This variable can be set to the following values:
        /// "0"       - Disable 3D acceleration
        /// "1"       - Enable 3D acceleration, using the default renderer.
        /// "X"       - Enable 3D acceleration, using X where X is one of the valid rendering drivers.  (e.g. "direct3d", "opengl", etc.)
        /// <para /> By default SDL tries to make a best guess for each platform whether to use acceleration or not.
        /// </summary>
        public const string HintFrameBufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
        /// <summary>
        /// A variable controlling the scaling policy for <see cref="RenderSetLogicalSize"/>.
        /// <para /> This variable can be set to the following values:
        /// "0" or "letterbox" - Uses letterbox/sidebars to fit the entire rendering on screen
        /// "1" or "overscan"  - Will zoom the rendering so it fills the entire screen, allowing edges to be drawn offscreen
        /// <para /> By default letterbox is used
        /// </summary>
        public const string HintRenderLogicalSizeMode = "SDL_HINT_RENDER_LOGICAL_SIZE_MODE";

        // TODO: Add all other non-renderer specific hints

        [DllImport (LibName, EntryPoint = "SDL_SetHintWithPriority", CallingConvention = CallingConvention.Cdecl)]
        private extern static bool SetHintWithPriorityInternal (IntPtr name, IntPtr value, HintPriority priority);

        /// <summary>
        /// Set a hint with a specific priority
        /// <para /> The priority controls the behavior when setting a hint that already has a value.Hints will replace existing hints of their priority and lower.Environment variables are considered to have override priority.
        /// </summary>
        /// <returns> True if the hint was set, false otherwise </returns>
        public static bool SetHintWithPriority (string name, string value, HintPriority priority)
        {
            return SetHintWithPriorityInternal (name.ToIntPtr (), value.ToIntPtr (), priority);
        }

        [DllImport (LibName, EntryPoint = "SDL_SetHint", CallingConvention = CallingConvention.Cdecl)]
        private extern static bool SetHintInternal (IntPtr name, IntPtr value);

        /// <summary>
        /// Set a hint with normal priority.
        /// </summary>
        /// <returns> True if the hint was set, false otherwise </returns>
        public static bool SetHint (string name, string value)
        {
            return SetHintInternal (name.ToIntPtr (), value.ToIntPtr ());
        }

        [DllImport (LibName, EntryPoint = "SDL_GetHint", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetHintInternal (IntPtr name);

        /// <summary>
        /// Get a hint.
        /// </summary>
        /// <returns> The string value of a hint variable. </returns>
        public static string GetHint (string name)
        {
            return GetHintInternal (name.ToIntPtr ()).ToStr ();
        }

        [DllImport (LibName, EntryPoint = "SDL_GetHintBoolean", CallingConvention = CallingConvention.Cdecl)]
        private extern static bool GetHintBooleanInternal (IntPtr name, bool defaultValue);

        /// <summary>
        /// Get a hint.
        /// </summary>
        /// <returns> The boolean value of a hint variable. </returns>
        public static bool GetHintBoolean (string name, bool defaultValue)
        {
            return GetHintBooleanInternal (name.ToIntPtr (), defaultValue);
        }

        [DllImport (LibName, EntryPoint = "SDL_AddHintCallback", CallingConvention = CallingConvention.Cdecl)]
        private extern static void AddHintCallbackInternal (IntPtr name, HintCallback callback, IntPtr userData);

        /// <summary>
        /// Add a function to watch a particular hint.
        /// </summary>
        /// <param name="name"> The hint to watch </param>
        /// <param name="callback"> The function to call when the hint value changes </param>
        /// <param name="userData"> A pointer to pass to the callback function </param>
        public static void AddHintCallback (string name, HintCallback callback, IntPtr userData)
        {
            AddHintCallbackInternal (name.ToIntPtr (), callback, userData);
        }

        [DllImport (LibName, EntryPoint = "SDL_DelHintCallback", CallingConvention = CallingConvention.Cdecl)]
        private extern static void DelHintCallbackInternal (IntPtr name, HintCallback callback, IntPtr userData);

        /// <summary>
        /// Remove a function watching a particular hint.
        /// </summary>
        /// <param name="name"> The hint to watch </param>
        /// <param name="callback"> The function to call when the hint value changes </param>
        /// <param name="userData"> A pointer to pass to the callback function </param>
        public static void DelHintCallback (string name, HintCallback callback, IntPtr userData)
        {
            DelHintCallbackInternal (name.ToIntPtr (), callback, userData);
        }

        /// <summary>
        /// Clear all hints.
        /// <para /> This function is called during <see cref="Quit"/> to free stored hints.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_ClearHints", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ClearHints ();
    }
}