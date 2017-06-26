using TundraEngine;
using TundraEngine.Input;
using TundraEngine.Graphics;

namespace Slayer
{
    class Program
    {
        static void Main (string[] args)
        {
            SlayerGame game = new SlayerGame(new GameInfo
            {
                Name = new String32("Tundra Breakout"),
                Version = new String8("0.1.0"),
                MaxResources = GameInfo.DefaultMaxResources,
                MaxEntitiesPerPrefab = GameInfo.DefaultMaxEntitiesPerPrefab,
                MainPrefab = "MainMenu",
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
                    ResolutionX = 1280,
                    ResolutionY = 720,
                    SSAA = 8,
                    VSync = false
                },
                InputInfo = new InputInfo
                {
                    RepeatInterval = InputInfo.DefaultRepeatInterval,
                    RepeatDelay = InputInfo.DefaultRepeatDelay,
                    Bindings = new InputBindings
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
                            new ButtonBinding(Button.GamepadRight, "MoveLeft"),
                            new ButtonBinding(Button.GamepadUp, "MoveLeft"),
                            new ButtonBinding(Button.GamepadDown, "MoveLeft"),
                        }
                    }
                }
            }, args);

            game.Run ();
        }
    }
}