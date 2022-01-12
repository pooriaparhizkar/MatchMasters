using System;
using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample
{
    public interface InGameBoosterActivationSystemPresentationPort : PresentationPort
    {
        void PlayInGameBoosterActivation(
            InGameBoosterActivationBlackBoard.InGameBoosterActivationData InGameBoosterActivationData,
            BasicGameplayMainController gameplayController,
            Action onCompleted);
    }

    public class InGameBoosterActivationSystemKeyType : KeyType
    {
    }

    public class InGameBoosterActivationSystem : BasicGameplaySystem
    {
        private InGameBoosterActivationSystemPresentationPort presentationPort;
        private InGameBoosterActivationBlackBoard InGameBoosterActivationBlackBoard;

        public InGameBoosterActivationSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            InGameBoosterActivationBlackBoard = GetFrameData<InGameBoosterActivationBlackBoard>();
        }


        public override void Update(float dt)
        {
            foreach (var item in InGameBoosterActivationBlackBoard.requestedInGameBoosterActivations)
            {
                //updown arrow
                if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    for (int i = 0; i < 7; i++)
                    {
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                                i)));
                    }
                }
                //leftRight arrow
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    for (int i = 0; i < 7; i++)
                    {
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(i,
                                (int) item.position.y)));
                    }
                }
                //bomb
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.bomb)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    int xPosition = (int)item.position.x;
                    int yPosition = (int)item.position.y;
                    var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                    for (int j = -1; j <= 1; j++)
                        for (int i = -1; i <= 1; i++)
                            if (cellStackBoard[new Vector2Int(xPosition+i, yPosition+j)].HasTileStack())
                                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition+i,
                                        yPosition+j)));
                    if (cellStackBoard[new Vector2Int(xPosition+2, yPosition)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition+2,
                                yPosition)));
                    if (cellStackBoard[new Vector2Int(xPosition-2, yPosition)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition-2,
                                yPosition)));
                    if (cellStackBoard[new Vector2Int(xPosition, yPosition+2)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                                yPosition+2)));
                    if (cellStackBoard[new Vector2Int(xPosition, yPosition-2)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                                yPosition-2)));

                }
                //lightning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.lightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    int counter = 0;
                    var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                    for (var i = 0; i < 7; i++)
                    for (var j = 0; j < 7; j++)
                        if (cellStackBoard[new Vector2Int(i, j)].HasTileStack())
                        {
                            gemTile tileStack =
                                cellStackBoard[new Vector2Int(i, j)].CurrentTileStack().Top() as gemTile;
                            if (tileStack._color == item.gemColors)
                            {
                                counter++;
                                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                    new DestroyBlackBoard.DestroyData(new Vector2Int(
                                        (int) tileStack.Parent().Position().x,
                                        (int) tileStack.Parent().Position().y)));
                            }
                        }

                    if (counter <= 16)
                    {
                        for (var i = 0; i < 7; i++)
                            for (var j = 0; j < 7; j++)
                                if (cellStackBoard[new Vector2Int(i, j)].HasTileStack())
                                {
                                    gemTile tileStack =
                                        cellStackBoard[new Vector2Int(i, j)].CurrentTileStack().Top() as gemTile;
                                    if (tileStack._gemTypes != gemTypes.normal)
                                    {
                                        counter++;
                                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                            new DestroyBlackBoard.DestroyData(new Vector2Int(
                                                (int) tileStack.Parent().Position().x,
                                                (int) tileStack.Parent().Position().y)));
                                    }
                                }
                    }

                    while (counter <= 16)
                    {
                        int i = Random.Range(0, 7);
                        int j = Random.Range(0, 7);
                        if (cellStackBoard[new Vector2Int(i, j)].HasTileStack())
                        {
                            counter++;
                            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                new DestroyBlackBoard.DestroyData(new Vector2Int(
                                    i,
                                    j)));
                        }
                    }
                }
                //Arrow+Arrow
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.ArrowArrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    for (int i = 0; i < 7; i++)
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                                i)));
                    for (int i = 0; i < 7; i++)
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(i,
                                (int) item.position.y)));
                }
            }
        }
    }
}