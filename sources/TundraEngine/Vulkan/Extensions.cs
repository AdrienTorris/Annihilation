﻿using System.Runtime.CompilerServices;

namespace Vulkan
{
    public static class ResultExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckError(this Result result)
        {
            if (result < 0)
            {
                throw new VulkanException(result);
            }
        }
    }
}