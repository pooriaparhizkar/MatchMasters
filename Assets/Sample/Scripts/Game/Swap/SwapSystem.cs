using System;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
using UnityEngine;

namespace Sample
{
    public interface SwapSystemPresentationPort : PresentationPort
    {
        void PlaySwap(CellStack cellStack1, CellStack cellStack2, Action onCompleted);
    }

    public class SwapSystemKeyType : KeyType
    {
    }

    public class SwapSystem : BasicGameplaySystem
    {
        private SwapSystemPresentationPort presentationPort;
        private SwapBlackBoard swapBlackBoard;

        public SwapSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            swapBlackBoard = GetFrameData<SwapBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<SwapSystemPresentationPort>();
        }

        public override void Update(float dt)
        {
            foreach (var swapData in swapBlackBoard.requestedSwaps)
                StartSwap(swapData);
        }

        private void StartSwap(SwapBlackBoard.SwapData swapData)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            var cellStack1 = cellStackBoard[swapData.pos1];
            var cellStack2 = cellStackBoard[swapData.pos2];


            if (cellStack1 == cellStack2)
                return;


            gameplayController.LevelBoard.CellStackBoard().setBoardLock();
            // ActionUtilites.FullyLock<SwapSystemKeyType>(cellStack1);
            // ActionUtilites.FullyLock<SwapSystemKeyType>(cellStack2);

            presentationPort.PlaySwap(cellStack1, cellStack2, () => ApplySwap(cellStack1, cellStack2, swapData.isDrag));
            socketLogic.sendChat("1", cellStack1.Position().ToString(), cellStack2.Position().ToString());
        }

        private void ApplySwap(CellStack cellStack1, CellStack cellStack2, bool isDrag)
        {
            // Note that we only swap the TileStacks of these CellStacks. CellStacks are usually considered 
            // fixed in the board.
            ActionUtilites.SwapTileStacksOf(cellStack1, cellStack2);

            // if (isDrag)
            // {
            //     GetFrameData<CheckBlackBoard>().requestedChecks.Add(
            //         new CheckBlackBoard.CheckData(cellStack1, cellStack2));
            //     await Task.Delay(300);
            //     gameplayController.LevelBoard.CellStackBoard().setBoardUnlock();
            // }
            // else
            // {
            //     gameplayController.LevelBoard.CellStackBoard().setBoardUnlock();
            // }


            gameplayController.LevelBoard.CellStackBoard().setBoardUnlock();

            // ActionUtilites.FullyUnlock(cellStack1);
            // ActionUtilites.FullyUnlock(cellStack2);
        }
    }
}