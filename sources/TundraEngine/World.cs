using System;
using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    /// <summary>
    /// A world manages entities through a set of component managers.
    /// </summary>
    public class World
    {
        public List<ComponentManager> ComponentManagers { get; private set; }

        private WorldFlags _worldFlags;

        private Entity[] _entities;
        private int _numEntities;
        private Dictionary<Entity, int> _indexMap;

        private ResourceManager _resourceManager;

        private const int DefaultEntityCapacity = 1024;

        public World (WorldFlags flags, ResourceManager resourceManager) : 
            this (flags, DefaultEntityCapacity, resourceManager) { }

        public World (WorldFlags flags, int entityCapacity, ResourceManager resourceManager)
        {
            _worldFlags = flags;
            ComponentManagers = new List<ComponentManager> (8);
            _numEntities = 0;
            _entities = new Entity[entityCapacity];
            _indexMap = new Dictionary<Entity, int> (entityCapacity);
        }

        public void Destroy ()
        {

        }

        public Entity SpawnEntity ()
        {
            Entity entity = EntityManager.Create ();
            int index = _numEntities++;
            _entities[index] = entity;
            _indexMap.Add (entity, index);
            // TODO: Post UnitSpawned event
            return entity;
        }

        public Entity SpawnEntity (Text name)
        {
            Entity entity = EntityManager.Create ();
            EntityResource resource = _resourceManager.Get<EntityResource> (name);
            foreach (Guid componentId in resource.Components)
            {

            }
            return entity;
        }
        
        public void DestroyEntity (Entity entity)
        {
            EntityManager.Destroy (entity);
            int lastIndex = _numEntities - 1;
            int index = _indexMap[entity];
            _entities[index] = _entities[lastIndex];
            _indexMap[_entities[lastIndex]] = index;
            _indexMap.Remove (entity);
            --_numEntities;
        }

        public void UpdateAnimations (float deltaTime)
        {

        }

        public void UpdateTransforms (float deltaTime)
        {

        }

        public void Render (Matrix view, Matrix projection)
        {

        }

        public void UpdatePhysics ()
        {

        }

        public void UpdateTimeOfDay (float deltaTime)
        {

        }

        public void Update (float deltaTime)
        {
            bool hasRendering = _worldFlags.Has (WorldFlags.EnableRendering);

            if (hasRendering)
            {
                UpdateAnimations (deltaTime);
                UpdateTransforms (deltaTime);
            }
            if (_worldFlags.Has (WorldFlags.EnablePhysics))
            {
                UpdatePhysics ();
            }
            if (_worldFlags.Has (WorldFlags.EnableTimeOfDay))
            {
                UpdateTimeOfDay (deltaTime);
            }
            if (hasRendering)
            {
                Render ();
            }
        }
    }
}