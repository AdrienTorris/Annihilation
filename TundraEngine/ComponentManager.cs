using System.Collections.Generic;

namespace TundraEngine
{
    public abstract class ComponentManager
    {
        protected int _count;
        protected Entity[] _entities;
        protected Dictionary<Entity, int> _indexMap;

        public ComponentManager (int capacity)
        {
            _count = 0;
            _indexMap = new Dictionary<Entity, int> (capacity);
        }

        public int Create (Entity entity)
        {
            int index = _count;
            _entities[index] = entity;
            _indexMap.Add (entity, index);
            ++_count;
            return index;
        }

        public void Destroy (Entity entity, int instanceIndex = -1)
        {
            int index = instanceIndex > -1 ? instanceIndex : _indexMap[entity];
            int lastIndex = _count - 1;

            Entity lastEntity = _entities[lastIndex];
            _indexMap[lastEntity] = index;
            _indexMap.Remove (entity);
            --_count;
        }
    }
}