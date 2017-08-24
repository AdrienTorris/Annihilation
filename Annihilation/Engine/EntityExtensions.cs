namespace Engine
{
    public static class EntityExtensions
    {
        public static bool IsAlive (this Entity entity)
        {
            return EntityManager.IsAlive (entity);
        }
        
        public static void Destroy (this Entity entity)
        {
            EntityManager.Destroy (entity);
        }
    }
}