using System;
using System.Collections.Generic;
using SDL2;

namespace TundraEngine
{
    public static class Application
    {
        public static readonly World MainWorld = new World();

        private static List<World> _worlds = new List<World>(DefaultWorldCapacity);

        private const int DefaultWorldCapacity = 8;
        
        static unsafe void Main(string[] args)
        {
            Console.WriteLine("Tundra Engine");

            if (SDL.SDL_Init (SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine("Unable to initialize SDL: ", SDL.SDL_GetError());
            }
        }

        public static World GetWorld<T>()
        {
            return new World();
        }

        public static void Quit()
        {

        }
    }
}