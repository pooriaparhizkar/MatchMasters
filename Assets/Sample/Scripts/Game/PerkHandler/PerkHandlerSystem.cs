using System;
using System.Linq;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
using UnityEngine;

namespace Sample
{
    public interface PerkHandlerSystemPresentationPort : PresentationPort
    {
        void PlayPerkHandler(bool isStart,bool isMyPerk,PerkHandlerBlackBoard.PerkHandlerType type, Action onCompleted);
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
            foreach (var PerkHandlerData in PerkHandlerBlackBoard.requestedPerkHandlers.ToList())
                StartPerkHandler(PerkHandlerData);
        }

        private void StartPerkHandler(PerkHandlerBlackBoard.PerkHandlerData PerkHandlerData)
        {
            if (PerkHandlerData.type == PerkHandlerBlackBoard.PerkHandlerType.hammer)
            {
                if (PerkHandlerData.isStart == 1)
                    presentationPort.PlayPerkHandler(true,PerkHandlerData.isMyPerk,PerkHandlerData.type, () => ApplyPerkHandler());
                else if (PerkHandlerData.isStart == 0)
                    presentationPort.PlayPerkHandler(false,PerkHandlerData.isMyPerk,PerkHandlerData.type, () => ApplyPerkHandler());
                else
                {
                    var cellStackBoard = gameplayController.LevelBoard.CellStackBoard();
                    gemTile VARIABLE =
                        (cellStackBoard[
                                new Vector2Int((int) PerkHandlerData.position.x, (int) PerkHandlerData.position.y)]
                            .CurrentTileStack()
                            .Top() as gemTile);
                    if (VARIABLE._gemTypes == gemTypes.normal)
                        GetFrameData<DestroyBlackBoard>().requestedDestroys.Add(
                            new DestroyBlackBoard.DestroyData(new Vector2Int((int) VARIABLE.Parent().Position().x,
                                (int) VARIABLE.Parent().Position().y)));
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
            else if (PerkHandlerData.type == PerkHandlerBlackBoard.PerkHandlerType.shuffle)
            {
                presentationPort.PlayPerkHandler(true,PerkHandlerData.isMyPerk,PerkHandlerData.type, () => ApplyPerkHandler());
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