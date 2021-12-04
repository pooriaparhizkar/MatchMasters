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
        private int counter;
        public List<gemTile> rowArray = new List<gemTile>();

        public CheckSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            CheckBlackBoard = GetFrameData<CheckBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<CheckSystemPresentationPort>();
            counter = 0;
        }

        public override void Update(float dt)
        {
            counter = 0;
            foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
                {
                    if (cellStack.HasTileStack())
                    {
                        counter++;
                        var tileStack = cellStack.CurrentTileStack();
                        var gem = tileStack.Top() as gemTile;
                        rowArray.Add(gem);
                        if (counter % 7 == 0 && counter!=0)
                        {
                            checkRow(rowArray);
                            rowArray.Clear();
                        }
                    }
                }

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
                    Debug.Log(VARIABLE.Parent().Position());
                    VARIABLE.Parent();
                    VARIABLE.Parent().Push(new emptyTile());
                    VARIABLE.Parent().Top();
                    return;
                    // VARIABLE.Parent().Destroy();
                }
            }
        }
    }
    public class emptyTile : Tile
    {
    }

}