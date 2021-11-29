using Medrick.ComponentSystem.Core;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public abstract class CellAttachment : BasicEntity
    {
        CellStack owner;

        public CellStack Owner()
        {
            return owner;
        }

        public void SetOwner(CellStack owner)
        {
            this.owner = owner;
        }

        public bool HasOwner()
        {
            return owner != null;
        }
    }
}