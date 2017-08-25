using System;
using System.Numerics;
using System.Threading.Tasks;

using Engine;
using Engine.Input;
using Engine.Rendering;

namespace Game
{
    class Program
    {
        public static class Context
        {
            public static readonly StringHash32 MainMenu = "MainMenu";

            public static readonly StringHash32 Game = "Game";
            public static readonly StringHash32 GameMenu = "GameMenu";
        }

        public static class Action
        {
            public static readonly StringHash32 MoveHorizontal = "MoveHorizontal";
            public static readonly StringHash32 MoveVertical = "MoveVertical";
            public static readonly StringHash32 MoveUp = "MoveUp";
            public static readonly StringHash32 MoveDown = "MoveDown";
            public static readonly StringHash32 MoveLeft = "MoveLeft";
            public static readonly StringHash32 MoveRight = "MoveRight";
            public static readonly StringHash32 PlaceBomb = "PlaceBomb";

            public static readonly StringHash32 Accept = "Accept";
            public static readonly StringHash32 Cancel = "Cancel";
        }

        static void Main(string[] args)
        {
            GameSettings settings = new GameSettings
            {
                Name = "Bomberman",

                CommandLineArgs = args,

                InitialContext = Context.MainMenu,

                ResourcePath = GameSettings.DefaultResourcePath,
                MaxResources = GameSettings.DefaultMaxResources,
                MaxEntitiesPerPrefab = GameSettings.DefaultMaxEntitiesPerPrefab,

                WindowSettings = new WindowSettings
                {
                    PositionX = 50,
                    PositionY = 50,
                    Width = 1280,
                    Height = 720,
                    Mode = WindowMode.Windowed,
                    AllowHighDPI = false,
                    AlwaysOnTop = false
                },
                RendererSettings = new RendererSettings
                {
                    Width = 1280,
                    Height = 720,
                },
                InputSettings = new InputSettings
                {
                    RepeatInterval = InputSettings.DefaultRepeatInterval,
                    RepeatDelay = InputSettings.DefaultRepeatDelay,
                    ActionMaps = new ActionMap[]
                    {
                        new ActionMap()
                        {
                            Context = Context.Game,
                            ButtonBindings = new ButtonBinding[]
                            {
                                // Keyboard
                                new ButtonBinding(Button.LeftArrow, Action.MoveLeft),
                                new ButtonBinding(Button.RightArrow, Action.MoveRight),
                                new ButtonBinding(Button.W, Action.MoveUp),
                                new ButtonBinding(Button.S, Action.MoveDown),
                                new ButtonBinding(Button.A, Action.MoveLeft),
                                new ButtonBinding(Button.D, Action.MoveRight),
                                new ButtonBinding(Button.Space, Action.PlaceBomb),
                                // Gamepad
                                new ButtonBinding(Button.GamepadA, Action.PlaceBomb)
                            },
                            AxisBindings = new AxisBinding[]
                            {
                                // Gamepad
                                new AxisBinding(Axis.GamepadLeftStickX, Action.MoveHorizontal),
                                new AxisBinding(Axis.GamepadLeftStickY, Action.MoveVertical)
                            }
                        },
                        new ActionMap()
                        {
                            Context = Context.MainMenu,
                            ButtonBindings = new ButtonBinding[]
                            {
                                // Keyboard
                                new ButtonBinding(Button.Return, Action.Accept),
                                new ButtonBinding(Button.Escape, Action.Cancel),
                                // Gamepad
                                new ButtonBinding(Button.GamepadA, Action.Accept),
                                new ButtonBinding(Button.GamepadB, Action.Cancel)
                            }
                        }
                    }
                }
            };
            
            Game game = new Game(settings, null, null, null);
        }
    }
}