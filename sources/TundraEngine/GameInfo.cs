using System.Runtime.InteropServices;
using TundraEngine.Graphics;

namespace TundraEngine
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GameInfo
    {
        public String32 Name;
        public String8 Version;
        public StringId64 MainPrefab;
        public int MaxResources;
        public int MaxEntitiesPerPrefab;
        public WindowInfo WindowInfo;
        public GraphicsInfo GraphicsInfo;
        public InputInfo InputInfo;

        public const int DefaultMaxResources = 1024;
        public const int DefaultMaxEntitiesPerPrefab = 1024;
    }
}