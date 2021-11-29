using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;

namespace Sample
{
    public class MainCellStackFactory : CellStackFactory
    {
        public CellStack Create(int xPos, int yPos)
        {
            var stack = new CellStack(xPos, yPos);
            stack.AddComponent(new LockState());
            return stack;
        }
    }
}