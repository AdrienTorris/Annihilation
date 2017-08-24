using System;

namespace Engine
{
    public static class Log
    {
        public static void Info(string text)
        {
            System.Diagnostics.Debug.WriteLine(text, "[Info]");
        }

        public static void Debug(string text)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            System.Diagnostics.Debug.WriteLine(text, "[Debug]");
            Console.ResetColor();
        }

        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Diagnostics.Debug.WriteLine(text, "[Warning]");
            Console.ResetColor();
        }

        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Diagnostics.Debug.WriteLine(text, "[Error]");
            Console.ResetColor();
        }

        public static void Performance(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            System.Diagnostics.Debug.WriteLine(text, "[Performance]");
            Console.ResetColor();
        }
    }
}