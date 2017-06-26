using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    unsafe public delegate void* LoadFunction(string path);

    public static class ResourceManager
    {
        /*public static async Task<T> LoadAsync<T>(string path) where T : IResource, new()
        {
            T resource = new T();
            byte[] bytes;
            using (var file = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read, 4096, true))
            {
                resource.Name = file.Name;
                bytes = new byte[file.Length];
                await file.ReadAsync(bytes, 0, (int)file.Length);
            }
        }*/
    }
}