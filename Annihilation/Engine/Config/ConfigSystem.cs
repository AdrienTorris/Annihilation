using Engine.Graphics;

namespace Engine.Config
{
    public static class ConfigSystem
    {
        public const int MaxConfigVars = 2048;

        private const int MaxUnregisteredVars = 1024;
        private static string[] _unregisteredNames = new string[MaxUnregisteredVars];

        private static int _unregisteredCount;
        private static float _scrollOffset = 0f;
        private static float _scrollTopTrigger = 1080f * 0.2f;
        private static float _scrollBottomTrigger = 1080f * 0.8f;

        public static bool IsVisible { get; private set; }

        public static void Initialize()
        {
            _unregisteredCount = -1;
        }

        public static void HandleInput(float deltaTime)
        {
        }

        public static void Display(GraphicsContext context, float x, float y, float w, float h)
        {
        }

        internal static void RegisterBool(Hash name, BoolVar var)
        {
        }

        internal static void RegisterInt(Hash name, IntVar var)
        {
        }

        internal static void RegisterFloat(Hash name, FloatVar var)
        {
        }

        internal static void RegisterString(Hash name, StringVar var)
        {
        }

        public static void SetBool(Hash name, bool value)
        {

        }

        public static void SetInt(Hash name, int value)
        {

        }

        public static void SetFloat(Hash name, float value)
        {

        }

        public static void SetString(Hash name, string value)
        {

        }
    }
}