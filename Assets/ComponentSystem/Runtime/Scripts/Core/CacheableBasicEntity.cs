namespace Medrick.ComponentSystem.Core
{
    public interface ComponentCache
    {
        void TryCache(Component component);
    }

    public class CacheableBasicEntity<T> : BasicEntity where T : ComponentCache
    {
        readonly T cache;

        public CacheableBasicEntity(T cache)
        {
            this.cache = cache;
        }

        protected override void OnComponentAdded(Component component)
        {
            cache.TryCache(component);
        }

        public T Cache()
        {
            return cache;
        }
    }

}