using System.Numerics;

using Vulkan;

namespace Engine.Rendering
{
    public struct Vertex
    {
        public Vector2 Position;
        public Vector4 Color;

        public static readonly Vk.VertexInputBindingDescription BindingDescription = new Vk.VertexInputBindingDescription
        {
            Binding = 0,
            Stride = sizeof(float) * 2 * 3,
            InputRate = Vk.VertexInputRate.Vertex
        };

        public static readonly Vk.VertexInputAttributeDescription[] AttributeDescriptions = new Vk.VertexInputAttributeDescription[]
        {
            new Vk.VertexInputAttributeDescription
            {
                Binding = 0,
                Location = 0,
                Format = Vk.Format.R32G32SFloat,
                Offset = 0
            },
            new Vk.VertexInputAttributeDescription
            {
                Binding = 0,
                Location = 1,
                Format = Vk.Format.R32G32B32SFloat,
                Offset = sizeof(float) * 2
            }
        };
    }
}