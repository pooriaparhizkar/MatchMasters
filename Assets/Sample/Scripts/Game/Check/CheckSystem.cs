using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface CheckSystemPresentationPort : PresentationPort
    {
        void PlayCheck(CellStack cellStack1, CellStack cellStack2, Action onCompleted);
    }

    public class CheckSystemKeyType : KeyType
    {
    }

    public class CheckSystem : BasicGameplaySystem
    {
        private CheckBlackBoard CheckBlackBoard;
        public List<gemTile> columnArray = new List<gemTile>();
        private int columnCounter;
        private int gemIndex;
        private CheckSystemPresentationPort checkSystemPresentationPort;
        private SwapSystemPresentationPort swapSystemPresentationPort;
        public List<gemTile> rowArray = new List<gemTile>();
        private bool isMatched;

        public CheckSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            isMatched = false;
            CheckBlackBoard = GetFrameData<CheckBlackBoard>();
            checkSystemPresentationPort = gameplayController.GetPresentationPort<CheckSystemPresentationPort>();
            swapSystemPresentationPort = gameplayController.GetPresentationPort<SwapSystemPresentationPort>();
            gemIndex = 0;
            columnCounter = 0;
        }

        private bool iterateColumn(int index)
        {
            bool result;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (var i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int(index, i)].HasTileStack())
                    columnArray.Add(cellStackBoard[new Vector2Int(index, i)].CurrentTileStack().Top() as gemTile);
                else break;

            result = checkRow(columnArray);
            columnArray.Clear();
            return result;
        }

        private bool iterateRow(int index)
        {
            bool result;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (var i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int(i, index)].HasTileStack())
                    rowArray.Add(cellStackBoard[new Vector2Int(i, index)].CurrentTileStack().Top() as gemTile);
                else break;
            result = checkRow(rowArray);
            rowArray.Clear();
            return result;
        }

        public override void Update(float dt)
        {
            if (!gameplayController.LevelBoard.CellStackBoard().isBoardLock()) //in destroy system and physic system lock and unlock
            {
                for (var i = 0; i < 7; i++)
                {
                    iterateColumn(i);
                    iterateRow(i);
                }
            }

            foreach (var swapData in CheckBlackBoard.requestedChecks)
                checkAfterSwap(swapData);
        }


        public async void checkAfterSwap(CheckBlackBoard.CheckData swapData)
        {
            // Debug.Log(swapData);
            bool isMatched1 = false;
            bool isMatched2 = false;
            for (var i = 0; i < 7; i++)
            {
                if (iterateColumn(i))
                    isMatched1 = true;
                if (iterateRow(i))
                    isMatched2 = true;
            }

            if (!isMatched1 && !isMatched2)
            {
                Debug.Log(swapData.cell1);
                Debug.Log(swapData.cell2);
                await Task.Delay(100);
                GetFrameData<SwapBlackBoard>().requestedSwaps.Add(
                    new SwapBlackBoard.SwapData(swapData.cell1.Position(), swapData.cell2.Position()));
            }
            //GetFrameData<CheckBlackBoard>().Clear();
        }


        private bool checkRow(List<gemTile> rowArray)
        {
            var matched = new List<gemTile>();
            for (var i = 0; i < rowArray.Count - 2; i++)
                if (rowArray[i]._color == rowArray[i + 1]._color && rowArray[i + 1]._color == rowArray[i + 2]._color)
                {
                    matched.Add(rowArray[i]);
                    matched.Add(rowArray[i + 1]);
                    matched.Add(rowArray[i + 2]);
                    if (i < rowArray.Count - 3 && rowArray[i + 2]._color == rowArray[i + 3]._color)
                    {
                        matched.Add(rowArray[i + 3]);
                        if (i < rowArray.Count - 4 && rowArray[i + 3]._color == rowArray[i + 4]._color)
                        {
                            matched.Add(rowArray[i + 4]);
                            if (i < rowArray.Count - 5 && rowArray[i + 4]._color == rowArray[i + 5]._color)
                            {
                                matched.Add(rowArray[i + 5]);
                                if (i < rowArray.Count - 6 && rowArray[i + 5]._color == rowArray[i + 6]._color)
                                    matched.Add(rowArray[i + 6]);
                            }
                        }
                    }
                }

            if (matched.Count != 0)
            {
                foreach (var VARIABLE in matched)
                    // Debug.Log(VARIABLE.Parent().Position());
                    // VARIABLE.Parent().Destroy();
                    // VARIABLE.Parent().Push(new emptyTile());

                    // return;
                    // VARIABLE.Parent().Destroy();
                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                        new DestroyBlackBoard.DestroyData(new Vector2Int((int) VARIABLE.Parent().Position().x,
                            (int) VARIABLE.Parent().Position().y)));

                matched.Clear();
                return true;
            }

            return false;
        }
    }
}