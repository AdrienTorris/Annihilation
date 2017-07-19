using System;

namespace TundraEngine.Rendering
{
    public class VulkanException : Exception
    {
        public Result Result { get; private set; }
        
        public VulkanException(Result result)
            : base($"A Vulkan error of type [{result}] occurred.")
        {
            Result = result;
        }
    }
}