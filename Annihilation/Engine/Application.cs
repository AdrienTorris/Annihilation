﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Engine.Input;
using Engine.Config;
using Engine.Graphics;

using SDL2;

namespace Engine
{
    public enum GameState : byte
    {
        Running,
        Quitting,
    }

    // TODO: Should be singleton?
    public unsafe class Application : IDisposable
    {
        public const int MaxPathLength = 256;

        // TODO: Best place/best name for these?
        public const string ConfigResolutionX = "ResolutionX";
        public const string ConfigResolutionY = "ResolutionY";
        public const string ConfigEnableVulkanValidation = "EnableVulkanValidation";

        /// <summary>
        /// The platform-specific path where you can write files. Perfect for save games.
        /// </summary>
        /// <remarks>
        /// Must be called after <see cref="Run(Action, Action{double}, Action)"/>.
        /// </remarks>
        public static string PreferencePath { get; private set; }
        
        public string[] Args { get; private set; }
        public HashSet<string> ArgsSet { get; private set; }

        public ApplicationSettings ApplicationSettings { get; private set; }
        public GraphicsSettings GraphicsSettings { get; private set; }
        public InputSettings InputSettings { get; private set; }

        // TODO: Support multiple windows per application
        public Window Window { get; private set; }
        public GraphicsManager GraphicsManager { get; private set; }

        private GameState _state = GameState.Running;
        private bool _isDisposed = false;
        
        private Application() { }

        public Application(string[] args,
                           ref ApplicationSettings applicationSettings,
                           ref GraphicsSettings graphicsSettings,
                           ref InputSettings inputSettings)
        {
            if (args != null)
            {
                Args = args;
                ArgsSet = new HashSet<string>(args.Length);
                foreach (string arg in args)
                {
                    ArgsSet.Add(arg);
                }
            }
            else
            {
                Args = new string[0];
                ArgsSet = new HashSet<string>(0);
            }

            applicationSettings.CheckError();

            ApplicationSettings = applicationSettings;
            GraphicsSettings = graphicsSettings;
            InputSettings = inputSettings;
        }

        public void Run(Action initFunction, Action<double> updateFunction, Action shutdownFunction)
        {
            Log.Info("Loading SDL functions.");
            SDL.LoadFunctions(SDLModule.SDL);
            SDL.LoadFunctions(SDLModule.Video);
            SDL.LoadFunctions(SDLModule.Events);
            SDL.LoadFunctions(SDLModule.Keyboard);
            SDL.LoadFunctions(SDLModule.Mouse);
            SDL.LoadFunctions(SDLModule.Version);
            SDL.LoadFunctions(SDLModule.SysWm);
            SDL.LoadFunctions(SDLModule.FileSystem);
            SDL.LoadFunctions(SDLModule.Vulkan);

            byte* title = ApplicationSettings.Title.ToBytes();
            byte* organization = ApplicationSettings.Organization.ToBytes();

            Log.Info("Getting preference path.");
            PreferencePath = PreferencePath ?? StringUtility.GetString(SDL.GetPrefPath(title, organization));
            Log.Info("Preference path found: " + PreferencePath);
            
            Log.Info("Creating window.");
            Window = new Window(title);

            Log.Info("Creating input manager.");
            InputManager.Init(this);
            
            Log.Info("Calling game init callback.");
            initFunction?.Invoke();
            
            while (_state == GameState.Running)
            {
                InputManager.Update();
                
                updateFunction?.Invoke(1f / 144);
            }
            
            Log.Info("Calling game shutdown callback.");
            shutdownFunction?.Invoke();
        }
        
        public void Quit()
        {
            _state = GameState.Quitting;
        }
        
        private void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing) { }

                Window.Dispose();

                _isDisposed = true;
            }
        }
        
        ~Application()
        {
            Dispose(false);
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}