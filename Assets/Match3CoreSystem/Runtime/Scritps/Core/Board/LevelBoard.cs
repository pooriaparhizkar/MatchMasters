using UnityEngine;

namespace Medrick.Match3CoreSystem.Game.Core
{
    public class LevelBoard
    {
        private readonly CellStackBoard cellStackBoard;
        public readonly CellStack[] leftToRightButtomUpCellStackArray;
        public readonly CellStack[] leftToRightTopDownCellStackArray;

        public readonly int size;

        public LevelBoard(CellStackBoard cellStackBoard)
        {
            this.cellStackBoard = cellStackBoard;

            size = cellStackBoard.Width() * cellStackBoard.Height();

            leftToRightButtomUpCellStackArray = new CellStack[size];
            leftToRightTopDownCellStackArray = new CellStack[size];


            RefillArrays();
        }

        public void RefillArrays()
        {
            var i = 0;
            foreach (var element in new LeftToRightBottomUpGridIterator<CellStack>(cellStackBoard))
            {
                leftToRightButtomUpCellStackArray[i] = element.value;
                ++i;
            }

            i = 0;
            foreach (var element in new LeftToRightTopDownGridIterator<CellStack>(cellStackBoard))
            {
                leftToRightTopDownCellStackArray[i] = element.value;
                ++i;
            }
        }

        public CellStack[] ArrbitrayCellStackArray()
        {
            return leftToRightTopDownCellStackArray;
        }

        public CellStackBoard CellStackBoard()
        {
            return cellStackBoard;
        }


        // TODO: Find a better name.
        public TileStack DirectionalTileStackOf(int x, int y, Direction dir)
        {
            var cell = cellStackBoard.DirectionalElementOf(x, y, dir);

            if (cell == null)
                return null;

            return cell.CurrentTileStack();
        }

        // TODO: Find a better name.
        public TileStack DirectionalTileStackOf(Vector2Int pos, Direction dir)
        {
            return DirectionalTileStackOf(pos.x, pos.y, dir);
        }

        // TODO: Find a better name.
        public CellStack DirectionalCellStackOf(int x, int y, Direction dir)
        {
            return cellStackBoard.DirectionalElementOf(x, y, dir);
        }

        public CellStack DirectionalCellStackOf(Vector2Int pos, Direction dir)
        {
            return DirectionalCellStackOf(pos.x, pos.y, dir);
        }

        public bool AreAdjacent(TileStack tileStack1, TileStack tileStack2)
        {
            return cellStackBoard.AreAdjacent(tileStack1.Parent().Position(), tileStack2.Parent().Position());
        }

        public Direction RelativeDirectionOf(TileStack from, TileStack to)
        {
            return cellStackBoard.RelativeDirectionOf(from.Parent().Position(), to.Parent().Position());
        }

        public Direction RelativeDirectionOf(Vector2Int from, Vector2Int to)
        {
            return cellStackBoard.RelativeDirectionOf(from, to);
        }

        public GridIterator<CellStack> DefaultCellBoardIterator()
        {
            return new LeftToRightTopDownGridIterator<CellStack>(cellStackBoard);
        }
    }
}