namespace TundraEngine.IMGUI
{
    public enum DebugUIType : byte
    {
        None,
        Vulkan,
        BGFX,
        DearIMGUI,
    }

    public struct DebugUIInfo
    {
        public DebugUIType DebugUIType;
    }
}