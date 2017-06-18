using System;
using System.Diagnostics;

namespace TundraEngine
{
    public static class Assert
    {
        [Conditional ("DEBUG")]
        public static void IsNotNull<T> (T value, string message)
        {
            if (value == null) throw new Exception (message);
        }

        [Conditional ("DEBUG")]
        public static void IsNull<T> (T value, string message)
        {
            if (value != null) throw new Exception (message);
        }

        [Conditional ("DEBUG")]
        public static void IsTrue (bool value, string message)
        {
            if (!value) throw new Exception (message);
        }

        [Conditional ("DEBUG")]
        public static void IsFalse (bool value, string message)
        {
            if (value) throw new Exception (message);
        }

        [Conditional ("DEBUG")]
        public static void IsZero (int value, string message)
        {
            if (value != 0) throw new Exception (message);
        }
    }
}