using System;
using System.IO;
using System.Collections.Generic;
using Annihilation.Vk;
using Annihilation.Graphics;

namespace Annihilation
{
    public static class KTX
    {
        private static readonly byte[] KtxIdentifier =
        {
            0xAB, 0x4B, 0x54, 0x58, 0x20, 0x31, 0x31, 0xBB, 0x0D, 0x0A, 0x1A, 0x0A
        };

        // PERF: Replace by switch
        private static readonly Dictionary<int, Format> GlInternalFormatToFormat = new Dictionary<int, Format>()
        {
            [0x8229] = Format.R8UNorm,
            [0x8F94] = Format.R8SNorm,
            [0x822A] = Format.R16UNorm,
            [0x8F98] = Format.R16SNorm,
            [0x822B] = Format.R8G8UNorm,
            [0x8F95] = Format.R8G8SNorm,
            [0x822C] = Format.R16G16UNorm,
            [0x8F99] = Format.R16G16SNorm,
            [0x8D62] = Format.R5G6B5UNormPack16,
            [0x8051] = Format.R8G8B8UNorm,
            [0x8F96] = Format.R8G8B8SNorm,
            [0x8054] = Format.R16G16B16UNorm,
            [0x8F9A] = Format.R16G16B16SNorm,
            [0x8056] = Format.R4G4B4A4UNormPack16,
            [0x8057] = Format.R5G5B5A1UNormPack16,
            [0x8058] = Format.R8G8B8A8UNorm,
            [0x8F97] = Format.R8G8B8A8SNorm,
            [0x805B] = Format.R16G16B16A16UNorm,
            [0x8F9B] = Format.R16G16B16A16SNorm,
            [0x8C41] = Format.R8G8B8SRGB,
            [0x8C43] = Format.R8G8B8A8SRGB,
            [0x822D] = Format.R16SFloat,
            [0x822F] = Format.R16G16SFloat,
            [0x881B] = Format.R16G16B16SFloat,
            [0x881A] = Format.R16G16B16A16SFloat,
            [0x822E] = Format.R32SFloat,
            [0x8230] = Format.R32G32SFloat,
            [0x8815] = Format.R32G32B32SFloat,
            [0x8814] = Format.R32G32B32A32SFloat,
            [0x8C3D] = Format.E5B9G9R9UFloatPack32,
            [0x8231] = Format.R8SInt,
            [0x8232] = Format.R8UInt,
            [0x8233] = Format.R16SInt,
            [0x8234] = Format.R16UInt,
            [0x8235] = Format.R32SInt,
            [0x8236] = Format.R32UInt,
            [0x8237] = Format.R8G8SInt,
            [0x8238] = Format.R8G8UInt,
            [0x8239] = Format.R16G16SInt,
            [0x823A] = Format.R16G16UInt,
            [0x823B] = Format.R32G32SInt,
            [0x823C] = Format.R32G32UInt,
            [0x8D8F] = Format.R8G8B8SInt,
            [0x8D7D] = Format.R8G8B8UInt,
            [0x8D89] = Format.R16G16B16SInt,
            [0x8D77] = Format.R16G16B16UInt,
            [0x8D83] = Format.R32G32B32SInt,
            [0x8D71] = Format.R32G32B32UInt,
            [0x8D8E] = Format.R8G8B8A8SInt,
            [0x8D7C] = Format.R8G8B8A8UInt,
            [0x8D88] = Format.R16G16B16A16SInt,
            [0x8D76] = Format.R16G16B16A16UInt,
            [0x8D82] = Format.R32G32B32A32SInt,
            [0x8D70] = Format.R32G32B32A32UInt
        };

        //public static Image LoadKtxImage(string path)
        //{
        //    using (BinaryReader reader = new BinaryReader())
        //    {
        //        // Check if the file is in KTX format
        //        byte[] identifier = reader.ReadBytes(12);
        //        for (int i = 0; i < 12; ++i)
        //        {
        //            if (identifier[i] != KtxIdentifier[i])
        //            {
        //                throw new InvalidOperationException("File \"" + path + "\" is not in KTX format.");
        //            }
        //        }

        //        // Read KTX properties
        //        int endienness = reader.ReadInt32();
        //        int glType = reader.ReadInt32();
        //        int glTypeSize = reader.ReadInt32();
        //        int glFormat = reader.ReadInt32();
        //        int glInternalFormat = reader.ReadInt32();
        //        int glBaseInternalFormat = reader.ReadInt32();
        //        int pixelWidth = reader.ReadInt32();
        //        int pixelHeight = reader.ReadInt32();
        //        int pixelDepth = reader.ReadInt32();
        //        int numberOfArrayElements = reader.ReadInt32();
        //        int numberOfFaces = reader.ReadInt32();
        //        int numberOfMipmapLevels = reader.ReadInt32();
        //        int bytesOfKeyValueData = reader.ReadInt32();

        //        // Skip key-value data
        //        reader.ReadBytes(bytesOfKeyValueData);

        //        // Some of the values may be 0 - ensure at least 1
        //        pixelWidth = Math.Max(pixelWidth, 1);
        //        pixelHeight = Math.Max(pixelHeight, 1);
        //        pixelDepth = Math.Max(pixelDepth, 1);
        //        numberOfArrayElements = Math.Max(numberOfArrayElements, 1);
        //        numberOfFaces = Math.Max(numberOfFaces, 1);
        //        numberOfMipmapLevels = Math.Max(numberOfMipmapLevels, 1);

        //        int numberOfSlices = Math.Max(numberOfFaces, numberOfArrayElements);

        //        // Check if we support the internal format
        //        if (!GlInternalFormatToFormat.TryGetValue(glInternalFormat, out Format format))
        //        {
        //            throw new NotImplementedException("GL internal format \"" + glInternalFormat + "\" not mapped to Format");
        //        }


        //    }
        //}
    }
}