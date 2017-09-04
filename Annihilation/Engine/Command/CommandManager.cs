using System;
using System.Collections.Generic;
using System.Text;

namespace Engine
{
    public static unsafe class CommandManager
    {
        public const int MaxArgs = 80;

        public static int ArgCount { get; private set; }

        private static char*[] _args = new char*[MaxArgs];
        private static Command[] _commands;

        static CommandManager()
        {
            // Add the engine's default command-related commands

        }

        public static string GetArg(int index)
        {
            if (index < 0 || index >= ArgCount)
            {
                return "";
            }
            return new string(_args[index]);
        }

        public static void AddCommand(string name, CommandFunction function)
        {

        }
    }
}