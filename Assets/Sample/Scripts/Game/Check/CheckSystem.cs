using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
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

        private List<gemTile> iterateColumn(int index)
        {
            List<gemTile> result;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (var i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int(index, i)].HasTileStack())
                    columnArray.Add(cellStackBoard[new Vector2Int(index, i)].CurrentTileStack().Top() as gemTile);
                else break;

            result = checkRow(columnArray);
            columnArray.Clear();
            return result;
        }

        private List<gemTile> iterateRow(int index)
        {
            List<gemTile> result;
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            for (var i = 0; i < 7; i++)
                if (cellStackBoard[new Vector2Int(i, index)].HasTileStack())
                    rowArray.Add(cellStackBoard[new Vector2Int(i, index)].CurrentTileStack().Top() as gemTile);
                else break;
            result = checkRow(rowArray);
            rowArray.Clear();
            return result;
        }

        private int getPositionX(gemTile gemTile)
        {
            return (int) gemTile.Parent().Position().x;
        }

        private int getPositionY(gemTile gemTile)
        {
            return (int) gemTile.Parent().Position().y;
        }


        static int mostFrequent(int[] arr, int n)
        {
            // Sort the array
            Array.Sort(arr);

            // find the max frequency using
            // linear traversal
            int max_count = 1, res = arr[0];
            int curr_count = 1;

            for (int i = 1; i < n; i++)
            {
                if (arr[i] == arr[i - 1])
                    curr_count++;
                else
                {
                    if (curr_count > max_count)
                    {
                        max_count = curr_count;
                        res = arr[i - 1];
                    }

                    curr_count = 1;
                }
            }

            // If last element is most frequent
            if (curr_count > max_count)
            {
                max_count = curr_count;
                res = arr[n - 1];
            }

            return res;
        }

        private int howManyGemInRow(List<gemTile> matched)
        {
            int[] temp = new int[matched.Count];
            int counter = 0;
            foreach (var VARIABLE in matched)
            {
                temp[counter] = getPositionY(VARIABLE);
                counter++;
            }

            int n = temp.Length;
            int freq = mostFrequent(temp, n);
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (temp[i] == freq)
                {
                    count++;
                }
            }

            return count;
        }

        private int howManyGemInColomn(List<gemTile> matched)
        {
            int[] temp = new int[matched.Count];
            int counter = 0;
            foreach (var VARIABLE in matched)
            {
                temp[counter] = getPositionX(VARIABLE);
                counter++;
            }

            int n = temp.Length;
            int freq = mostFrequent(temp, n);
            int count = 0;
            for (int i = 0; i < n; i++)
            {
                if (temp[i] == freq)
                {
                    count++;
                }
            }

            return count;
        }

        private async void detectTypeAndCallDestroy(List<gemTile> matched)
        {
            if (matched.Count != 0)
            {
                matched = matched.Distinct().ToList();
                gemTile lastGemTileMove = matched[0];
                foreach (var VARIABLE in matched)
                {
                    if (DateTime.Compare(lastGemTileMove._utcTime, VARIABLE._utcTime) <= 0)
                        lastGemTileMove = VARIABLE;
                }

                foreach (var VARIABLE in matched)
                {
                    //Should create booster
                    if (VARIABLE._gemTypes==gemTypes.normal && matched.Count > 3 && VARIABLE.Parent().Position() == lastGemTileMove.Parent().Position())
                    {
                        GetFrameData<TurnBlackBoard>().requestedTurns.Add(new TurnBlackBoard.TurnData(true));
                        //Arrow :
                        if (matched.Count == 4 )
                        {
                            //upDown
                            if (matched[0].Parent().Position().x == matched[1].Parent().Position().x)
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.upDownarrow,
                                        lastGemTileMove._color));
                            //leftRight
                            else
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.leftRightArrow,
                                        lastGemTileMove._color));
                        }

                        else if (matched.Count == 5 )
                        {
                            //lightning
                            if (howManyGemInRow(matched) == 5 || howManyGemInColomn(matched) == 5)
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.lightning,
                                        lastGemTileMove._color));
                            //bomb:
                            else
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.bomb,
                                        lastGemTileMove._color));
                        }
                        else if (matched.Count > 5 )
                        {
                            //lightning
                            if (howManyGemInRow(matched) >= 5 || howManyGemInColomn(matched) >= 5)
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.lightning,
                                        lastGemTileMove._color));
                            //bomb ( aghooj situation )
                            else if (howManyGemInRow(matched) == 4 && howManyGemInColomn(matched) == 3)
                                GetFrameData<InGameBoosterInstanceBlackBoard>().requestedInGameBoosterInstances.Add(
                                    new InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData(
                                        lastGemTileMove.Parent().Position(),
                                        InGameBoosterInstanceBlackBoard.InGameBoosterType.bomb,
                                        lastGemTileMove._color));
                        }
                    }

                    //Normal :
                    else
                    {
                        if (VARIABLE._gemTypes == gemTypes.normal)
                        {
                            GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                                new DestroyBlackBoard.DestroyData(new Vector2Int((int) VARIABLE.Parent().Position().x,
                                    (int) VARIABLE.Parent().Position().y)));
                        }
                        else
                            GetFrameData<InGameBoosterActivationBlackBoard>().requestedInGameBoosterActivations.Add(
                                new InGameBoosterActivationBlackBoard.InGameBoosterActivationData(new Vector2Int(
                                        (int) VARIABLE.Parent().Position().x,
                                        (int) VARIABLE.Parent().Position().y),
                                    VARIABLE._gemTypes == gemTypes.bomb
                                        ? InGameBoosterActivationBlackBoard.InGameBoosterType.bomb
                                        : VARIABLE._gemTypes == gemTypes.lightning
                                            ? InGameBoosterActivationBlackBoard.InGameBoosterType.lightning
                                            : VARIABLE._gemTypes == gemTypes.upDownarrow
                                                ? InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow
                                                : InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow,
                                    VARIABLE._color));
                    }
                }

                Debug.Log(matched);
                matched.Clear();
            }
        }



        public override void Update(float dt)
        {
            if (!gameplayController.LevelBoard.CellStackBoard()
                .isBoardLock()) //in destroy system and physic system lock and unlock
            {
                if (ActionUtilites.isMapStable(gameplayController))
                {
                    List<gemTile> matched = new List<gemTile>();
                    for (var i = 0; i < 7; i++)
                    {
                        matched.AddRange(iterateColumn(i));
                        matched.AddRange(iterateRow(i));
                    }

                    detectTypeAndCallDestroy(matched);
                    matched.Clear();
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
                if (iterateColumn(i).Count != 0)
                    isMatched1 = true;
                if (iterateRow(i).Count != 0)
                    isMatched2 = true;
            }

            if (!isMatched1 && !isMatched2)
            {
                await Task.Delay(100);
                GetFrameData<SwapBlackBoard>().requestedSwaps.Add(
                    new SwapBlackBoard.SwapData(swapData.cell1.Position(), swapData.cell2.Position()));
            }
            else
            {
                GetFrameData<TurnBlackBoard>().requestedTurns.Add(new TurnBlackBoard.TurnData(false));
            }
            //GetFrameData<CheckBlackBoard>().Clear();
        }


        private List<gemTile> checkRow(List<gemTile> rowArray)
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


            return matched;
        }
    }
}