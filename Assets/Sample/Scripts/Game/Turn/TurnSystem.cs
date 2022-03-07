using System;
using Medrick.Match3CoreSystem.Game;
using Script.CoreGame;
using UnityEngine;

namespace Sample
{
    public interface TurnSystemPresentationPort : PresentationPort
    {
        void PlayTurn(bool isMyTurn, int remainMove, Action onCompleted);
    }

    public class TurnSystemKeyType : KeyType
    {
    }

    public class TurnSystem : BasicGameplaySystem
    {
        private TurnSystemPresentationPort presentationPort;
        private TurnBlackBoard TurnBlackBoard;

        public TurnSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            TurnBlackBoard = GetFrameData<TurnBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<TurnSystemPresentationPort>();
        }

        private void applyUpdateTurn()
        {

        }

        public override void Update(float dt)
        {
            foreach (var TurnData in TurnBlackBoard.requestedTurns)
            {

                turnHandler.setRemainMove(turnHandler.getRemainMove() - 1);
                // StartTurn(turnHandler.isMyTurn(), turnHandler.getRemainMove());
                if (turnHandler.getRemainMove() == 2)
                    turnHandler.setTurn(2);
                if (turnHandler.getRemainMove() == 0)
                {
                    turnHandler.setTurn(1);
                    turnHandler.resetRemainMove();
                    // StartTurn(turnHandler.isMyTurn(), turnHandler.getRemainMove());
                }
                StartTurn(turnHandler.isMyTurn(), turnHandler.getRemainMove());

            }
        }

        private void StartTurn(bool isMyTurn, int remainMove)
        {
            presentationPort.PlayTurn(isMyTurn, remainMove, () => ApplyTurn());
        }

        private void ApplyTurn()
        {
            Debug.Log("Turn added");
        }
    }
}