using System;
using System.Diagnostics;

namespace Vulkan
{
    public static class ResultExtensions
    {
        [Conditional("DEBUG")]
        public static void CheckError(this Result result)
        {
            if (result < 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Trace.TraceError(result.ToString());
                Console.ResetColor();
            }
            else if (result > 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Trace.TraceWarning(result.ToString());
                Console.ResetColor();
            }
        }
    }
}