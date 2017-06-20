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
        private WorldFlags _worldFlags;
        private Entity[] _entities;
        private Dictionary<Entity, int> _indexMap;
        private List<ComponentManager> _componentManagers = new List<ComponentManager>();
        
        private const int DefaultEntityCapacity = 1024;

        public int NumEntities { get; private set; }

        public World(WorldFlags flags) :
            this(flags, DefaultEntityCapacity)
        { }

        public World(WorldFlags flags, int entityCapacity)
        {
            _worldFlags = flags;
            _componentManagers = new List<ComponentManager>(8);
            NumEntities = 0;
            _entities = new Entity[entityCapacity];
            _indexMap = new Dictionary<Entity, int>(entityCapacity);
        }

        public void Destroy()
        {
            foreach (var entity in _entities)
            {
                EntityManager.Destroy(entity);
            }
        }

        public Entity SpawnEntity()
        {
            Entity entity = EntityManager.Create();
            int index = NumEntities++;
            _entities[index] = entity;
            _indexMap.Add(entity, index);
            // TODO: Post EntitySpawned event
            return entity;
        }

        public Entity SpawnEntity(StringId32 name)
        {
            Entity entity = EntityManager.Create();
            EntityResource resource = ResourceManager.Get<EntityResource>(name);
            foreach (Guid componentId in resource.Components)
            {

            }
            return entity;
        }

        public void DestroyEntity(Entity entity)
        {
            EntityManager.Destroy(entity);
            int lastIndex = NumEntities - 1;
            int index = _indexMap[entity];
            _entities[index] = _entities[lastIndex];
            _indexMap[_entities[lastIndex]] = index;
            _indexMap.Remove(entity);
            --NumEntities;
            // TODO: Post EntityDestroyed event
        }

        private void UpdateAnimations(float deltaTime)
        {

        }

        private void UpdateTransforms(float deltaTime)
        {

        }

        private void UpdatePhysics()
        {

        }

        private void UpdateTimeOfDay(float deltaTime)
        {

        }

        public void Update(float deltaTime)
        {
            bool hasRendering = _worldFlags.Has(WorldFlags.Enable3D);

            if (hasRendering)
            {
                UpdateAnimations(deltaTime);
                UpdateTransforms(deltaTime);
            }
            if (_worldFlags.Has(WorldFlags.EnablePhysics))
            {
                UpdatePhysics();
            }
            if (_worldFlags.Has(WorldFlags.EnableTimeOfDay))
            {
                UpdateTimeOfDay(deltaTime);
            }
        }

        public void Render(Matrix view, Matrix projection)
        {

        }
    }
}