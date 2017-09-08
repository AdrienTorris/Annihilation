using System;
using System.Diagnostics;

namespace Engine
{
    public static class Log
    {
        static Log()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        [Conditional("DEBUG")]
        public static void Info(string text)
        {
            Trace.TraceInformation(text);
        }

        [Conditional("DEBUG")]
        public static unsafe void Info(Utf8 text)
        {
            Info(text.ToString());
        }

        [Conditional("DEBUG")]
        public static unsafe void Info(char* text)
        {
            Info(new string(text));
        }

        [Conditional("DEBUG")]
        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Trace.TraceWarning(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Warning(Utf8 text)
        {
            Warning(text.ToString());
        }

        [Conditional("DEBUG")]
        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.TraceError(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Error(Utf8 text)
        {
            Error(text.ToString());
        }

        [Conditional("DEBUG")]
        public static void Performance(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Trace.TraceWarning(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Performance(Utf8 text)
        {
            Performance(text.ToString());
        }
    }
}