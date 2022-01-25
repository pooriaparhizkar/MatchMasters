using System;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
using UnityEngine;

namespace Sample
{
    public interface PerkHandlerSystemPresentationPort : PresentationPort
    {
        void PlayPerkHandler(bool isStart,Action onCompleted);
    }

    public class PerkHandlerSystemKeyType : KeyType
    {
    }

    public class PerkHandlerSystem : BasicGameplaySystem
    {
        private PerkHandlerSystemPresentationPort presentationPort;
        private PerkHandlerBlackBoard PerkHandlerBlackBoard;

        public PerkHandlerSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            PerkHandlerBlackBoard = GetFrameData<PerkHandlerBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<PerkHandlerSystemPresentationPort>();
        }

        public override void Update(float dt)
        {
            foreach (var PerkHandlerData in PerkHandlerBlackBoard.requestedPerkHandlers)
                StartPerkHandler(PerkHandlerData);
        }

        private void StartPerkHandler(PerkHandlerBlackBoard.PerkHandlerData PerkHandlerData)
        {

            if (PerkHandlerData.type==PerkHandlerBlackBoard.PerkHandlerType.hammer)
            {
                if (PerkHandlerData.isStart==1)
                    presentationPort.PlayPerkHandler(true,() => ApplyPerkHandler());
                else if (PerkHandlerData.isStart==0)
                    presentationPort.PlayPerkHandler(false,() => ApplyPerkHandler());
                else
                    GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                        new DestroyBlackBoard.DestroyData(PerkHandlerData.position));
            }

        }

        private void ApplyPerkHandler()
        {
            gameplayController.LevelBoard.CellStackBoard().setBoardUnlock();
            // ActionUtilites.FullyUnlock(cellStack1);
            // ActionUtilites.FullyUnlock(cellStack2);
        }
    }
}