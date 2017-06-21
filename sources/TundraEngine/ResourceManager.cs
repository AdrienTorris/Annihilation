using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using ZeroFormatter;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    public static class ResourceManager
    {
        private static Dictionary<StringId64, IResource> _loadedResources = new Dictionary<StringId64, IResource>(512);

        public static T Get<T>(StringId64 name) where T : IResource
        {
            if (_loadedResources.TryGetValue(name, out IResource resource))
            {

            }
            else
            {

            }
        }

        // TODO: Compare performance of .Net vs SDL
        public static void ReadFileNative(string path, out byte[] bytes)
        {
            IntPtr rwOps = SDL_RWFromFile(path, "rb");
            Assert.IsFalse(rwOps == IntPtr.Zero, "Could not open the file \"" + path + "\"");
            int size = (int)SDL_RWsize(rwOps);
            Assert.IsTrue(size >= 0, "Could not get the size of the file \"" + path + "\"");
            IntPtr bytesPtr = Marshal.AllocHGlobal(size);
            SDL_RWread(rwOps, bytesPtr, size, size);
            bytes = new byte[size];
            Marshal.Copy(bytesPtr, bytes, 0, size);
            int result = SDL_RWclose(rwOps);
            Assert.IsZero(result, "Could not close the file \"" + path + "\"");
        }

        public static void Load(string path, out byte[] bytes)
        {
            bytes = File.ReadAllBytes(path);
        }

        public static void Unload()
        {

        }

        public static void RegisterType(StringId64 type)
        {

        }
    }
}