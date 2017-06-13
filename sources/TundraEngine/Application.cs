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

        static void Main (string[] args)
        {
            SDL.SetHint (SDL.HintFrameBufferAcceleration, "1");

            if (SDL.Init (SDL.InitFlags.Video) != 0)
            {
                SDL.LogError (SDL.LogCategory.Error, SDL.GetError ());
                return;
            }
            
            IntPtr windowPtr = SDL.CreateWindow (
                "Tundra Engine",
                SDL.SDL_WINDOWPOS_UNDEFINED,
                SDL.SDL_WINDOWPOS_UNDEFINED,
                1280,
                768,
                SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            if (windowPtr == null)
            {
                SDL.LogError (SDL.LogCategory.Error, SDL.GetError ());
                return;
            }

            Console.WriteLine ("Tundra Engine Initialized");

            Event sdlEvent;
            bool quit = false;
            while (!quit)
            {
                while (SDL.PollEvent (out sdlEvent) == 1)
                {
                    if (sdlEvent.Type == EventType.Quit ||
                        sdlEvent.Type == EventType.KeyDown ||
                        sdlEvent.Type == EventType.MouseButtonDown)
                    {
                        quit = true;
                    }
                }
            }

            SDL.DestroyWindow (windowPtr);
            SDL.Quit ();
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