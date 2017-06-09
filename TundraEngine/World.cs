using System.Collections.Generic;

namespace TundraEngine
{
    public class World
    {
        private List<Entity> _entities = new List<Entity> (DefaultEntityCapacity);

        private const int DefaultEntityCapacity = 1024;

        public void AddEntity (Entity entity)
        {
            _entities.Add (entity);
        }

        public void Destroy ()
        {
            foreach (Entity entity in _entities)
            {
                entity.Destroy ();
            }
        }
    }
}