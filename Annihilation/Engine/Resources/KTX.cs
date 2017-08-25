using System;
using System.IO;
using System.Collections.Generic;
using Vulkan;
using Engine.Rendering;

namespace Engine
{
    public static class KTX
    {
        private static readonly byte[] KtxIdentifier =
        {
            0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A
        };

        private static readonly Dictionary<int, Vk.Format> GlInternalFormatToVkFormat = new Dictionary<int, Vk.Format>()
        {
            [0x8229] = Vk.Format.R8UNorm,
            [0x8F94] = Vk.Format.R8SNorm,
            [0x822A] = Vk.Format.R16UNorm,
            [0x8F98] = Vk.Format.R16SNorm,
            [0x822B] = Vk.Format.R8G8UNorm,
            [0x8F95] = Vk.Format.R8G8SNorm,
            [0x822C] = Vk.Format.R16G16UNorm,
            [0x8F99] = Vk.Format.R16G16SNorm,
            [0x8D62] = Vk.Format.R5G6B5UNormPack16,
            [0x8051] = Vk.Format.R8G8B8UNorm,
            [0x8F96] = Vk.Format.R8G8B8SNorm,
            [0x8054] = Vk.Format.R16G16B16UNorm,
            [0x8F9A] = Vk.Format.R16G16B16SNorm,
            [0x8056] = Vk.Format.R4G4B4A4UNormPack16,
            [0x8057] = Vk.Format.R5G5B5A1UNormPack16,
            [0x8058] = Vk.Format.R8G8B8A8UNorm,
            [0x8F97] = Vk.Format.R8G8B8A8SNorm,
            [0x805B] = Vk.Format.R16G16B16A16UNorm,
            [0x8F9B] = Vk.Format.R16G16B16A16SNorm,
            [0x8C41] = Vk.Format.R8G8B8SRGB,
            [0x8C43] = Vk.Format.R8G8B8A8SRGB,
            [0x822D] = Vk.Format.R16SFloat,
            [0x822F] = Vk.Format.R16G16SFloat,
            [0x881B] = Vk.Format.R16G16B16SFloat,
            [0x881A] = Vk.Format.R16G16B16A16SFloat,
            [0x822E] = Vk.Format.R32SFloat,
            [0x8230] = Vk.Format.R32G32SFloat,
            [0x8815] = Vk.Format.R32G32B32SFloat,
            [0x8814] = Vk.Format.R32G32B32A32SFloat,
            [0x8C3D] = Vk.Format.E5B9G9R9UFloatPack32,
            [0x8231] = Vk.Format.R8SInt,
            [0x8232] = Vk.Format.R8UInt,
            [0x8233] = Vk.Format.R16SInt,
            [0x8234] = Vk.Format.R16UInt,
            [0x8235] = Vk.Format.R32SInt,
            [0x8236] = Vk.Format.R32UInt,
            [0x8237] = Vk.Format.R8G8SInt,
            [0x8238] = Vk.Format.R8G8UInt,
            [0x8239] = Vk.Format.R16G16SInt,
            [0x823A] = Vk.Format.R16G16UInt,
            [0x823B] = Vk.Format.R32G32SInt,
            [0x823C] = Vk.Format.R32G32UInt,
            [0x8D8F] = Vk.Format.R8G8B8SInt,
            [0x8D7D] = Vk.Format.R8G8B8UInt,
            [0x8D89] = Vk.Format.R16G16B16SInt,
            [0x8D77] = Vk.Format.R16G16B16UInt,
            [0x8D83] = Vk.Format.R32G32B32SInt,
            [0x8D71] = Vk.Format.R32G32B32UInt,
            [0x8D8E] = Vk.Format.R8G8B8A8SInt,
            [0x8D7C] = Vk.Format.R8G8B8A8UInt,
            [0x8D88] = Vk.Format.R16G16B16A16SInt,
            [0x8D76] = Vk.Format.R16G16B16A16UInt,
            [0x8D82] = Vk.Format.R32G32B32A32SInt,
            [0x8D70] = Vk.Format.R32G32B32A32UInt
        };

        public static Image LoadKtxImage(string path)
        {
            using (BinaryReader reader = new BinaryReader())
            {
                // Check if the file is in KTX format
                byte[] identifier = reader.ReadBytes(12);
                for (int i = 0; i < 12; ++i)
                {
                    if (identifier[i] != KtxIdentifier[i])
                    {
                        throw new InvalidOperationException("File \"" + path + "\" is not in KTX format.");
                    }
                }

                // Read KTX properties
                int endienness = reader.ReadInt32();
                int glType = reader.ReadInt32();
                int glTypeSize = reader.ReadInt32();
                int glFormat = reader.ReadInt32();
                int glInternalFormat = reader.ReadInt32();
                int glBaseInternalFormat = reader.ReadInt32();
                int pixelWidth = reader.ReadInt32();
                int pixelHeight = reader.ReadInt32();
                int pixelDepth = reader.ReadInt32();
                int numberOfArrayElements = reader.ReadInt32();
                int numberOfFaces = reader.ReadInt32();
                int numberOfMipmapLevels = reader.ReadInt32();
                int bytesOfKeyValueData = reader.ReadInt32();

                // Skip key-value data
                reader.ReadBytes(bytesOfKeyValueData);

                // Some of the values may be 0 - ensure at least 1
                pixelWidth = Math.Max(pixelWidth, 1);
                pixelHeight = Math.Max(pixelHeight, 1);
                pixelDepth = Math.Max(pixelDepth, 1);
                numberOfArrayElements = Math.Max(numberOfArrayElements, 1);
                numberOfFaces = Math.Max(numberOfFaces, 1);
                numberOfMipmapLevels = Math.Max(numberOfMipmapLevels, 1);

                int numberOfSlices = Math.Max(numberOfFaces, numberOfArrayElements);

                // Check if we support the internal format
                if (!GlInternalFormatToVkFormat.TryGetValue(glInternalFormat, out Vk.Format format))
                {
                    throw new NotImplementedException("GL internal format \"" + glInternalFormat + "\" not mapped to Vk.Format");
                }


            }
        }
    }
}