﻿using System.Collections.Generic;
using TundraEngine.Mathematics;

namespace TundraEngine
{
    public static class EntityManager
    {
        private static List<byte> _generations = new List<byte> (MinFreeIndices);
        private static Queue<int> _freeIndexQueue = new Queue<int> (MinFreeIndices);
        
        private const int MinFreeIndices = 1024;
        
        public static Entity Create (World world)
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

            if (!ReferenceEquals(world, null))
            {
                world.AddEntity (entity);
            }

            return entity;
        }

        public static Entity Spawn (EntityAsset entityAsset, World world)
        {
            Entity entity = Create(world);

            foreach (Component component in entityAsset.Components)
            {

            }

            return entity;
        }

        public static Entity Spawn (EntityAsset entityAsset, World world, Vector3 position, Quaternion orientation)
        {
            Entity entity = Spawn(entityAsset, world);

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
            // TODO: Index could easily be out of bounds here. Handle that?
            return _generations[entity.Index - 1] == entity.Generation;
        }
    }
}