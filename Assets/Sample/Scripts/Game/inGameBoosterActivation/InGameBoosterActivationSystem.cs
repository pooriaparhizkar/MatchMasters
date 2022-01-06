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


        public override void Update(float dt)
        {
            foreach (var item in InGameBoosterActivationBlackBoard.requestedInGameBoosterActivations)
            {
                if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.upDownarrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    for (int i = 0; i < 7; i++)
                    {
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int((int) item.position.x,
                                i)));
                    }
                }
                else if (item.type == InGameBoosterActivationBlackBoard.InGameBoosterType.leftRightArrow)
                {
                    ActionUtilites.FullyUnlock(
                        (gameplayController.LevelBoard.CellStackBoard())[
                            (int) item.position.x, (int) item.position.y]);
                    for (int i = 0; i < 7; i++)
                    {
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int(i,
                                (int) item.position.y)));
                    }
                }
            }
        }

    }
}