
namespace TundraEngine
{
    public class NameComponentManager : ComponentManager
    {
        private string[] _names;

        public NameComponentManager (int capacity) : base (capacity)
        {
            _names = new string[capacity];
        }
        
        public void SetName (Entity entity, string name)
        {
            int index = _indexMap[entity];
            _names[index] = name;
        }

        public string GetName (Entity entity)
        {
            int index = _indexMap[entity];
            return _names[index];
        }
    }
}