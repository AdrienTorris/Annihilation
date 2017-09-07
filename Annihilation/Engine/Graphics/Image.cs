using System;
using Vulkan;

namespace Engine.Rendering
{
    public class Image
    {
        private Vk.VkImage _image;
        private Vk.VkImageView _imageView;
        private Vk.ImageUsageFlags _usage;
        
    }
}