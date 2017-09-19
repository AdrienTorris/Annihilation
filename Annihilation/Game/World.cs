using System;
using System.Numerics;
using System.Collections.Generic;
using Engine.Core;

namespace Engine
{
    [Flags]
    public enum WorldFlags
    {
        Nothing = 0,
        EnableSound = 1 << 0,
        Enable3D = 1 << 1,
        Enable2D = 1 << 2,
        EnablePhysics = 1 << 3,
        EnableTimeOfDay = 1 << 4,
        Everything = ~0
    }

    internal static class WorldFlagsExtensions
    {
        public static bool Has(this WorldFlags variable, WorldFlags flag)
        {
            return (variable & flag) != 0;
        }

        public static bool HasNot(this WorldFlags variable, WorldFlags flag)
        {
            return (variable & flag) == 0;
        }
    }

    /// <summary>
    /// A world manages entities through a set of component managers.
    /// </summary>
    public class World
    {
        private WorldFlags _worldFlags;
        private Array<Entity> _entities;
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
            _entities = new Array<Entity>(entityCapacity);
            _indexMap = new Dictionary<Entity, int>(entityCapacity);
        }

        public void AddEntity(Entity entity)
        {
            _entities.Add(entity);
        }

        public void Destroy()
        {
            foreach (var entity in _entities)
            {
                EntitySystem.Destroy(entity);
            }
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

        public void Render(Matrix4x4 view, Matrix4x4 projection)
        {

        }
    }
}