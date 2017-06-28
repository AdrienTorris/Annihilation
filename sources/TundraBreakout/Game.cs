﻿using System.Numerics;

using TundraEngine;
using TundraEngine.Input;
using TundraEngine.Graphics;
using TundraEngine.Windowing;
using TundraEngine.Mathematics;
using TundraEngine.EntityComponent;

namespace Breakout
{
    public class Game : Application
    {
        public static class Context
        {
            public static readonly StringId32 MainMenu = "MainMenu";
            public static readonly StringId32 Game = "Game";
            public static readonly StringId32 GameMenu = "GameMenu";
        }

        public static class Action
        {
            public static readonly StringId32 Move = "Move";
            public static readonly StringId32 MoveLeft = "MoveLeft";
            public static readonly StringId32 MoveRight = "MoveRight";
            public static readonly StringId32 Power = "Power";

            public static readonly StringId32 Accept = "Accept";
            public static readonly StringId32 Cancel = "Cancel";
        }
        
        static void Main(string[] args)
        {
            Game game = new Game();
            game.Run(args);
        }

        protected override void Initialize()
        {
            // Pad transform
            Transform2DComponent padTransform = new Transform2DComponent()
            {
                Position = Vector2.Zero,
                Rotation = new Angle(),
                Scale = Vector2.One
            };
            padTransform.GetBytes(out byte[] transformBytes);

            // Pad entity
            EntityResource padResource = new EntityResource()
            {
                NumEntities = 1,
                NumComponentTypes = 3,
                Components = new ComponentTypeData[]
                {
                    new ComponentTypeData()
                    {
                        Type = Transform2DComponent.Type,
                        Size = Transform2DComponent.Size,
                        NumInstances = 1,
                        EntityIndices = new uint[] { 0 },
                        Data = transformBytes
                    }
                }
            };

            // Spawn Pad
            EntityManager.Spawn(null, padResource);
        }

        protected override void UpdateAsync(double deltaTime)
        {
        }

        protected override void Shutdown()
        {
        }

        protected override void GetApplicationInfo(out ApplicationInfo applicationInfo)
        {
            applicationInfo = new ApplicationInfo
            {
                Name = "Tundra Breakout",
                Version = "0.1.0",

                ResourcePath = ApplicationInfo.DefaultResourcePath,
                MaxResources = ApplicationInfo.DefaultMaxResources,
                InitialPrefab = "MainMenu",
                MaxEntitiesPerPrefab = ApplicationInfo.DefaultMaxEntitiesPerPrefab,

                WindowInfo = new WindowInfo
                {
                    PositionX = 10,
                    PositionY = 10,
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
                                new ButtonBinding(Button.A, Action.MoveLeft),
                                new ButtonBinding(Button.D, Action.MoveRight),
                                new ButtonBinding(Button.Space, Action.Power),
                                // Gamepad
                                new ButtonBinding(Button.GamepadA, Action.Power)
                            },
                            AxisBindings = new AxisBinding[]
                            {
                                // Gamepad
                                new AxisBinding(Axis.LeftThumb, Action.Move)
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
        }
    }
}