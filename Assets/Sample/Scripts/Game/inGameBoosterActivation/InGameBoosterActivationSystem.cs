using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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
        private CellStackBoard cellStackBoard;

        public InGameBoosterActivationSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            InGameBoosterActivationBlackBoard = GetFrameData<InGameBoosterActivationBlackBoard>();
            cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
        }

        private async void detectGemAndDestroy(gemTile VARIABLE)
        {
            Vector2Int position = new Vector2Int((int) VARIABLE.Parent().Position().x,
                (int) VARIABLE.Parent().Position().y);
            if (VARIABLE._gemTypes == gemTypes.normal)
                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    new DestroyBlackBoard.DestroyData(position));
            else
            {
                // await Task.Delay(1000);
                if (VARIABLE._gemTypes == gemTypes.bomb)
                    activateBomb(new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(position,
                        InGameBoosterActivationBlackBoard.InGameBoosterType.bomb, VARIABLE._color));
                else if (VARIABLE._gemTypes == gemTypes.lightning)
                    activeLightning(
                        new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(position,
                            InGameBoosterActivationBlackBoard.InGameBoosterType.lightning, VARIABLE._color), 16);
                else if (VARIABLE._gemTypes == gemTypes.upDownarrow)
                    activateUpDownArrow(new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(position,
                        InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow, VARIABLE._color));
                else if (VARIABLE._gemTypes == gemTypes.leftRightArrow)
                    activateLeftRightArrow(new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(
                        position,
                        InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow, VARIABLE._color));
            }


            // {
            //     Debug.Log("invaaar");
            //     GetFrameData<InGameBoosterActivationBlackBoard>().requestedInGameBoosterActivations.Add(
            //         new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(new Vector2Int(
            //                 (int) VARIABLE.Parent().Position().x,
            //                 (int) VARIABLE.Parent().Position().y),
            //             VARIABLE._gemTypes == gemTypes.bomb
            //                 ? InGameBoosterActivationBlackBoard.InGameBoosterType.bomb
            //                 : VARIABLE._gemTypes == gemTypes.lightning
            //                     ? InGameBoosterActivationBlackBoard.InGameBoosterType.lightning
            //                     : VARIABLE._gemTypes == gemTypes.upDownarrow
            //                         ? InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow
            //                         : InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow,
            //             VARIABLE._color));
            // }
        }

        private void activateBomb(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                    (int) item.position.y)));
            int xPosition = (int) item.position.x;
            int yPosition = (int) item.position.y;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            (cellStackBoard[new Vector2Int(xPosition, yPosition)]
                .CurrentTileStack()
                .Top() as gemTile).setGemType(gemTypes.normal);
            for (int j = -1; j <= 1; j++)
            for (int i = -1; i <= 1; i++)
            {
                if ((xPosition + i) >= 0 && (xPosition + i) < 7 && (yPosition + j) >= 0 &&
                    (yPosition + j) < 7)
                    if (cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                        // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                        //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                        //         yPosition + j)));
                        detectGemAndDestroy(cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)]
                            .CurrentTileStack()
                            .Top() as gemTile);
            }

            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
            //         yPosition + j)));
            if ((xPosition + 2) >= 0 && (xPosition + 2) < 7 &&
                cellStackBoard[new Vector2Int(xPosition + 2, yPosition)].HasTileStack())
                detectGemAndDestroy(cellStackBoard[new Vector2Int(xPosition + 2, yPosition)].CurrentTileStack()
                    .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + 2,
            //         yPosition)));
            if ((xPosition - 2) >= 0 && (xPosition - 2) < 7 &&
                cellStackBoard[new Vector2Int(xPosition - 2, yPosition)].HasTileStack())
                detectGemAndDestroy(cellStackBoard[new Vector2Int(xPosition - 2, yPosition)].CurrentTileStack()
                    .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition - 2,
            //         yPosition)));
            if ((yPosition + 2) >= 0 && (yPosition + 2) < 7 &&
                cellStackBoard[new Vector2Int(xPosition, yPosition + 2)].HasTileStack())
                detectGemAndDestroy(cellStackBoard[new Vector2Int(xPosition, yPosition + 2)].CurrentTileStack()
                    .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
            //         yPosition + 2)));
            if ((yPosition - 2) >= 0 && (yPosition - 2) < 7 &&
                cellStackBoard[new Vector2Int(xPosition, yPosition - 2)].HasTileStack())
                detectGemAndDestroy(cellStackBoard[new Vector2Int(xPosition, yPosition - 2)].CurrentTileStack()
                    .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition,
            //         yPosition - 2)));
        }

        private void activateUpDownArrow(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            (cellStackBoard[new Vector2Int((int) item.position.x, (int) item.position.y)]
                .CurrentTileStack()
                .Top() as gemTile).setGemType(gemTypes.normal);
            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                    (int) item.position.y)));
            for (int i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int((int) item.position.x, i)].HasTileStack())
                    detectGemAndDestroy(cellStackBoard[new Vector2Int((int) item.position.x, i)].CurrentTileStack()
                        .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
            //         i)));
        }

        private void activateLeftRightArrow(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item)
        {
            (cellStackBoard[new Vector2Int((int) item.position.x, (int) item.position.y)]
                .CurrentTileStack()
                .Top() as gemTile).setGemType(gemTypes.normal);
            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                    (int) item.position.y)));
            for (int i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int(i, (int) item.position.y)].HasTileStack())
                    detectGemAndDestroy(cellStackBoard[new Vector2Int(i, (int) item.position.y)].CurrentTileStack()
                        .Top() as gemTile);
            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
            //     new DestroyBlackBoard.DestroyData(new Vector2Int(i,
            //         (int) item.position.y)));
        }

        private void activeLightning(InGameBoosterActivationBlackBoard.InGameBoosterActivationData item,
            int givenCounter)
        {
            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                    (int) item.position.y)));
            int counter = 0;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            (cellStackBoard[new Vector2Int((int) item.position.x, (int) item.position.y)]
                .CurrentTileStack()
                .Top() as gemTile).setGemType(gemTypes.normal);
            for (var i = 0; i < 7; i++)
            for (var j = 0; j < 7; j++)
                if (cellStackBoard[new Vector2Int(i, j)].HasTileStack())
                {
                    gemTile tileStack =
                        cellStackBoard[new Vector2Int(i, j)].CurrentTileStack().Top() as gemTile;
                    if (tileStack._color == item.gemColors)
                    {
                        counter++;
                        detectGemAndDestroy(tileStack);
                        // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                        //     new DestroyBlackBoard.DestroyData(new Vector2Int(
                        //         (int) tileStack.Parent().Position().x,
                        //         (int) tileStack.Parent().Position().y)));
                    }
                }

            if (counter <= givenCounter)
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
                            detectGemAndDestroy(tileStack);
                            // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            //     new DestroyBlackBoard.DestroyData(new Vector2Int(
                            //         (int) tileStack.Parent().Position().x,
                            //         (int) tileStack.Parent().Position().y)));
                        }
                    }
            }

            while (counter <= givenCounter)
            {
                int i = spawnGems.randomSeed.Next(7);
                int j = spawnGems.randomSeed.Next(7);
                if (cellStackBoard[new Vector2Int(i, j)].HasTileStack())
                {
                    counter++;
                    detectGemAndDestroy(cellStackBoard[new Vector2Int(i, j)].CurrentTileStack().Top() as gemTile);
                    // GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                    //     new DestroyBlackBoard.DestroyData(new Vector2Int(
                    //         i,
                    //         j)));
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
                    activeLightning(item, 16);
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
                    activeLightning(item, 16);
                }
                //TopDownArrow+Lighning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.TopDownArrowLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateUpDownArrow(item);
                    activeLightning(item, 16);
                }
                //Bomb+Lighning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.BombLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activateBomb(item);
                    activeLightning(item, 16);
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
                        if ((xPosition + i) >= 0 && (xPosition + i) < 7 && (yPosition + j) >= 0 && (yPosition + j) < 7)
                            if (cellStackBoard[new Vector2Int(xPosition + i, yPosition + j)].HasTileStack())
                                GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                    new DestroyBlackBoard.DestroyData(new Vector2Int(xPosition + i,
                                        yPosition + j)));

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
                //Lightning+Lightning
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.LightningLightning)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    activeLightning(item, 32);
                }
            }
        }
    }
}