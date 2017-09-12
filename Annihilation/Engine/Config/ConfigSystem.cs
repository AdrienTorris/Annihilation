using System.IO;
using System.Collections.Generic;
using Engine.Graphics;

namespace Engine.Config
{
    public static class ConfigSystem
    {
        public static bool IsVisible { get; private set; }

        private const int MaxUnregisteredVars = 1024;
        private static string[] _unregisteredPaths = new string[MaxUnregisteredVars];
        private static ConfigVar[] _unregisteredVars = new ConfigVar[MaxUnregisteredVars];

        private static int _unregisteredCount = 0;
        private static float _scrollOffset = 0f;
        private static float _scrollTopTrigger = 1080f * 0.2f;
        private static float _scrollBottomTrigger = 1080f * 0.8f;

        private static ConfigVar _selectedVar = null;

        public static void Initialize()
        {
            for (int i = 0; i < _unregisteredCount; ++i)
            {
                Assert.IsFalse(string.IsNullOrEmpty(_unregisteredPaths[i]), "Register = " + i);
                Assert.IsFalse(_unregisteredVars[i] == null);

                AddToVarGraph(_unregisteredPaths[i], _unregisteredVars[i]);
            }
            _unregisteredCount = -1;
        }

        public static void HandleInput(float deltaTime)
        {

        }

        public static void Display(GraphicsContext context, float x, float y, float w, float h)
        {

        }

        internal static void RegisterVar(string path, ConfigVar var)
        {
            if (_unregisteredCount >= 0)
            {
                int index = _unregisteredCount++;
                _unregisteredPaths[index] = path;
                _unregisteredVars[index] = var;
            }
            else
            {
                AddToVarGraph(path, var);
            }
        }

        private static void AddToVarGraph(string path, ConfigVar var)
        {
            List<string> separatedPath = new List<string>();
            string leafName;
            int start = 0;
            int end = 0;

            while (true)
            {
                end = path.IndexOf('/', start);
                if (end == -1)
                {
                    leafName = path.Substring(start);
                    break;
                }
                else
                {
                    separatedPath.Add(path.Substring(start, end - start));
                    start = end + 1;
                }
            }

            ConfigVarGroup group = ConfigVarGroup.Root;

            // TODO: Remove this
            if (group == null) return;

            foreach (string part in separatedPath)
            {
                ConfigVarGroup nextGroup;
                ConfigVar node = group.FindChild(part);
                if (node == null)
                {
                    nextGroup = new ConfigVarGroup();
                    group.AddChild(part, nextGroup);
                    group = nextGroup;
                }
                else
                {
                    nextGroup = (ConfigVarGroup)node;
                    group = nextGroup;
                }
            }

            group.AddChild(leafName, var);
        }
    }

    public class ConfigVarGroup : ConfigVar
    {
        public static ConfigVarGroup Root;

        public bool IsExpanded { get; private set; }

        private Dictionary<Hash, ConfigVar> _children;

        public ConfigVarGroup() { }

        public ConfigVarGroup(string path) : base(path)
        {
        }

        public ConfigVar FindChild(string name)
        {
            Hash hash = new Hash(name);
            if (_children.TryGetValue(hash, out ConfigVar child))
            {
                return child;
            }
            return null;
        }

        public void AddChild(string name, ConfigVar child)
        {
            Hash hash = new Hash(name);
            _children[hash] = child;
            child.Group = this;
        }

        public ConfigVar FirstVar()
        {
            // TODO: Remove the dictionary implementation
            return _children.Count == 0 ? null : _children.Values.GetEnumerator().Current;
        }

        public ConfigVar LastVar()
        {
            return null;
        }

        public ConfigVar NextVar(ConfigVar currentVar)
        {
            return null;
        }

        public ConfigVar PreviousVar(ConfigVar currentVar)
        {
            return null;
        }

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
        
        public override void SetValue(StreamReader stream, string setting)
        {
            throw new System.NotImplementedException();
        }
    }
}