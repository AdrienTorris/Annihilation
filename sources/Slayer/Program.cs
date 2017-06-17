namespace Slayer
{
    class Program
    {
        static void Main (string[] args)
        {
            using (var game = new SlayerGame ())
            {
                game.Run ();
            }
        }
    }
}