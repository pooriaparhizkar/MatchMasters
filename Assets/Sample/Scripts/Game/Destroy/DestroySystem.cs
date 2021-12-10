using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;

namespace Sample
{
    public interface DestroySystemPresentationPort : PresentationPort
    {
        void PlayDestroy(CellStack cellStack1, Action onCompleted);
    }

    public class DestroySystemKeyType : KeyType
    {
    }

    public class DestroySystem : BasicGameplaySystem
    {
        private DestroyBlackBoard DestroyBlackBoard;
        private DestroySystemPresentationPort presentationPort;

        public DestroySystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            DestroyBlackBoard = GetFrameData<DestroyBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<DestroySystemPresentationPort>();
        }

        public override void Update(float dt)
        {
            foreach (var DestroyData in DestroyBlackBoard.requestedDestroys)
                StartDestroy(DestroyData);
        }

        private void StartDestroy(DestroyBlackBoard.DestroyData DestroyData)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            var cellStack1 = cellStackBoard[DestroyData.pos1];


            if (QueryUtilities.IsFullyFree(cellStack1))
            {
                gameplayController.LevelBoard.CellStackBoard().setBoardLock();
                ActionUtilites.FullyLock<DestroySystemKeyType>(cellStack1);

                presentationPort.PlayDestroy(cellStack1, () => ApplyDestroy(cellStack1));
            }
        }

        private void ApplyDestroy(CellStack cellStack1)
        {
            // Note that we only Destroy the TileStacks of these CellStacks. CellStacks are usually considered
            // fixed in the board.
            // ActionUtilites.DestroyTileStacksOf(cellStack1, cellStack2);
            // Debug.Log(cellStack1);
            // cellStack1.GetComponent<gemTilePresenter>().delete(cellStack1.CurrentTileStack());
            // cellStack1.CurrentTileStack().Destroy();
            // cellStack1.Pop();
            cellStack1.DetachTileStack();
            // ActionUtilites.FullyDestroy(cellStack1.CurrentTileStack().Top());
            ActionUtilites.FullyUnlock(cellStack1);
            gameplayController.LevelBoard.CellStackBoard().setBoardUnlock();
        }
    }
}