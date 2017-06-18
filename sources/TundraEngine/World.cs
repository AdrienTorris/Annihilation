using System.Collections.Generic;

namespace TundraEngine
{
    /// <summary>
    /// A world manages entities through a set of component managers.
    /// </summary>
    public class World
    {
        public List<Entity> Entities { get; private set; }

        private const int DefaultEntityCapacity = 1024;

        public World ()
        {
            Entities = new List<Entity>(DefaultEntityCapacity);
        }

        public World (int entityCapacity)
        {
            Entities = new List<Entity>(entityCapacity);
        }

        public void AddEntity (Entity entity)
        {
            Entities.Add (entity);
        }

        public void Update (float deltaTime)
        {

        }

        public void Destroy ()
        {
            foreach (Entity entity in Entities)
            {
                entity.Destroy ();
            }
        }
    }
}