using System;
using System.Security;
using System.Runtime.InteropServices;

namespace TundraEngine.SDL
{
    [SuppressUnmanagedCodeSecurity]
    public static partial class SDL
    {
        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        unsafe private extern static IntPtr SDL_GetBasePath ();

        /// <summary>
        /// Use this function to get the directory where the application was run from. This is where the application data directory is.
        /// </summary>
        /// <returns>
        /// Returns an absolute path in UTF-8 encoding to the application data directory. NULL will be returned on error or when the platform doesn't implement this functionality, call <see cref="GetError"/> for more information.
        /// <para /> The return path will be guaranteed to end with a path separator ('\' on Windows, '/' on most other platforms)
        /// <para /> The pointer returned is owned by you. Please call SDL_free() on the pointer when you are done with it.
        /// </returns>
        /// <remarks>
        /// This is not necessarily a fast call, though, so you should call this once near startup and save the string if you need it.
        /// </remarks>
        unsafe public static string SDL_GetBasePathString ()
        {
            return GetString(SDL_GetBasePath ());
        }

        [DllImport (LibraryName, CallingConvention = CallingConvention.Cdecl)]
        unsafe private extern static IntPtr SDL_GetPrefPath (string org, string app);

        /// <summary>
        /// Use this function to get the "pref dir". This is meant to be where the application can write personal files (Preferences and save games, etc.) that are specific to the application. This directory is unique per user and per application.
        /// </summary>
        /// <returns>
        /// Returns a UTF-8 string of the user directory in platform-dependent notation. NULL if there's a problem (creating directory failed, etc.).
        /// <para /> The return path will be guaranteed to end with a path separator ('\' on Windows, '/' on most other platforms)
        /// <para /> The pointer returned is owned by you. Please call SDL_free() on the pointer when you are done with it.
        /// </returns>
        /// <remarks>
        /// You should assume the path returned by this function is the only safe place to write files (and that <see cref="GetBasePath"/>, while it might be writable, or even the parent of the returned path, aren't where you should be writing things).
        /// </remarks>
        unsafe public static string SDL_GetPrefPathString (string org, string app)
        {
            return GetString (SDL_GetPrefPath (org, app));
        }
    }
}