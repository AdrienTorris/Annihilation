namespace Vulkan
{
    public static class Constants
    {
        public const float LodClampNone = 1000f;
        public const uint RemainingMipLevels = ~0U;
        public const uint RemainingArrayLayers = ~0U;
        public const ulong WholeSize = ~0UL;
        public const uint AttachmentUnused = ~0U;
        public const uint QueueFamilyIgnored = ~0U;
        public const uint SubpassExternal = ~0U;
        public const uint MaxPhysicalDeviceNameSize = 256;
        public const uint UUIDSize = 16;
        public const uint MaxMemoryTypes = 32;
        public const uint MaxMemoryHeaps = 16;
        public const uint MaxExtensionNameSize = 256;
        public const uint MaxDescriptionSize = 256;
        public const int LUIDSize = 8;
        public const string DebugReportExtensionName = "VK_EXT_debug_report";
        public const int MaxDeviceGroupSize = 32;
        public const string DeviceGroupExtensionName = "VK_KHX_device_group";
        public const string DeviceGeneratedCommandsExtensionName = "VK_NVX_device_generated_commands";
    }
}