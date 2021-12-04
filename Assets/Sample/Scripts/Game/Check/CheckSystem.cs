using System;
using System.Collections.Generic;
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
        private CheckSystemPresentationPort presentationPort;
        private CheckBlackBoard CheckBlackBoard;
        private int gemIndex;
        private int columnCounter;
        public List<gemTile> rowArray = new List<gemTile>();
        public List<gemTile> columnArray = new List<gemTile>();

        public CheckSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            CheckBlackBoard = GetFrameData<CheckBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<CheckSystemPresentationPort>();
            gemIndex = 0;
            columnCounter = 0;
        }

        private void checkColumn(int index)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (int i = 0; i < 7; i++)
            {
                if (cellStackBoard[new Vector2Int(index, i)].HasTileStack())
                    columnArray.Add(cellStackBoard[new Vector2Int(index, i)].CurrentTileStack().Top() as gemTile);
            }

            checkRow(columnArray);
            columnArray.Clear();
        }

        private void checkRow(int index)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (int i = 0; i < 7; i++)
            {
                if (cellStackBoard[new Vector2Int(i, index)].HasTileStack())
                    rowArray.Add(cellStackBoard[new Vector2Int(i, index)].CurrentTileStack().Top() as gemTile);
            }
            checkRow(rowArray);
            rowArray.Clear();
        }

        public override void Update(float dt)
        {
            // gemIndex = 0;
            // columnArray.Add(gameplayController[]);
            for (int i = 0; i < 7; i++)
            {
                checkColumn(i);
                checkRow(i);
            }


            // columnCounter = 0;
            // foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
            // {
            //     if (cellStack.HasTileStack())
            //     {
            //         gemIndex++;
            //         var tileStack = cellStack.CurrentTileStack();
            //         var gem = tileStack.Top() as gemTile;
            //         rowArray.Add(gem);
            //         if (gemIndex % 7 == 0 && gemIndex != 0)
            //         {
            //             checkRow(rowArray);
            //             rowArray.Clear();
            //         }
            //         columnArray.Add(gem);
            //         if (gemIndex%7==columnCounter)
            //         {
            //
            //             Debug.Log(columnArray.Count);
            //             if (columnArray.Count==7)
            //             {
            //                 checkRow(columnArray);
            //                 columnArray.Clear();
            //
            //                 if (columnCounter >=6)
            //                     columnCounter = 0;
            //                 else  columnCounter++;
            //             }
            //         }
            //
            //     }
            // }
        }

        private void checkRow(List<gemTile> rowArray)
        {
            List<gemTile> matched = new List<gemTile>();
            for (int i = 0; i < rowArray.Count - 2; i++)
            {
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
                                {
                                    matched.Add(rowArray[i + 6]);
                                }
                            }
                        }
                    }
                }
            }

            if (matched.Count != 0)
            {
                foreach (var VARIABLE in matched)
                {
                    // Debug.Log(VARIABLE.Parent().Position());
                    // VARIABLE.Parent().Destroy();
                    // VARIABLE.Parent().Push(new emptyTile());

                    // return;
                    // VARIABLE.Parent().Destroy();
                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                        new DestroyBlackBoard.DestroyData(new Vector2Int((int) VARIABLE.Parent().Position().x,
                            (int) VARIABLE.Parent().Position().y)));
                }

                matched.Clear();
            }
        }
    }
}