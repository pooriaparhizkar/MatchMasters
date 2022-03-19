using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
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
            {
                StartDestroy(DestroyData);
            }
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
            gemTile localGemTile = cellStack1.CurrentTileStack().Top() as gemTile;
            if (turnHandler.isMyTurn())
            {

                if (turnHandler.getRemainMove() % 2 == 0)
                {
                    Debug.Log("111111111111111111111111111");
                    GetFrameData<ScoreBlackBoard>().requestedScores.Add(
                        new ScoreBlackBoard.ScoreData(!turnHandler.isMyTurn()));

                    if (localGemTile._color == gemColors.red)
                    {
                        Debug.Log("________________111111111111111111111111111");
                        GetFrameData<BoosterProgressBarBlackBoard>().requestedBoosterProgressBars
                            .Add(new BoosterProgressBarBlackBoard.BoosterProgressBarData(!turnHandler.isMyTurn()));
                    }
                }
                else
                {
                    Debug.Log("22222222222222222222222222");
                    GetFrameData<ScoreBlackBoard>().requestedScores.Add(
                        new ScoreBlackBoard.ScoreData(turnHandler.isMyTurn()));

                    if (localGemTile._color == gemColors.blue)
                    {
                        Debug.Log("_________________22222222222222222222222222");
                        GetFrameData<BoosterProgressBarBlackBoard>().requestedBoosterProgressBars
                            .Add(new BoosterProgressBarBlackBoard.BoosterProgressBarData(turnHandler.isMyTurn()));
                    }
                }
            }
            else
            {


                if (turnHandler.getRemainMove() % 2 == 1)
                {
                    Debug.Log("33333333333333333333333333333");
                    GetFrameData<ScoreBlackBoard>().requestedScores.Add(
                        new ScoreBlackBoard.ScoreData(turnHandler.isMyTurn()));

                    if (localGemTile._color == gemColors.red)
                    {
                        Debug.Log("_______________33333333333333333333333333333");
                        GetFrameData<BoosterProgressBarBlackBoard>().requestedBoosterProgressBars
                            .Add(new BoosterProgressBarBlackBoard.BoosterProgressBarData(turnHandler.isMyTurn()));
                    }
                }
                else
                {
                    Debug.Log("4444444444444444444444444444");
                    GetFrameData<ScoreBlackBoard>().requestedScores.Add(
                        new ScoreBlackBoard.ScoreData(!turnHandler.isMyTurn()));

                    if (localGemTile._color == gemColors.blue)
                    {
                        Debug.Log("_____________444444444444444");
                        GetFrameData<BoosterProgressBarBlackBoard>().requestedBoosterProgressBars
                            .Add(new BoosterProgressBarBlackBoard.BoosterProgressBarData(!turnHandler.isMyTurn()));
                    }
                }
            }

            // GetFrameData<ScoreBlackBoard>().requestedScores.Add(
            //     new ScoreBlackBoard.ScoreData(turnHandler.isMyTurn()));


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