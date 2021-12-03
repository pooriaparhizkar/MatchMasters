using Medrick.ComponentSystem.Core;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public class CellComponentCache : ComponentCache
    {
        public void TryCache(Component component)
        {
            // You can cache some most used components here to improve performance.
        }
    }

    public abstract class Cell : CacheableBasicEntity<CellComponentCache>
    {
        private CellStack parent;

        public Cell() : base(new CellComponentCache())
        {
        }

        public void SetParent(CellStack parent)
        {
            this.parent = parent;
        }

        public CellStack Parent()
        {
            return parent;
        }
    }
}