using System.IO;
using System.Collections.Generic;
using Engine.Graphics;

namespace Engine.Config
{
    public static class ConfigSystem
    {
        public static bool IsFocused { get; }

        public static void Initialize()
        {

        }

        public static void Update(float deltaTime)
        {

        }

        public static void Display(GraphicsContext context, float x, float y, float w, float h)
        {

        }
    }

    private class ConfigVarGroup : ConfigVar
    {
        private bool _isExpanded;
        private Dictionary<Hash, ConfigVar> _children;

        public override void Decrement()
        {
            throw new System.NotImplementedException();
        }

        public override void DisplayValue(TextContext textContext)
        {
            throw new System.NotImplementedException();
        }

        public override void Increment()
        {
            throw new System.NotImplementedException();
        }

        public override void Select()
        {
            throw new System.NotImplementedException();
        }

        public override void SetValue(FileStream file, string setting)
        {
            throw new System.NotImplementedException();
        }
    }
}