using TundraEngine;
using TundraEngine.Input;
using TundraEngine.Graphics;

namespace Slayer
{
    class Application
    {
        static void Main (string[] args)
        {
            Game game = new Game(new GameInfo
            {
                Name = new String32("Tundra Breakout"),
                Version = new String8("0.1.0"),
                MaxResources = GameInfo.DefaultMaxResources,
                MaxEntitiesPerPrefab = GameInfo.DefaultMaxEntitiesPerPrefab,
                InitialPrefab = "MainMenu",
                WindowInfo = new WindowInfo
                {
                    PositionX = WindowInfo.DefaultPosition,
                    PositionY = WindowInfo.DefaultPosition,
                    Width = 1280,
                    Height = 720,
                    Mode = WindowMode.Windowed,
                    AllowHighDPI = true,
                    AlwaysOnTop = false
                },
                GraphicsInfo = new GraphicsInfo
                {
                    RenderScale = 1f,
                    SSAA = 8,
                    VSync = false,
                    PresentMode = PresentMode.Fifo,
                    EnableValidation = false
                },
                InputInfo = new InputInfo
                {
                    RepeatInterval = InputInfo.DefaultRepeatInterval,
                    RepeatDelay = InputInfo.DefaultRepeatDelay,
                    ActionMap = new ActionMap
                    {
                        Name = "Game",
                        ButtonBindings = new ButtonBinding[]
                        {
                            // Keyboard
                            new ButtonBinding(Button.LeftArrow, "MoveLeft"),
                            new ButtonBinding(Button.RightArrow, "MoveRight"),
                            new ButtonBinding(Button.UpArrow, "MoveUp"),
                            new ButtonBinding(Button.DownArrow, "MoveDown"),

                            // Gamepad
                            new ButtonBinding(Button.GamepadLeft, "MoveLeft"),
                            new ButtonBinding(Button.GamepadRight, "MoveRight"),
                            new ButtonBinding(Button.GamepadUp, "MoveUp"),
                            new ButtonBinding(Button.GamepadDown, "MoveDown"),
                        },
                        AxisBindings = new AxisBinding[]
                        {
                            new AxisBinding(Axis.MouseWheel, "Zoom")
                        }
                    }
                }
            }, args);

            game.Run ();
        }
    }
}