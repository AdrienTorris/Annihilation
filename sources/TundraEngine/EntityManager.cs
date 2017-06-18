using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public static class EntityManager
    {
        // Entities
        private static List<byte> _generations = new List<byte> (MinFreeIndices);
        private static Queue<int> _freeIndexQueue = new Queue<int> (MinFreeIndices);
        private const int MinFreeIndices = 1024;

        // Components
        private static Dictionary<byte, ComponentManager> _componentManagerMap = new Dictionary<byte, ComponentManager> ();

        public static Entity Create (World owner)
        {
            Entity entity;

            if (_freeIndexQueue.Count > MinFreeIndices)
            {
                int index = _freeIndexQueue.Dequeue ();
                entity = new Entity (index, _generations[index]);
            }
            else
            {
                _generations.Add (0);
                int index = _generations.Count;
                entity = new Entity (index, 0);
            }
            
            return entity;
        }

        public static Entity Spawn (World world, IEntityAsset entityAsset)
        {
            Entity entity = Create(world);

            entityAsset.Spawn (entity);

            return entity;
        }

        public static Entity Spawn (World world, IEntityAsset entityAsset, Matrix transform)
        {
            Entity entity = Spawn(world, entityAsset);

            entityAsset.Spawn (entity);

            return entity;
        }

        public static void Destroy (Entity entity)
        {
            if (!IsAlive (entity)) return;

            int index = entity.Index;
            ++_generations[index - 1];
            _freeIndexQueue.Enqueue (index);
        }

        public static bool IsAlive (Entity entity)
        {
            Assert.IsFalse (entity.Index - 1 < _generations.Count, "Index overflow.");
            return _generations[entity.Index - 1] == entity.Generation;
        }
    }
}