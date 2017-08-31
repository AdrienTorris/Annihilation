using System;
using System.Numerics;
using System.Threading.Tasks;

using Engine;
using Engine.Input;
using Engine.Rendering;

namespace Annihilation
{
    [Flags]
    public enum GameContext : int
    {
        Opening = 1 << 0,
        MainMenu = 1 << 1,
        Game = 1 << 2,
        GameMenu = Game | 1 << 3,
        GameResults = 1 << 4
    }

    public enum GameAction
    {
        
    }

    class Program
    {
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
            InputSettings inputSettings = default(InputSettings);
            inputSettings.RepeatInterval = InputSettings.DefaultRepeatInterval;
            inputSettings.RepeatDelay = InputSettings.DefaultRepeatDelay;
            inputSettings.ActionMaps = new ActionMap[]
            {
                new ActionMap()
                {
                    Context = (int)GameContext.Game,
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
                    Context = (int)GameContext.MainMenu,
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
            };

            Settings settings = default(Settings);
            settings.Title = "Bomberman";
            settings.CommandLineArgs = args;
            settings.InputSettings = inputSettings;

            Game.Start(settings, null, null, null);
        }
    }
}