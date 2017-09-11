using Vulkan;

namespace Engine.Graphics
{
    public static class ResultExtensions
    {
        public static void CheckError(this VkResult result)
        {
            if (result < 0)
            {
                Log.Error(result.ToString());
            }
            else if (result > 0)
            {
                Log.Warning(result.ToString());
            }
        }
    }
}