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
        public static unsafe void Info(byte* text)
        {
            Info(text);
        }

        [Conditional("DEBUG")]
        public static void Warning(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Trace.TraceWarning(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Warning(byte* text)
        {
            Warning(text);
        }

        [Conditional("DEBUG")]
        public static void Error(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Trace.TraceError(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Error(byte* text)
        {
            Error(text);
        }

        [Conditional("DEBUG")]
        public static void Performance(string text)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Trace.TraceWarning(text);
            Console.ResetColor();
        }

        [Conditional("DEBUG")]
        public static unsafe void Performance(byte* text)
        {
            Performance(text);
        }
    }
}