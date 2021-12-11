using System;
using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game.Core;
using Random = UnityEngine.Random;

namespace Medrick.Match3CoreSystem.Game
{
    public class RandomCellStackChooser
    {
        private readonly CellStackBoard cellStackBoard;
        private readonly HashSet<CellStack> chosenCellStacks = new HashSet<CellStack>();

        private readonly int lastLinearIndex;

        private Predicate<CellStack> originalMultiValidator;


        public RandomCellStackChooser(CellStackBoard cellStackBoard)
        {
            this.cellStackBoard = cellStackBoard;
            lastLinearIndex =
                cellStackBoard.PositionToLinearIndex(cellStackBoard.Width() - 1, cellStackBoard.Height() - 1);
        }

        public List<CellStack> Choose(int count, Predicate<CellStack> validator)
        {
            var cellStacks = new List<CellStack>();

            originalMultiValidator = validator;

            for (var i = 0; i < count; ++i)
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

        private bool MultiValidator(CellStack cellStack)
        {
            return chosenCellStacks.Contains(cellStack) == false && originalMultiValidator(cellStack);
        }

        public CellStack ChooseOne(Predicate<CellStack> validator)
        {
            var startPosition = Random.Range(0, lastLinearIndex + 1);

            var currentPosition = startPosition;
            CellStack current = null;
            var isValid = false;

            do
            {
                current = cellStackBoard[cellStackBoard.LinearIndexToPosition(currentPosition)];
                isValid = validator(current);
                currentPosition = (currentPosition + 1) % (lastLinearIndex + 1);
            } while (currentPosition != startPosition && isValid == false);

            if (isValid)
                return current;
            return null;
        }
    }
}