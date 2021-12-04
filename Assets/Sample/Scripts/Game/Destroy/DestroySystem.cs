using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

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
        private DestroySystemPresentationPort presentationPort;
        private DestroyBlackBoard DestroyBlackBoard;

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
                ActionUtilites.FullyLock<DestroySystemKeyType>(cellStack1);

                presentationPort.PlayDestroy(cellStack1,  () => ApplyDestroy(cellStack1));
            }
        }

        private void ApplyDestroy(CellStack cellStack1)
        {
            // Note that we only Destroy the TileStacks of these CellStacks. CellStacks are usually considered
            // fixed in the board.
            // ActionUtilites.DestroyTileStacksOf(cellStack1, cellStack2);
            // Debug.Log(cellStack1);
            // cellStack1.GetComponent<gemTilePresenter>().delete(cellStack1.CurrentTileStack());
            cellStack1.CurrentTileStack().Destroy();
            cellStack1.Pop();
            ActionUtilites.FullyUnlock(cellStack1);
        }
    }
}