
namespace TundraEngine
{
    public class NameComponentManager : ComponentManager
    {
        private int _count;
        private Entity[] _entities;
        private string[] _names;

        public NameComponentManager (int capacity) : base (capacity)
        {
            _count = 0;
            _entities = new Entity[capacity];
            _names = new string[capacity];
        }
        
        public int Create (Entity entity)
        {
            int index = _count;
            _entities[index] = entity;
            _indexMap.Add (entity, index);
            ++_count;
            return index;
        }

        public void Destroy (Entity entity, int index = -1)
        {
            int idx = index > -1 ? index : _indexMap[entity];
            int last = _count - 1;

            _indexMap.Remove (entity);
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