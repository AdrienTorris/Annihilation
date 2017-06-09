using System;
using System.Collections.Generic;

namespace TundraEngine
{
    public static class Application
    {
        private static List<World> _worlds = new List<World>(DefaultWorldCapacity);

        private const int DefaultWorldCapacity = 8;
        
        static void Main(string[] args)
        {
            Console.WriteLine("Tundra Engine");
            Console.ReadKey();
        }

        public static World GetWorld<T>()
        {
            return new World();
        }

        public static void Quit()
        {

        }
    }
}