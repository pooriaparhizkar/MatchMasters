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

        private void activateBomb(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            int xPosition = (int) item.position.x;
            int yPosition = (int) item.position.y;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (int j = -1; j <= 1; j++)
            for (int i = -1; i <= 1; i++)
            {
                if (i == -1)
                    if (xPosition != 0 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                yPosition + j)));

                if (i == 1)
                    if (xPosition != 6 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                yPosition + j)));
                if (j == -1)
                    if (yPosition != 0 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                yPosition + j)));
                if (j == 1)
                    if (yPosition != 6 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                yPosition + j)));
            }

            if (xPosition < 5 && cellStackBoard[new Vector2Int(xPosition + 2, yPosition)].HasTileStack())
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + 2,
                        yPosition)));
            if (xPosition > 1 && cellStackBoard[new Vector2Int(xPosition - 2, yPosition)].HasTileStack())
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition - 2,
                        yPosition)));
            if (yPosition < 5 && cellStackBoard[new Vector2Int(xPosition, yPosition + 2)].HasTileStack())
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                        yPosition + 2)));
            if (yPosition > 1 && cellStackBoard[new Vector2Int(xPosition, yPosition - 2)].HasTileStack())
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                        yPosition - 2)));
        }

        private void activateUpDownArrow(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            for (int i = 0; i < 7; i++)
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                        i)));
        }

        private void activateLeftRightArrow(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            for (int i = 0; i < 7; i++)
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(new Vector2Int(i,
                        (int) item.position.y)));
        }

        private void activeLightning(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
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

        public override void Update(float dt)
        {
            foreach (var item in InGameBoosterActivationBlackBoard.requestedInGameBoosterActivations)
            {
                //upDown arrow
                if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateUpDownArrow(item);
                }
                //leftRight arrow
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateLeftRightArrow(item);
                }
                //bomb
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.bomb)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateBomb(item);
                }
                //lightning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.lightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activeLightning(item);
                }
                //Arrow+Arrow
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.ArrowArrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateLeftRightArrow(item);
                    activateUpDownArrow(item);
                }
                //TopDownArrow+Bomb
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.TopDownArrowBomb)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateUpDownArrow(item);
                    activateBomb(item);
                }
                //LeftRightArrow+Bomb
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.LeftRightArrowBomb)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateLeftRightArrow(item);
                    activateBomb(item);
                }
                //LeftRightArrow+Lighning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.LeftRightArrowLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateLeftRightArrow(item);
                    activeLightning(item);
                }
                //TopDownArrow+Lighning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.TopDownArrowLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateUpDownArrow(item);
                    activeLightning(item);
                }
                //Bomb+Lighning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.BombLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateBomb(item);
                    activeLightning(item);
                }
                //Bomb+Bomb
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.BombBomb)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    int xPosition = (int) item.position.x;
                    int yPosition = (int) item.position.y;
                    var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                    for (int j = -2; j <= 2; j++)
                        for (int i = -2; i <= 2; i++)
                        {
                            if (i == -1)
                                if (xPosition != 0 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));

                            if (i == 1)
                                if (xPosition != 6 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                            if (j == -1)
                                if (yPosition != 0 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                            if (j == 1)
                                if (yPosition != 6 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                            if (i == -2)
                                if (xPosition >1 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));

                            if (i == 2)
                                if (xPosition < 5 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                            if (j == -2)
                                if (yPosition >1 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                            if (j == 2)
                                if (yPosition < 5 && cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                                    .HasTileStack())
                                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                        new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                            yPosition + j)));
                        }

                    if (xPosition < 4 && cellStackBoard[new Vector2Int(xPosition + 3, yPosition)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + 3,
                                yPosition)));
                    if (xPosition > 2 && cellStackBoard[new Vector2Int(xPosition - 3, yPosition)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition - 3,
                                yPosition)));
                    if (yPosition < 4 && cellStackBoard[new Vector2Int(xPosition, yPosition + 3)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                                yPosition + 3)));
                    if (yPosition > 2 && cellStackBoard[new Vector2Int(xPosition, yPosition - 3)].HasTileStack())
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
                                yPosition - 3)));
                }
            }
        }
    }
}