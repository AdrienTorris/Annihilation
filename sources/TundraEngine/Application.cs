using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
            SDL.Init (SDL.InitFlags.Video);

            SDL.SetClipboardText ("MDNIDFN dfijdnsf in sf nds iufjndsun");

            string str = SDL.GetClipboardText ();
            Console.WriteLine (str);
            Console.ReadKey ();
            
            SDL.Quit ();

            //if (SDL.Init (SDL.InitFlags.Video) != 0)
            //{
            //    Console.WriteLine ("Unable to initialize SDL: ", SDL.SDL_GetError ());
            //    Console.ReadKey ();
            //}

            //IntPtr windowPtr = SDL.SDL_CreateWindow (
            //    "Tundra Engine",
            //    SDL.SDL_WINDOWPOS_UNDEFINED,
            //    SDL.SDL_WINDOWPOS_UNDEFINED,
            //    1280,
            //    768,
            //    SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN);

            //if (windowPtr == null)
            //{
            //    Console.WriteLine ("Unable to create window: ", SDL.SDL_GetError ());
            //    Console.ReadKey ();
            //}

            //Console.WriteLine ("Tundra Engine Initialized");

            //Event sdlEvent;
            //bool quit = false;
            //while (!quit)
            //{
            //    while (SDL.PollEvent (out sdlEvent) == 1)
            //    {
            //        if (sdlEvent.Type == EventType.Quit ||
            //            sdlEvent.Type == EventType.KeyDown ||
            //            sdlEvent.Type == EventType.MouseButtonDown)
            //        {
            //            quit = true;
            //        }
            //    }
            //}
            
            //SDL.SDL_DestroyWindow (windowPtr);
            //SDL.Quit ();
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