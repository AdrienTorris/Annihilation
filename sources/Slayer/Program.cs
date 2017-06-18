using TundraEngine;

namespace Slayer
{
    class Program
    {
        static void Main (string[] args)
        {
            var gameInfo = new GameInfo
            {
                Name = "Slayer",
                WindowInfo = WindowInfo.Default
            };
            var game = new SlayerGame ();

            game.Run (gameInfo);
        }
    }
}