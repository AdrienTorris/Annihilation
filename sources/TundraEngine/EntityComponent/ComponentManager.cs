using System;
using System.Collections.Generic;

namespace TundraEngine
{
    public class ComponentManager
    {
        protected int _count;
        protected Entity[] _entities;
        protected Dictionary<Entity, int> _indexMap;

        private static Dictionary<Guid, ComponentManager> _componentManagerMap = new Dictionary<Guid, ComponentManager> ();

        public ComponentManager (int capacity)
        {
            _count = 0;
            _indexMap = new Dictionary<Entity, int> (capacity);
        }
        
        public ComponentInstance Create (Entity entity, ComponentInstance instance)
        {
            int index = _count;
            _entities[index] = entity;
            _indexMap.Add (entity, index);
            ++_count;
            return index;
        }

        public void Destroy (Entity entity, ComponentInstance instance)
        {
            int index = instance.IsValid() ? instance.Index : _indexMap[entity];
            int lastIndex = _count - 1;

            Entity lastEntity = _entities[lastIndex];
            _indexMap[lastEntity] = index;
            _indexMap.Remove (entity);
            --_count;
        }
    }
}