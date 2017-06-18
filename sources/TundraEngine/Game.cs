﻿using TundraEngine.Graphics;
using System.Collections.Generic;

using static TundraEngine.SDL.SDL;

namespace TundraEngine
{
    /// <summary>
    /// The game manages all systems (Input, Graphics, etc.) and worlds.
    /// </summary>
    public abstract class Game
    {
        private static bool _quitRequested = false;
        
        /// <summary>
        /// The startup settings of the game.
        /// </summary>
        public static GameInfo GameInfo { get; private set; }
        
        /// <summary>
        /// The command-line arguments that were supplied to the game when it was executed, if any.
        /// </summary>
        public static string[] Args { get; private set; }

        /// <summary>
        /// Returns all the created game worlds.
        /// </summary>
        public static List<World> Worlds { get; private set; }

        /// <summary>
        /// The number of worlds.
        /// </summary>
        public static int NumWorlds { get { return Worlds.Count; } }

        private const int DefaultWorldCount = 4;

        public Game (GameInfo gameInfo, string[] args)
        {
            GameInfo = gameInfo;
            Args = args;
            Worlds = new List<World> (gameInfo.ApproximateWorldCound == 0 ? 
                                      DefaultWorldCount : 
                                      gameInfo.ApproximateWorldCound);
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public static void Quit ()
        {
            _quitRequested = true;
        } 

        /// <summary>
        /// Returns the world at the specified index.
        /// </summary>
        public static World GetWorld (int index)
        {
            Assert.IsTrue (index < Worlds.Count - 1, "Index overflow");
            return Worlds[index];
        }

        /// <summary>
        /// Creates and returns a new world with the specified flags.
        /// </summary>
        public static World CreateWorld (WorldFlags flags)
        {
            World world = new World ();
            Worlds.Add (world);
            return world;
        }

        /// <summary>
        /// Destroys the specified world.
        /// </summary>
        public static void DestroyWorld (World world)
        {
            bool result = Worlds.Remove (world);
            Assert.IsTrue (result, "World \"" + world + "\" was not created through the game.");
            // TODO: Release world memory
        }

        protected abstract void Initialize ();
        protected abstract void Simulate (double deltaTime);
        protected abstract void Shutdown ();
        
        internal void ProcessInput ()
        {
            SDL_Event sdlEvent;
            while (SDL_PollEvent (out sdlEvent) == 1)
            {
                // TODO: Here we should fill the high-level input manager
                if (sdlEvent.Type == SDL_EventType.Quit ||
                    sdlEvent.Type == SDL_EventType.KeyDown)
                {
                    Quit ();
                }
            }
        }

        public void Run ()
        {
            {
                bool result = SDL_SetHint (HintFrameBufferAcceleration, "1");
                Assert.IsTrue (result, "Unable to set hint \"" + HintFrameBufferAcceleration + "\"");
            }
            {
                int result = SDL_Init (SDL_InitFlags.Video | SDL_InitFlags.Timer);
                Assert.IsZero (result, "Unable to init SDL");
            }

            // Create window
            using (Window window = new Window (GameInfo.Name, GameInfo.WindowInfo))
            // Create graphics device
            using (GraphicsDevice graphicsDevice = new GraphicsDevice (GameInfo.Name, GameInfo.GraphicsInfo, window.WindowManagerInfo))
            {
                // Create timer
                Timer timer = new Timer ();

                // Initialize game
                Initialize ();

                while (!_quitRequested)
                {
                    // Update timer
                    timer.Update ();

                    // Process input
                    ProcessInput ();

                    // Simulate game
                    Simulate (timer.DeltaTime);

                    // Render
                }

                // Shutdown game
                Shutdown ();
            }

            SDL_Quit ();
        }
    }
}