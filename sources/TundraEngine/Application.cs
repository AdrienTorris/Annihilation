using System;
using SDL2;

namespace TundraEngine
{
    public static class Application
    {
        static void Main (string[] args)
        {
            SDL.SetHint (SDL.HintFrameBufferAcceleration, "1");

            if (SDL.Init (SDL.InitFlags.Video) != 0)
            {
                SDL.LogError (SDL.LogCategory.Error, SDL.GetError ());
                return;
            }

            Window window = new Window ();

            Console.WriteLine ("Tundra Engine Initialized");

            Event sdlEvent;
            bool quit = false;
            while (!quit)
            {
                while (SDL.PollEvent (out sdlEvent) == 1)
                {
                    if (sdlEvent.Type == EventType.Quit ||
                        sdlEvent.Type == EventType.KeyDown)
                    {
                        quit = true;
                    }
                }
            }
            
            SDL.Quit ();
        }
    }
}