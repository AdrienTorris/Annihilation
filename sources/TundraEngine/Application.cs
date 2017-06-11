using System;
using System.Collections.Generic;
using SDL2;

namespace TundraEngine
{
    public static class Application
    {
        public static readonly World MainWorld = new World ();

        private static List<World> _worlds = new List<World> (DefaultWorldCapacity);

        private const int DefaultWorldCapacity = 8;

        static unsafe void Main (string[] args)
        {
            if (SDL.SDL_Init (SDL.SDL_INIT_VIDEO) != 0)
            {
                Console.WriteLine ("Unable to initialize SDL: ", SDL.SDL_GetError ());
                Console.ReadKey ();
            }

            IntPtr windowPtr = SDL.SDL_CreateWindow (
                "Tundra Engine",
                SDL.SDL_WINDOWPOS_UNDEFINED,
                SDL.SDL_WINDOWPOS_UNDEFINED,
                1280,
                768,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (windowPtr == null)
            {
                Console.WriteLine ("Unable to create window: ", SDL.SDL_GetError ());
                Console.ReadKey ();
            }

            Console.WriteLine ("Tundra Engine Initialized");

            bool isRunning = true;
            while (isRunning)
            {
                SDL.SDL_Event sdlEvent;
            }
            

            SDL.SDL_DestroyWindow (windowPtr);
            SDL.SDL_Quit ();
        }

        public static World GetWorld<T> ()
        {
            return new World ();
        }

        public static void Quit ()
        {

        }
    }
}