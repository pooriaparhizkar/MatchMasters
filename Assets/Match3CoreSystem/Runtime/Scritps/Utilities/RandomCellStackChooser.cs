
using Medrick.Match3CoreSystem.Game.Core;
using System;
using System.Collections.Generic;

namespace Medrick.Match3CoreSystem.Game
{
    public class RandomCellStackChooser
    {
        CellStackBoard cellStackBoard;

        Predicate<CellStack> originalMultiValidator;
        HashSet<CellStack> chosenCellStacks = new HashSet<CellStack>();

        int lastLinearIndex;


        public RandomCellStackChooser(CellStackBoard cellStackBoard)
        {
            this.cellStackBoard = cellStackBoard;
            this.lastLinearIndex = cellStackBoard.PositionToLinearIndex(cellStackBoard.Width() - 1, cellStackBoard.Height() - 1);
        }

        public List<CellStack> Choose(int count, Predicate<CellStack> validator)
        {
            List<CellStack> cellStacks = new List<CellStack>();

            this.originalMultiValidator = validator;

            for (int i = 0; i < count; ++i)
            {
                var stack = ChooseOne(MultiValidator);
                if (stack != null)
                {
                    chosenCellStacks.Add(stack);
                    cellStacks.Add(stack);
                }
            }

            chosenCellStacks.Clear();

            return cellStacks;
        }

        bool MultiValidator(CellStack cellStack)
        {
            return chosenCellStacks.Contains(cellStack) == false && originalMultiValidator(cellStack);
        }

        public CellStack ChooseOne(Predicate<CellStack> validator)
        {
            var startPosition = UnityEngine.Random.Range(0, lastLinearIndex + 1);

            var currentPosition = startPosition;
            CellStack current = null;
            bool isValid = false;

            do
            {
                current = cellStackBoard[cellStackBoard.LinearIndexToPosition(currentPosition)];
                isValid = validator(current);
                currentPosition = (currentPosition + 1) % (lastLinearIndex + 1);
            } while (currentPosition != startPosition && isValid == false);

            if (isValid)
                return current;
            else
                return null;
        }
    }

}