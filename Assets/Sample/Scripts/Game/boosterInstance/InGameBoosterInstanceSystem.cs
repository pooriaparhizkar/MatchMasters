using System;
using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Sample
{
    public interface InGameBoosterInstanceSystemPresentationPort : PresentationPort
    {
        void PlayInGameBoosterInstance(InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData inGameBoosterInstanceData, BasicGameplayMainController gameplayController,
            Action onCompleted);
    }

    public class InGameBoosterInstanceSystemKeyType : KeyType
    {
    }

    public class InGameBoosterInstanceSystem : BasicGameplaySystem
    {
        private InGameBoosterInstanceSystemPresentationPort presentationPort;
        private InGameBoosterInstanceBlackBoard InGameBoosterInstanceBlackBoard;

        public InGameBoosterInstanceSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            InGameBoosterInstanceBlackBoard = GetFrameData<InGameBoosterInstanceBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<InGameBoosterInstanceSystemPresentationPort>();
        }


        public override void Update(float dt)
        {
            foreach (var item in InGameBoosterInstanceBlackBoard.requestedInGameBoosterInstances)
            {
                ActionUtilites.FullyLock<InGameBoosterInstanceSystemKeyType>((gameplayController.LevelBoard.CellStackBoard())[
                    (int) item.position.x, (int) item.position.y]);
                presentationPort.PlayInGameBoosterInstance(item, gameplayController,
                    () => ApplyInGameBoosterInstance(item));
            }
        }

        private void ApplyInGameBoosterInstance(
            InGameBoosterInstanceBlackBoard.InGameBoosterInstanceData inGameBoosterInstanceData)
        {
            Debug.Log(inGameBoosterInstanceData);

            ActionUtilites.FullyUnlock((gameplayController.LevelBoard.CellStackBoard())[
                (int) inGameBoosterInstanceData.position.x, (int) inGameBoosterInstanceData.position.y]);
            // var tileStackFactory = new MainTileStackFactory();
            // var tileStack = tileStackFactory.Create();
            // cellStack.SetCurrnetTileStack(tileStack);
            // tileStack.SetPosition(cellStack.Position());
            // tileStack.Push(new gemTile( (gemColors)Random.Range(0,6)));
        }
    }
}