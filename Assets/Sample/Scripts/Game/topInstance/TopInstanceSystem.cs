using System;
using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample
{
    public interface TopInstanceSystemPresentationPort : PresentationPort
    {
        void PlayTopInstance(CellStack cellStack1,BasicGameplayMainController gameplayController, Action onCompleted);
    }

    public class TopInstanceSystemKeyType : KeyType
    {
    }

    public class TopInstanceSystem : BasicGameplaySystem
    {
        public List<gemTile> columnArray = new List<gemTile>();
        private int columnCounter;
        private int gemIndex;
        private TopInstanceSystemPresentationPort presentationPort;
        public List<gemTile> rowArray = new List<gemTile>();
        private TopInstanceBlackBoard TopInstanceBlackBoard;

        public TopInstanceSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            TopInstanceBlackBoard = GetFrameData<TopInstanceBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<TopInstanceSystemPresentationPort>();
            gemIndex = 0;
            columnCounter = 0;
        }


        public override void Update(float dt)
        {
            var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
            foreach (var cellStack in gameplayController.LevelBoard.leftToRightTopDownCellStackArray)
            {
                if (cellStack.Position().y == 0 && !cellStack.HasTileStack() && QueryUtilities.IsFullyFree(cellStack))
                {
                    ActionUtilites.FullyLock<SwapSystemKeyType>(cellStack);
                    presentationPort.PlayTopInstance(cellStack, gameplayController ,() => ApplyDestroy(cellStack));


                }
            }
        }

        private void ApplyDestroy(CellStack cellStack)
        {
            Debug.Log(cellStack);
            ActionUtilites.FullyUnlock(cellStack);
            // var tileStackFactory = new MainTileStackFactory();
            // var tileStack = tileStackFactory.Create();
            // cellStack.SetCurrnetTileStack(tileStack);
            // tileStack.SetPosition(cellStack.Position());
            // tileStack.Push(new gemTile( (gemColors)Random.Range(0,6)));
        }
    }
}