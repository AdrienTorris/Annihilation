using System;
using System.Diagnostics;

namespace TundraEngine
{
    public static class Assert
    {
        static Assert()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
        }

        [Conditional ("DEBUG")]
        public static void IsNotNull<T> (T value, string message)
        {
            Trace.Assert(value != null, message);
        }

        [Conditional ("DEBUG")]
        public static void IsNull<T> (T value, string message)
        {
            Trace.Assert(value == null, message);
        }

        [Conditional ("DEBUG")]
        public static void IsTrue (bool value, string message)
        {
            Trace.Assert(value, message);
        }

        [Conditional ("DEBUG")]
        public static void IsFalse (bool value, string message)
        {
            Trace.Assert(!value, message);
        }

        [Conditional ("DEBUG")]
        public static void IsZero (int value, string message)
        {
            Trace.Assert(value == 0, message);
        }

        [Conditional("DEBUG")]
        public static void IsNotZero(int value, string message)
        {
            Trace.Assert(value != 0, message);
        }
    }
}