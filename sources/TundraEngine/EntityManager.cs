using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public static class EntityManager
    {
        // Entities
        private static List<uint> _generations = new List<uint> (MinFreeIndices);
        private static Queue<uint> _freeIndexQueue = new Queue<uint> (MinFreeIndices);
        private const int MinFreeIndices = 1024;

        // Components
        private static Dictionary<byte, ComponentManager> _componentManagerMap = new Dictionary<byte, ComponentManager> ();

        public static Entity Create ()
        {
            Entity entity;

            if (_freeIndexQueue.Count > MinFreeIndices)
            {
                uint index = _freeIndexQueue.Dequeue ();
                entity = new Entity (index, _generations[(int)index]);
            }
            else
            {
                _generations.Add (0);
                uint index = (uint)_generations.Count - 1;
                entity = new Entity (index, 0);
            }
            
            return entity;
        }

        public static Entity Create (World world)
        {

        }

        public static Entity Spawn (World world, IEntityResource entityAsset)
        {
            Entity entity = Create();

            entityAsset.Spawn (entity);

            return entity;
        }

        public static Entity Spawn (World world, IEntityResource entityAsset, Matrix transform)
        {
            Entity entity = Spawn(world, entityAsset);

            entityAsset.Spawn (entity);

            return entity;
        }

        public static void Destroy (Entity entity)
        {
            if (!IsAlive (entity)) return;

            uint index = entity.Index;
            ++_generations[(int)index - 1];
            _freeIndexQueue.Enqueue (index);
        }

        public static bool IsAlive (Entity entity)
        {
            Assert.IsFalse (entity.Index - 1 < _generations.Count, "Index overflow.");
            return _generations[(int)entity.Index] == entity.Generation;
        }
    }
}