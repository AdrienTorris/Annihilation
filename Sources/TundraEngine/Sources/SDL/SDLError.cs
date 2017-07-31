using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        /// <summary>
        /// Use this function to set the SDL error message.
        /// </summary>
        /// <returns> Returns always -1. </returns>
        /// <param name="fmt"> A printf() style message format string </param>
        /// <param name="objects"> Additional parameters matching % tokens in the fmt string, if any </param>
        /// <remarks> Calling this function will replace any previous error message that was set. </remarks>
        [DllImport (LibraryName)]
        unsafe public extern static int SDL_SetError (string fmt, params object[] objects);
        
        [DllImport (LibraryName)]
        unsafe private extern static byte* SDL_GetError ();

        /// <summary>
        /// Use this function to retrieve a message about the last error that occurred.
        /// </summary>
        /// <returns> Returns a message with information about the specific error that occurred, or an empty string if there hasn't been an error message set since the last call to <see cref="Clear"/>. The message is only applicable when an SDL function has signaled an error. You must check the return values of SDL function calls to determine when to appropriately call <see cref="Get"/>. </returns>
        /// <remarks> 
        /// It is possible for multiple errors to occur before calling <see cref="Get"/>. Only the last error is returned. 
        /// <para /> The returned string is statically allocated and must not be freed by the application.
        /// </remarks>
        unsafe public static string GetError ()
        {
            return GetString(SDL_GetError ());
        }

        /// <summary>
        /// Use this function to clear any previous error message.
        /// </summary>
        [DllImport (LibraryName)]
        public extern static void SDL_ClearError ();
    }
}
