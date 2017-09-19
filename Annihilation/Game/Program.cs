using Engine;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Annihilation game = new Annihilation())
            {
                Game.Run(game);
            }
        }
    }
}