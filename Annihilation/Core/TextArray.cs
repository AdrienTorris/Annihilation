using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace Annihilation.Core
{
    public unsafe struct TextArray
    {
        private Text* _texts;

        public TextArray(int capacity)
        {
            _texts = Marshal.AllocHGlobal()
        }
    }
}