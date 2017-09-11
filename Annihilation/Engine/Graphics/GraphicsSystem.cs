using System.Numerics;
using System.Collections.Generic;
using Engine.Config;
using Vulkan;

namespace Engine.Graphics
{
    public static class GraphicsSystem
    {
        public static int DisplayWidth = 1280;
        public static int DisplayHeight = 720;

        private static float _frameTime = 0f;
        private static ulong _frameIndex = 0;
        private static long _frameStartTick = 0;
        
        public static VkDevice Device;
        public static uint GraphicsQueueFamily;
        public static uint ComputeQueueFamily;
        public static uint TransferQueueFamily;
        public static VkQueue GraphicsQueue;
        public static VkQueue ComputeQueue;
        public static VkQueue TransferQueue;
        public static VkCommandBuffer CommandBuffer;
        public static VkPhysicalDeviceProperties DeviceProperties;
        public static VkPhysicalDeviceMemoryProperties DeviceMemoryProperties;
        public static VkFormat ColorFormat;
        public static VkFormat DepthFormat;
        public static VkSampleCountFlags SampleCount;
        public static bool Supersampling;
        public static bool DedicatedAllocation;

        public static void Initialize()
        {

        }

        public static float GetFrameTime()
        {
            return 1 / 144f;
        }

        public static void Present()
        {

        }

        public static void Resize(int width, int height)
        {

        }

        public static void Terminate()
        {

        }

        public static void Shutdown()
        {

        }
    }
}