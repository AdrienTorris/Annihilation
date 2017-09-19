using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Annihilation.Core;

namespace Annihilation
{
    public static class EntitySystem
    {
        public const int MaxEntities = 10240;
        private const int MinFreeIndices = 1024;

        private static Array<uint> _generations = new Array<uint>(MinFreeIndices);
        private static Queue<uint> _freeIndices = new Queue<uint>(MinFreeIndices);

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        public static Entity Create()
        {
            Entity entity;

            if (_freeIndices.Count > MinFreeIndices)
            {
                uint index = _freeIndices.Dequeue();
                entity = new Entity(index, _generations[index]);
            }
            else
            {
                _generations.Add(0);
                uint index = (uint)_generations.Count - 1;
                entity = new Entity(index, 0);
            }

            return entity;
        }

        /// <summary>
        /// Spawns a new empty entity in the specified world.
        /// </summary>
        public static Entity Spawn(World world)
        {
            Entity entity = Create();
            world.AddEntity(entity);
            return entity;
        }

        /// <summary>
        /// Spawns a new entity and its components from a resource.
        /// </summary>
        public static Entity Spawn(Name name, World world)
        {
            Entity entity = Create();

            return entity;
        }
        
        /// <summary>
        /// Destroys the specified entity.
        /// </summary>
        public static void Destroy(Entity entity)
        {
            uint index = entity.Index;
            ++_generations[index - 1];
            _freeIndices.Enqueue(index);
        }

        /// <summary>
        /// Destroys all entities in the specified world.
        /// </summary>
        public static void DestroyAll(World world)
        {
            
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(Entity entity)
        {
            return _generations[entity.Index] == entity.Generation;
        }
    }
}