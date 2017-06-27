using System.Runtime.InteropServices;

using TundraEngine.Windowing;
using TundraEngine.Graphics;
using TundraEngine.Input;

namespace TundraEngine
{
    /// <summary>
    /// Main configuration data for the engine.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct ApplicationInfo
    {
        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name;
        /// <summary>
        /// The version of the application.
        /// </summary>
        public string Version;

        /// <summary>
        /// The prefab to load on initialization.
        /// </summary>
        public StringId64 InitialPrefab;

        /// <summary>
        /// Path to binary application resources.
        /// </summary>
        public string ResourcePath;
        /// <summary>
        /// Maximum number of resources that can be loaded at one time.
        /// </summary>
        public int MaxResources;

        /// <summary>
        /// Maximum number of entities in a prefab hierarchy.
        /// </summary>
        public int MaxEntitiesPerPrefab;

        /// <summary>
        /// Window settings.
        /// </summary>
        public WindowInfo WindowInfo;
        /// <summary>
        /// Renderer settings.
        /// </summary>
        public GraphicsInfo GraphicsInfo;
        /// <summary>
        /// Input settings and default action maps.
        /// </summary>
        public InputInfo InputInfo;

        public const string DefaultResourcePath = "/Resources/";
        public const int DefaultMaxResources = 1024;
        public const int DefaultMaxEntitiesPerPrefab = 1024;
    }
}