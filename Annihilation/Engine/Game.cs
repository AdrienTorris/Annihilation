using System;
using Engine.Input;
using Engine.Config;
using Engine.Graphics;
using Engine.Profiling;
using SDL2;

namespace Engine
{
    public abstract unsafe class Game : IDisposable
    {
        protected SDL.Window _window;

        public abstract string Title { get; }
        public abstract string Organization { get; }

        protected abstract void Startup();
        protected abstract void Dispose(bool disposing);

        protected abstract void Update(float deltaTime);

        protected abstract void RenderScene();
        protected abstract void RenderUI(GraphicsContext graphicsContext);

        /// <summary>
        /// Returns true when the game should exit. By default, returns true when <see cref="SDL2.SDL.KeyCode.Escape"/> is pressed.
        /// </summary>
        public virtual bool IsDone()
        {
            return InputSystem.WasPressed(Button.Escape);
        }
        
        //
        // Disposable pattern
        //
        ~Game()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        //
        // Static methods
        //
        public static void Run<T>(T game) where T : Game
        {
            byte* title = Utf8.AllocateFromString(game.Title);

            CreateWindow(ref title, game);
            
            Initialize(ref title, game);

            Utf8.Free(title);

            SDL.ShowWindow(game._window);

            // Poll and handle OS events, then update systems and game
            SDL.Event evt;
            do
            {
                if (SDL.PollEvent(out evt) == 1)
                {
                    switch (evt.Type)
                    {
                        case SDL.EventType.Quit:
                        {
                            goto terminate;
                        }
                        case SDL.EventType.WindowEvent:
                        {
                            HandleWindowEvent(evt.WindowEvent);
                            break;
                        }
                    }
                }
            }
            while (Update(game, evt));

            terminate:
            Terminate(game);
        }

        private static void CreateWindow<T>(ref byte* title, T game) where T : Game
        {
            SDL.InitSubSystem(SDL.InitFlags.Video);
            
            SDL.VulkanLoadLibrary(null).CheckError();

            SDL.Window window = SDL.CreateWindow(
                title,
                SDL.WindowPositionCentered,
                SDL.WindowPositionCentered,
                GraphicsSystem.DisplayWidth,
                GraphicsSystem.DisplayHeight,
                SDL.WindowFlags.Hidden | SDL.WindowFlags.Vulkan
            );
            window.CheckError();

            game._window = window;
        }

        private static void Initialize<T>(ref byte* title, T game) where T : Game
        {
            GraphicsSystem.Initialize(ref title, ref game._window);
            TimeSystem.Initialize();
            InputSystem.Initialize();
            ConfigSystem.Initialize();

            game.Startup();
        }

        private static void Terminate<T>(T game) where T : Game
        {
            GraphicsSystem.Terminate();

            game.Dispose();

            InputSystem.Shutdown();
            GraphicsSystem.Shutdown();
        }

        private static bool Update<T>(T game, SDL.Event evt) where T : Game
        {
            ProfilingSystem.Update();

            float deltaTime = GraphicsSystem.GetFrameTime();

            InputSystem.Update(deltaTime, evt);
            ConfigSystem.HandleInput(deltaTime);

            game.Update(deltaTime);
            game.RenderScene();

            PostEffectSystem.Render();

            // TODO: Setup ui context
            GraphicsContext uiContext = new GraphicsContext();
            game.RenderUI(uiContext);

            uiContext.Finish();

            GraphicsSystem.Present();

            return !game.IsDone();
        }

        private static void HandleWindowEvent(SDL.WindowEvent windowEvent)
        {
            switch (windowEvent.Event)
            {
                case SDL.WindowEventID.SizeChanged:
                {
                    GraphicsSystem.Resize(windowEvent.Data1, windowEvent.Data2);
                    break;
                }
            }
        }
    }
}