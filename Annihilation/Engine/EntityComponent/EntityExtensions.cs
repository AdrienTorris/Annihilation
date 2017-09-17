namespace Engine
{
    public static class EntityExtensions
    {
        public static bool IsAlive (this Entity entity)
        {
            return EntitySystem.IsAlive (entity);
        }
        
        public static void Destroy (this Entity entity)
        {
            EntitySystem.Destroy (entity);
        }
    }
}