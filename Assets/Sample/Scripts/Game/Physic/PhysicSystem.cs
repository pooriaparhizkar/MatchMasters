using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface PhysicSystemPresentationPort : PresentationPort
    {
        void PlayPhysic(CellStack cellStack1,CellStack nextCell, BasicGameplayMainController gameplayController, Action onCompleted);
    }

    public class PhysicSystemKeyType : KeyType
    {
    }

    public class PhysicSystem : BasicGameplaySystem
    {
        private PhysicBlackBoard PhysicBlackBoard;
        private PhysicSystemPresentationPort presentationPort;

        public PhysicSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            PhysicBlackBoard = GetFrameData<PhysicBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<PhysicSystemPresentationPort>();
        }

        private CellStack findLowestTile(CellStack cellStack,CellStackBoard cellStackBoard)
        {
            if (cellStack.Position().y < 6)
            {
                CellStack nextCell = cellStackBoard[new Vector2Int(cellStack.Position().x, cellStack.Position().y + 1)];
                if (!nextCell.HasTileStack())
                {
                    return findLowestTile(nextCell, cellStackBoard);
                }
                return cellStack;
            }
            return cellStack;

        }

        public override void Update(float dt)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
                if (cellStack.HasTileStack() && cellStack.Position().y < 6)
                    if (!cellStackBoard[new Vector2Int(cellStack.Position().x, cellStack.Position().y + 1)]
                            .HasTileStack())
                        // Debug.Log((cellStack.CurrentTileStack().Top() as gemTile)._color);
                        // cellStackBoard[new Vector2Int(cellStack.Position().x, cellStack.Position().y+1)].SetCurrnetTileStack(cellStack.CurrentTileStack());
                        // cellStack.CurrentTileStack().Destroy();

                        // cellStack.Pop();

                    {
                        CellStack nextCell = findLowestTile(cellStack, cellStackBoard);
                        if (QueryUtilities.IsFullyFree(cellStack) && QueryUtilities.IsFullyFree(nextCell))
                        {

                            ActionUtilites.FullyLock<SwapSystemKeyType>(cellStack);
                            ActionUtilites.FullyLock<SwapSystemKeyType>(nextCell);
                            // Debug.Log(cellStack.Position().y);
                            // Debug.Log(nextCell.Position().y);



                            // TileStack tileStack = cellStack.CurrentTileStack();
                            // cellStack.CurrentTileStack().SetPosition(new Vector2(tileStack.Position().x,tileStack.Position().y+0.001f));




                            // ActionUtilites.SwapTileStacksOf(cellStack, nextCell);
                            //

                            presentationPort.PlayPhysic(cellStack,nextCell,
                                gameplayController, () => ApplyPhysic(cellStack,nextCell));




                        }
                    }




            // GetFrameData<SwapBlackBoard>().requestedSwaps.Add(new SwapBlackBoard.SwapData(new Vector2Int(cellStack.Position().x, cellStack.Position().y), new Vector2Int(cellStack.Position().x, cellStack.Position().y+1)));
        }

        private void ApplyPhysic(CellStack cellStack1,CellStack cellStack2)
        {
            ActionUtilites.SwapTileStacksOf(cellStack1, cellStack2);
            ActionUtilites.FullyUnlock(cellStack1);
            ActionUtilites.FullyUnlock(cellStack2);
        }
    }
}