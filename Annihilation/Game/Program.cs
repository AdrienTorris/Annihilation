using System;
using System.Numerics;
using System.Threading.Tasks;

using Engine;
using Engine.IO;

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
        Accept,
        Cancel,
    }

    class Program
    {
        static void Main(string[] args)
        {
            InputSettings inputSettings = default(InputSettings);
            inputSettings.RepeatInterval = InputSettings.DefaultRepeatInterval;
            inputSettings.RepeatDelay = InputSettings.DefaultRepeatDelay;

            Settings settings = default(Settings);
            settings.Title = "Annihilation";
            settings.Organization = "Illogika";
            settings.Version = "0.1.0";

            Game.Start(settings, Init, null, null);
        }

        private static void Init()
        {
            GraphicsOptions graphicsOptions = new GraphicsOptions();
            ConfigFile.Write(graphicsOptions, Game.PreferencePath + "settings.init");
            Log.Info("File written at " + Game.PreferencePath + "settings.init");
        }
    }
}