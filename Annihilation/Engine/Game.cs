using System;
using Engine.Input;
using Engine.Graphics;
using Engine.Profiling;
using SDL2;

namespace Engine
{
    public abstract unsafe class Game : IDisposable
    {
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
            return InputSystem.IsKeyDown(SDL2.SDL.KeyCode.Escape);
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
            // Create window
            byte* title = Utf8.AllocateFromString(game.Title);
            Window window = new Window(title);
            
            // Initialize engine systems and game
            Initialize(game);

            // Show window
            window.Show();

            // Poll and handle OS events, then update systems and game
            do
            {
                if (SDL.PollEvent(out SDL.Event ev) == 1)
                {
                    switch (ev.Type)
                    {
                        case SDL.EventType.Quit:
                        {
                            goto terminate;
                        }
                        case SDL.EventType.WindowEvent:
                        {
                            HandleWindowEvent(ev.Window);
                            break;
                        }
                    }
                }
            }
            while (Update(game));

            terminate:
            Terminate(game);
        }

        private static void Initialize<T>(T game) where T : Game
        {
            GraphicsSystem.Initialize();
            TimeSystem.Initialize();
            InputSystem.Initialize();
            VariableSystem.Initialize();

            game.Startup();
        }

        private static void Terminate<T>(T game) where T : Game
        {
            GraphicsSystem.Terminate();

            game.Dispose();

            InputSystem.Shutdown();
            GraphicsSystem.Shutdown();
        }

        private static bool Update<T>(T game) where T : Game
        {
            ProfilingSystem.Update();

            float deltaTime = GraphicsSystem.GetFrameTime();

            InputSystem.Update(deltaTime);
            VariableSystem.Update(deltaTime);

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