using Medrick.Match3CoreSystem.Game.Core;
using System;
using System.Collections.Generic;

namespace Medrick.Match3CoreSystem.Game
{
    public interface TileStackFactory
    {
        TileStack Create();
    }

    public interface CellStackFactory
    {
        CellStack Create(int xPos, int yPos);
    }

    public class CreationController
    {
        public event Action<TileStack> OnTileStackPlacedOnBoard = delegate { };
        public event Action<Tile> OnTilePlacedOnBoard = delegate { };
        public event Action<Cell> OnCellPlacedOnBoard = delegate { };
        public event Action<Cell> OnCellReordered = delegate { };


        private TileStackFactory tileStackFactory;
        CellStackBoard cellStackBoard;

        public CreationController(TileStackFactory tileStackFactory, CellStackBoard cellStackBoard)
        {
            this.tileStackFactory = tileStackFactory;
            this.cellStackBoard = cellStackBoard;
        }

        public void ReplaceTileInBoard(Tile tile, CellStack cellStack)
        {
            if (cellStack.HasTileStack())
            {
                cellStack.CurrentTileStack().Destroy();
            }

            var tileStack = tileStackFactory.Create();
            tileStack.Push(tile);

            cellStack.SetCurrnetTileStack(tileStack);
            tileStack.SetPosition(cellStack.Position());

            OnTileStackPlacedOnBoard.Invoke(tileStack);
        }

        public void PlaceTileInBoard(Tile tile, CellStack cellStack)
        {

            if (cellStack.HasTileStack() == false)
            {
                cellStack.SetCurrnetTileStack(tileStackFactory.Create());
                OnTileStackPlacedOnBoard.Invoke(cellStack.CurrentTileStack());
            }

            var tileStack = cellStack.CurrentTileStack();
            tileStack.Push(tile);
            tileStack.SetPosition(cellStack.Position());

            OnTilePlacedOnBoard.Invoke(tile);
        }

        public void PlaceCellInBoard(Cell cell, CellStack cellStack)
        {
            cellStack.Push(cell);

            OnCellPlacedOnBoard.Invoke(cell);
        }

        // TODO: Refactor this.
        public void MoveToTop(Cell targetCell)
        {
            var stack = new Stack<Cell>();
            var cellStack = targetCell.Parent();

            while(true)
            {
                var cell = cellStack.Stack().Pop();
                if (cell == targetCell)
                    break;
                else
                    stack.Push(cell);
            }

            foreach (var cell in stack)
                cellStack.Push(cell);

            cellStack.Push(targetCell);
            OnCellReordered.Invoke(targetCell);
        }
    }
}