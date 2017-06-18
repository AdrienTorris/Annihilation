using SharpVk;

namespace TundraEngine.Graphics
{
    public static class QueueFlagsExtensions
    {
        public static bool Has (this QueueFlags variable, QueueFlags flag)
        {
            return (variable & flag) != 0;
        }

        public static bool HasNot (this QueueFlags variable, QueueFlags flag)
        {
            return (variable & flag) == 0;
        }
    }
}