using System;
using System.Runtime.InteropServices;

using Engine.Windowing;
using Engine.Rendering;
using Engine.Input;
using Engine.IMGUI;

namespace Engine
{
    public struct Version
    {
        public int Major;
        public int Minor;
        public int Patch;

        public Version(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }
    }
    
    /// <summary>
    /// Main configuration data for the engine.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct GameSettings
    {
        /// <summary>
        /// The name of the application.
        /// </summary>
        public string Name;
        /// <summary>
        /// The version of the application.
        /// </summary>
        public Version Version;
        /// <summary>
        /// Command line arguments passed to the program.
        /// </summary>
        public string[] CommandLineArgs;
        
        /// <summary>
        /// The prefab to load on initialization.
        /// </summary>
        public StringHash32 InitialContext;
        
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
        public WindowSettings WindowSettings;
        /// <summary>
        /// Renderer settings.
        /// </summary>
        public RendererSettings RendererSettings;
        /// <summary>
        /// Input settings and default action maps.
        /// </summary>
        public InputSettings InputSettings;
        public DebugUISettings DebugUISettings;

        public const string DefaultResourcePath = "/Resources/";
        public const int DefaultMaxResources = 1024;
        public const int DefaultMaxEntitiesPerPrefab = 1024;
    }
}