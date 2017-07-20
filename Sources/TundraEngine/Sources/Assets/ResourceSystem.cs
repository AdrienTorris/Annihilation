using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TundraEngine
{
    public static class ResourceSystem
    {
        public const string ResourcePath = "Resources/";

        public static void LoadBinary(string file, out byte[] bytes)
        {
            bytes = File.ReadAllBytes(file);
        }
    }
}