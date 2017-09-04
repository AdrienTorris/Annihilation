using System.Numerics;
using System.Collections.Generic;
using Vulkan;

namespace Engine.Graphics
{
    public static class Graphics
    {
        public static readonly Dictionary<uint, string> VendorNames = new Dictionary<uint, string>
        {
            { 0x1002, "AMD" },
            { 0x10DE, "NVIDIA" },
            { 0x8086, "INTEL" },
            { 0x13B5, "ARM" },
            { 0x5143, "Qualcomm" },
            { 0x1010, "ImgTec" }
        };

        public static Vk.Device Device;
        public static uint GraphicsQueueFamily;
        public static uint ComputeQueueFamily;
        public static uint TransferQueueFamily;
        public static Vk.Queue GraphicsQueue;
        public static Vk.Queue ComputeQueue;
        public static Vk.Queue TransferQueue;
        public static Vk.CommandBuffer CommandBuffer;
        public static Vk.PhysicalDeviceProperties DeviceProperties;
        public static Vk.PhysicalDeviceMemoryProperties DeviceMemoryProperties;
        public static Vk.Format ColorFormat;
        public static Vk.Format DepthFormat;
        public static Vk.SampleCountFlags SampleCount;
        public static bool Supersampling;
        public static bool DedicatedAllocation;
    }
}