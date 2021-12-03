using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;

namespace Sample
{
    public interface SystemTwoPresentationPort : PresentationPort
    {
        void PlaySwap(CellStack cellStack1, CellStack cellStack2, Action onCompleted);
    }

    public class SystemTwoKeyType : KeyType
    {
    }

    public class SystemTwo : BasicGameplaySystem
    {
        private BlackBoardDataOne blackBoardDataOne;
        private SystemTwoPresentationPort presentationPort;

        public SystemTwo(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            blackBoardDataOne = GetFrameData<BlackBoardDataOne>();
            presentationPort = gameplayController.GetPresentationPort<SystemTwoPresentationPort>();
        }

        public override void Update(float dt)
        {
            foreach (var swapData in blackBoardDataOne.requestedSwaps)
                StartSwap(swapData);
        }

        private void StartSwap(BlackBoardDataOne.SwapData swapData)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            var cellStack1 = cellStackBoard[swapData.pos1];
            var cellStack2 = cellStackBoard[swapData.pos2];

            if (cellStack1 == cellStack2)
                return;

            if (QueryUtilities.IsFullyFree(cellStack1) && QueryUtilities.IsFullyFree(cellStack2))
            {
                ActionUtilites.FullyLock<SystemTwoKeyType>(cellStack1);
                ActionUtilites.FullyLock<SystemTwoKeyType>(cellStack2);

                presentationPort.PlaySwap(cellStack1, cellStack2, () => ApplySwap(cellStack1, cellStack2));
            }
        }

        private void ApplySwap(CellStack cellStack1, CellStack cellStack2)
        {
            // Note that we only swap the TileStacks of these CellStacks. CellStacks are usually considered 
            // fixed in the board.
            ActionUtilites.SwapTileStacksOf(cellStack1, cellStack2);

            ActionUtilites.FullyUnlock(cellStack1);
            ActionUtilites.FullyUnlock(cellStack2);
        }
    }
}