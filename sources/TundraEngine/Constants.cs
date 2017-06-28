namespace TundraEngine
{
    public static class Constants
    {
        /// <summary>
        /// Maximum number of players.
        /// </summary>
        public const int MaxPlayerCount = 4;

        /// <summary>
        /// Number of bytes in one CPU cache line.
        /// <para/> Set to 64 bytes for AMD Jaguar (PS4 and Xbox One) and Core i7.
        /// </summary>
        public const int CacheLineSize = 64;
        /// <summary>
        /// Size in bytes of a pointer or reference.
        /// </summary>
        public const int PointerSize = 8;
        /// <summary>
        /// The target time in ms that each step (logic, command buffers, gpu) should take.
        /// </summary>
        public const float TargetFrameStepTime = 6.944f;
    }
}