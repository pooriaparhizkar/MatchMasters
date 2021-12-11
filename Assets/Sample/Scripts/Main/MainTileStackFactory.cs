using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;

namespace Sample
{
    public class MainTileStackFactory : TileStackFactory
    {
        public TileStack Create()
        {
            var stack = new TileStack();
            stack.AddComponent(new LockState());
            return stack;
        }
    }
}