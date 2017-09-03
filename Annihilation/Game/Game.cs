using System;
using System.Numerics;
using System.Threading.Tasks;

using Engine;
using Engine.Config;

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

    class Game
    {
        static void Main(string[] args)
        {
            ApplicationSettings applicationSettings = default(ApplicationSettings);
            applicationSettings.Title = "Annihilation";
            applicationSettings.Organization = "Illogika";
            applicationSettings.Version = "0.1.0";

            GraphicsSettings graphicsSettings = new GraphicsSettings();

            InputSettings inputSettings = default(InputSettings);
            inputSettings.RepeatInterval = InputSettings.DefaultRepeatInterval;
            inputSettings.RepeatDelay = InputSettings.DefaultRepeatDelay;
            
            Application application = new Application(ref applicationSettings, ref graphicsSettings, ref inputSettings);

            application.Start(Init, null, null);

            // Do not put any code here
        }

        private static void Init()
        {
            string configPath = Application.PreferencePath + "settings.init";

            ConfigManager.AddVarsFromFile(configPath);
        }
    }
}