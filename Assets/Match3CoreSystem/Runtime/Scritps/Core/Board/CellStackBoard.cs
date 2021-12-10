namespace Medrick.Match3CoreSystem.Game.Core
{
    public class CellStackBoard : Grid<CellStack>
    {
        private bool isLock;

        public bool isBoardLock()
        {
            return isLock;
        }

        public void setBoardLock()
        {
            isLock = true;
        }
        public void setBoardUnlock()
        {
            isLock = false;
        }
        public CellStackBoard(int width, int height) : base(width, height)
        {
        }
    }
}