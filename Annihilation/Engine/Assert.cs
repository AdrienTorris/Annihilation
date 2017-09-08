using System.Diagnostics;

namespace Engine
{
    public static class Assert
    {
        [Conditional ("DEBUG")]
        public static void IsNotNull<T> (T value, string message = "")
        {
            Debug.Assert(value != null, message);
        }

        [Conditional ("DEBUG")]
        public static void IsNull<T> (T value, string message = "")
        {
            Debug.Assert(value == null, message);
        }

        [Conditional ("DEBUG")]
        public static void IsTrue (bool value, string message = "")
        {
            Debug.Assert(value, message);
        }

        [Conditional ("DEBUG")]
        public static void IsFalse (bool value, string message = "")
        {
            Debug.Assert(!value, message);
        }

        [Conditional ("DEBUG")]
        public static void IsZero (int value, string message = "")
        {
            Debug.Assert(value == 0, message);
        }

        [Conditional("DEBUG")]
        public static void IsNotZero(int value, string message = "")
        {
            Debug.Assert(value != 0, message);
        }
    }
}