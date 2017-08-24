using Vulkan;

namespace Engine.Rendering
{
    public static class ResultExtensions
    {
        public static void CheckError(this Result result)
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