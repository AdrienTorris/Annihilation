using System;
using Engine.Config;
using Engine.Graphics;
using Engine.Input;
using Engine.Profiling;
using SDL2;

namespace Engine
{
    public abstract unsafe class Game : IDisposable
    {
        protected SDL.Window _window;

        public abstract string Title { get; }
        public abstract string Organization { get; }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected abstract void Startup();
        protected abstract void Dispose(bool disposing);

        protected abstract void Update(float deltaTime);

        protected abstract void RenderScene();
        protected abstract void RenderUI(GraphicsContext graphicsContext);
        
        public virtual bool IsDone()
        {
            return InputSystem.WasPressed(Button.Escape);
        }
        
        ~Game()
        {
            Dispose(false);
        }
        
        public static void Run<T>(T game) where T : Game
        {
            using (Text title = new Text(game.Title))
            {
                CreateWindow(title, game);

                Initialize(title, game);
            }
            
            SDL.Event evt;
            do
            {
                if (SDL.PollEvent(out evt) == 1)
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
            } while (Update(game, evt));

            terminate:
            Terminate(game);
        }

        private static void CreateWindow<T>(Text title, T game) where T : Game
        {
            SDL.InitSubSystem(SDL.InitFlags.Video);

            SDL.VulkanLoadLibrary(null).CheckError();

            var window = SDL.CreateWindow(
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

        private static void Initialize<T>(Text title, T game) where T : Game
        {
            GraphicsSystem.Initialize(title, ref game._window);
            TimeSystem.Initialize();
            InputSystem.Initialize();
            ConfigSystem.Initialize();

            game.Startup();

            SDL.ShowWindow(game._window);
        }

        private static void Terminate<T>(T game) where T : Game
        {
            game.Dispose();

            InputSystem.Shutdown();
            GraphicsSystem.Shutdown();
        }

        private static bool Update<T>(T game, SDL.Event evt) where T : Game
        {
            TimeSystem.Update();
            ProfilingSystem.Update();

            float deltaTime = TimeSystem.DeltaTime;

            InputSystem.Update(deltaTime, evt);
            ConfigSystem.HandleInput(deltaTime);

            game.Update(deltaTime);
            game.RenderScene();

            PostEffectSystem.Render();
            
            var uiContext = new GraphicsContext();
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