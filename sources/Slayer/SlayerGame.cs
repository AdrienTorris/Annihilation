using System;
using TundraEngine;

namespace Slayer
{
    class SlayerGame : Game
    {
        protected override void Initialize ()
        {

        }

        protected override void Shutdown ()
        {

        }

        protected override void Simulate (double deltaTime)
        {
            int x = 0;
            for (int i = 0; i < 1000000; ++i)
            {
                x = 2;
            }

            Console.WriteLine (deltaTime);
        }
    }
}