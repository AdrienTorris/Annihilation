using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

        public static void Load<T>(string path, out T resource) where T : IResource, new()
        {
            resource = await LoadAsync<T>(path);
        }

        public static async Task<T> LoadAsync<T>(string path) where T : IResource, new()
        {
            T resource = new T();
            byte[] bytes;
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                resource.Name = file.Name;
                bytes = new byte[file.Length];
                await file.ReadAsync(bytes, 0, (int)file.Length);
            }

            resource.NumBytes = bytes.Length;
            resource.Bytes = &bytes;
        }

        public static void Unload()
        {

        }

        public static void RegisterType(StringId64 type)
        {

        }
    }
}