﻿using System;
using System.Numerics;
using System.Threading.Tasks;

using TundraEngine;
using TundraEngine.Input;
using TundraEngine.Rendering;
using TundraEngine.Windowing;
using TundraEngine.Mathematics;
using TundraEngine.EntityComponent;
using TundraEngine.IMGUI;

namespace Bomberman
{
    public static class Bomberman
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
            GameSettings settings = new GameSettings();
            settings.Name = "Bomberman";
            settings.Version = new TundraEngine.Version(0, 1, 0);
            settings.CommandLineArgs = args;

            settings.InitialPrefab = "MainMenu";

            settings.ResourcePath = GameSettings.DefaultResourcePath;
            settings.MaxResources = GameSettings.DefaultMaxResources;
            settings.MaxEntitiesPerPrefab = GameSettings.DefaultMaxEntitiesPerPrefab;
            
            settings.WindowSettings = new WindowSettings
            {
                Type = WindowType.SDL,
                PositionX = WindowSettings.DefaultPosition,
                PositionY = WindowSettings.DefaultPosition,
                Width = WindowSettings.DefaultSize,
                Height = WindowSettings.DefaultSize,
                Mode = WindowMode.FullscreenDesktop,
                AllowHighDPI = true,
                AlwaysOnTop = true
            };

            settings.RendererSettings = new RendererSettings
            {
                RendererType = RendererType.Vulkan,
                VulkanInfo = new VulkanInfo
                {
                    PresentMode = SharpVk.PresentMode.Mailbox
                },
                Width = RendererSettings.DefaultSize,
                Height = RendererSettings.DefaultSize,
                SSAA = 8,
                VSync = false,
            };

            settings.DebugUISettings = new DebugUISettings
            {
                DebugUIType = DebugUIType.BGFX
            };

            settings.InputSettings = new InputSettings
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
            };

            Game game = new Game(settings, Initialize, null, null);
        }

        private static void Initialize()
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
            Prefab padResource = new Prefab()
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

        /*protected override async Task UpdateAsync(double deltaTime)
        {
            DebugUI.Clear();
            DebugUI.Text(DateTime.Now.Second, 0, "Allo?");

            await Task.Delay(TimeSpan.FromMilliseconds(Constants.TargetFrameStepTime));
        }*/
    }
}