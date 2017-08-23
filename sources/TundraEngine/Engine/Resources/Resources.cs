using System;
using System.IO;
using System.Collections.Generic;
using Engine.Rendering;
using Vulkan;

namespace Engine
{
    public static class Resources
    {
        private static readonly byte[] KtxIdentifier =
        {
            0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A
        };
        
        private static readonly Dictionary<int, Vk.Format> GlInternalFormatToVkFormat = new Dictionary<int, Vk.Format>()
        {
            [32855] = Vk.Format.R5G5B5A1UnormPack16,
            [32856] = Vk.Format.R8G8B8A8Unorm
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