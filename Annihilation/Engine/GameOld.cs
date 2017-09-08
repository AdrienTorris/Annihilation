//using System;
//using System.Collections.Generic;

//using Engine.Input;
//using Engine.Graphics;

//using SDL2;

//namespace Engine
//{
//    public enum GameState : byte
//    {
//        Running,
//        Quitting,
//    }

//    // TODO: Should be singleton?
//    public unsafe abstract class GameOld : IDisposable
//    {
//        public const int MaxPathLength = 256;

//        // TODO: Best place/best name for these?
//        public const string ConfigResolutionX = "ResolutionX";
//        public const string ConfigResolutionY = "ResolutionY";
//        public const string ConfigEnableVulkanValidation = "EnableVulkanValidation";

//        /// <summary>
//        /// The platform-specific path where you can write files. Perfect for save games.
//        /// </summary>
//        /// <remarks>
//        /// Must be called after <see cref="Run(Action, Action{double}, Action)"/>.
//        /// </remarks>
//        public static string PreferencePath { get; private set; }

//        public string[] Args { get; private set; }
//        public HashSet<string> ArgsSet { get; private set; }

//        // TODO: Support multiple windows per application
//        public Window Window { get; private set; }
//        public GraphicsManager GraphicsManager { get; private set; }

//        private GameState _state = GameState.Running;
//        private bool _isDisposed = false;

//        private Game() { }
        
//        public Game(string[] args)
//        {
//            if (args != null)
//            {
//                Args = args;
//                ArgsSet = new HashSet<string>(args.Length);
//                foreach (string arg in args)
//                {
//                    ArgsSet.Add(arg);
//                }
//            }
//            else
//            {
//                Args = new string[0];
//                ArgsSet = new HashSet<string>(0);
//            }
//        }

//        public void Run(Action initFunction, Action<double> updateFunction, Action shutdownFunction)
//        {
//            byte* title = Strings.GameTitle.ToUtf8();
//            byte* organization = Strings.Organization.ToUtf8();
//            byte* preferencePath = SDL.GetPrefPath(title, organization);
//            organization.Free();

//            PreferencePath = preferencePath.ToString();
//            SDL.Free(preferencePath.BytePtr);

//            Log.Info("Preference path found: " + PreferencePath);

//            Window = new Window(ref title);
//            title.Free();

//            InputSystem.Init(this);

//            initFunction?.Invoke();

//            while (_state == GameState.Running)
//            {
//                InputSystem.Update();

//                updateFunction?.Invoke(1f / 144);
//            }

//            shutdownFunction?.Invoke();
//        }

//        public void Quit()
//        {
//            _state = GameState.Quitting;
//        }

//        private void Dispose(bool disposing)
//        {
//            if (!_isDisposed)
//            {
//                if (disposing) { }

//                Window.Dispose();

//                _isDisposed = true;
//            }
//        }

//        ~Game()
//        {
//            Dispose(false);
//        }

//        public void Dispose()
//        {
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//    }
//}