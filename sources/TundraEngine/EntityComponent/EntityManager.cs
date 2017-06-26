using System.Collections.Generic;
using System.Runtime.CompilerServices;

using TundraEngine.Mathematics;

namespace TundraEngine
{
    public static class EntityManager
    {
        // Entities
        private static List<uint> _generations = new List<uint>(MinFreeIndices);
        private static Queue<uint> _freeIndexQueue = new Queue<uint>(MinFreeIndices);
        private static List<World> _owners = new List<World>();
        private const int MinFreeIndices = 1024;

        /// <summary>
        /// Creates a new entity.
        /// </summary>
        public static Entity Create(World world)
        {
            Entity entity;

            if (_freeIndexQueue.Count > MinFreeIndices)
            {
                uint index = _freeIndexQueue.Dequeue();
                entity = new Entity(index, _generations[(int)index]);
            }
            else
            {
                _generations.Add(0);
                uint index = (uint)_generations.Count - 1;
                entity = new Entity(index, 0);
            }

            return entity;
        }

        public static Entity Spawn(World world, StringId64 entityResource)
        {
            Entity entity = Create(world);

            //entityResource.Spawn (entity);

            return entity;
        }

        public static Entity Spawn(World world, EntityResource entityResource)
        {
            Entity entity = Create(world);

            //entityResource.Spawn (entity);

            return entity;
        }

        public static Entity Spawn(World world, StringId64 entityResource, Matrix transform)
        {
            Entity entity = Spawn(world, entityResource);

            //entityResource.Spawn (entity);

            return entity;
        }

        /// <summary>
        /// Destroys the specified entity.
        /// </summary>
        public static void Destroy(Entity entity)
        {
            if (!IsAlive(entity)) return;

            uint index = entity.Index;
            ++_generations[(int)index - 1];
            _freeIndexQueue.Enqueue(index);
        }

        /// <summary>
        /// Destroys all entities in the specified world.
        /// </summary>
        public static void DestroyAll(World world)
        {
            
        }

        public static Entity Get(World world, int index)
        {

        }

        public static void GetAll(World world, out Entity[] entities)
        {

        }

        public static int Count(World world)
        {

        }

        public static World GetWorld(Entity entity)
        {
            return _owners[(int)entity.Index];
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsAlive(Entity entity)
        {
            Assert.IsFalse(entity.Index - 1 < _generations.Count, "Index overflow.");
            return _generations[(int)entity.Index] == entity.Generation;
        }
    }
}