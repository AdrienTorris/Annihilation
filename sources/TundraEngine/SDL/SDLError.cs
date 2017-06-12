using System;
using System.Runtime.InteropServices;

namespace SDL2
{
    public static partial class SDL
    {
        [DllImport (LibName, EntryPoint = "SDL_SetError", CallingConvention = CallingConvention.Cdecl)]
        extern private static int SetErrorInternal (IntPtr fmt, __arglist);

        /// <summary>
        /// Use this function to set the SDL error message.
        /// </summary>
        /// <returns> Returns always -1. </returns>
        /// <param name="fmt"> A printf() style message format string </param>
        /// <param name="objects"> Additional parameters matching % tokens in the fmt string, if any </param>
        /// <remarks> Calling this function will replace any previous error message that was set. </remarks>
        public static int SetError (string fmt, params object[] objects)
        {
            return SetErrorInternal (fmt.ToIntPtr (), __arglist (objects));
        }

        [DllImport (LibName, EntryPoint = "SDL_GetError", CallingConvention = CallingConvention.Cdecl)]
        private extern static IntPtr GetErrorInternal ();

        /// <summary>
        /// Use this function to retrieve a message about the last error that occurred.
        /// </summary>
        /// <returns> Returns a message with information about the specific error that occurred, or an empty string if there hasn't been an error message set since the last call to <see cref="ClearError"/>. The message is only applicable when an SDL function has signaled an error. You must check the return values of SDL function calls to determine when to appropriately call <see cref="GetError"/>. </returns>
        /// <remarks> 
        /// It is possible for multiple errors to occur before calling <see cref="GetError"/>. Only the last error is returned. 
        /// <para /> The returned string is statically allocated and must not be freed by the application.
        /// </remarks>
        public static string GetError ()
        {
            return GetErrorInternal ().ToStr ();
        }

        /// <summary>
        /// Use this function to clear any previous error message.
        /// </summary>
        [DllImport (LibName, EntryPoint = "SDL_ClearError", CallingConvention = CallingConvention.Cdecl)]
        public extern static void ClearError ();
    }
}
