using System;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface PhysicSystemPresentationPort : PresentationPort
    {
        void PlayPhysic(CellStack cellStack1, Action onCompleted);
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
                        ActionUtilites.SwapTileStacksOf(cellStack,
                            cellStackBoard[new Vector2Int(cellStack.Position().x, cellStack.Position().y + 1)]);
                        // presentationPort.PlayPhysic(cellStack, () => ApplyDestroy(cellStack, cellStackBoard));

                    // GetFrameData<SwapBlackBoard>().requestedSwaps.Add(new SwapBlackBoard.SwapData(new Vector2Int(cellStack.Position().x, cellStack.Position().y), new Vector2Int(cellStack.Position().x, cellStack.Position().y+1)));
        }

        private void ApplyDestroy(CellStack cellStack, CellStackBoard cellStackBoard)
        {
            ActionUtilites.SwapTileStacksOf(cellStack,
                cellStackBoard[new Vector2Int(cellStack.Position().x, cellStack.Position().y + 1)]);
        }
    }
}