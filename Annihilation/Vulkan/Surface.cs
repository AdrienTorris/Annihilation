using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace Annihilation.Vk
{
    public unsafe struct Surface
    {
        public SurfaceHandle Handle { get; private set; }
        public Instance Instance { get; private set; }

        public Surface(SurfaceHandle handle, Instance instance)
        {
            Handle = handle;
            Instance = instance;
        }
    }
}