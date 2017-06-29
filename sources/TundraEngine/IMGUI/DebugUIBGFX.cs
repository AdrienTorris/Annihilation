using System.Runtime.CompilerServices;

using TundraEngine.Mathematics;
using SharpBgfx;

using static SharpBgfx.Bgfx;

namespace TundraEngine.IMGUI
{
    internal class DebugUIBGFX : LibrarySystem<LibBGFX>, IDebugUI
    {
        public DebugUIBGFX()
        {
            // Enable debug text
            SetDebugFeatures(DebugFeatures.DisplayText);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            DebugTextClear();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, string text)
        {
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, Color foreColor, string text)
        {
            // TODO: Implement color.ToBGFX()
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Text(int x, int y, Color foreColor, Color backColor, string text)
        {
            // TODO: Implement color.ToBGFX()
            DebugTextWrite(x, y, DebugColor.White, DebugColor.Transparent, text);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Image(int x, int y, int width, int height, byte[] pixels, int stride)
        {
            DebugTextImage(x, y, width, height, pixels, stride);
        }

        protected override void InitializeLibrary()
        {
            Application.InitializeBGFX();
        }

        protected override void ShutdownLibrary()
        {
            Shutdown();
        }

        protected override void DisposeUnmanaged()
        {

        }
    }
}