using System;
using System.Numerics;
using System.Threading.Tasks;

using Engine;

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

            GraphicsSettings graphicsSettings = new GraphicsSettings();

            InputSettings inputSettings = default(InputSettings);
            inputSettings.RepeatInterval = InputSettings.DefaultRepeatInterval;
            inputSettings.RepeatDelay = InputSettings.DefaultRepeatDelay;

            using (Application application = new Application(args, ref applicationSettings, ref graphicsSettings, ref inputSettings))
            {
                application.Run(Init, Update, null);
            }
        }

        private unsafe static void Init()
        {
            
        }
        
        private static unsafe void Update(double dt)
        {

        }
    }
}