using Medrick.ComponentSystem.Core;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public class TileComponentCache : ComponentCache
    {
        public void TryCache(Component component)
        {
            // You can cache some most used components here to improve performance.
        }
    }


    // TODO: Maybe extract the Hit handling of tile to a separate component?
    public abstract class Tile : CacheableBasicEntity<TileComponentCache>
    {
        private int level;
        private TileStack parent;

        public Tile(int initialLevel) : base(new TileComponentCache())
        {
            level = initialLevel;
        }

        public Tile() : this(1)
        {
        }

        public void SetParent(TileStack parent)
        {
            this.parent = parent;
        }

        public void DecrementLevel()
        {
            --level;
        }

        public virtual bool IsDestroyed()
        {
            return level <= 0;
        }

        // WARNING: I'm not sure this is a good Idea. Use with care.
        public void ForceDestroy()
        {
            SetLevel(0);
        }

        public int CurrentLevel()
        {
            return level;
        }

        protected void SetLevel(int level)
        {
            this.level = level;
        }

        public TileStack Parent()
        {
            return parent;
        }
    }
}