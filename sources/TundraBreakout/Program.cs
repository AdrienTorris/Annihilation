using TundraEngine;

namespace Slayer
{
    class Program
    {
        static void Main (string[] args)
        {
            var game = new SlayerGame (new GameInfo
            {
                Name = "Slayer",
                WindowInfo = WindowInfo.Default
            });

            game.Run ();
        }
    }
}