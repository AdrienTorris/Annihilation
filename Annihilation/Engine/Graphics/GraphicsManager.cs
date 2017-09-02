using SDL2;
using Vulkan;

namespace Engine.Rendering
{
    public static class GraphicsManager
    {
        public static void Init()
        {

        }
        
        public static void Shutdown()
        {
            SDL.QuitSubSystem(SDL.InitFlags.Video);
        }
    }
}