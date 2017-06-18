using System.Collections.Generic;

namespace TundraEngine
{
    /// <summary>
    /// A world manages entities through a set of component managers.
    /// </summary>
    public abstract class World
    {
        public List<ComponentManager> ComponentManagers { get; private set; }
        public List<Entity> Entities { get; private set; }

        private const int DefaultEntityCapacity = 1024;

        public World () : this(DefaultEntityCapacity) { }

        public World (int entityCapacity)
        {
            ComponentManagers = new List<ComponentManager> (8);
            Entities = new List<Entity>(entityCapacity);
        }

        public virtual void UpdateAnimations (float deltaTime)
        {

        }

        public virtual void UpdateTransformsAndPhysics (float deltaTime)
        {

        }

        public virtual void Update (float deltaTime)
        {
            UpdateAnimations (deltaTime);
            UpdateTransformsAndPhysics (deltaTime);
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