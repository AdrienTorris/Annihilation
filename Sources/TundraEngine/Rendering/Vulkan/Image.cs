using System;
using System.Collections.Generic;
using System.Text;

namespace TundraEngine.Rendering.Vulkan
{
    public unsafe struct ImageCreateInfo
    {
        public StructureType Type;
        public void* Next;

    }

    public struct Image
    {
        internal ulong NativeHandle;
    }
}
