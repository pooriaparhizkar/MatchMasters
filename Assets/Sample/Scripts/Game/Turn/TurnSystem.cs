using System;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public interface TurnSystemPresentationPort : PresentationPort
    {
        void PlayTurn(bool isMyTurn, Action onCompleted);
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

        public override void Update(float dt)
        {
            foreach (var TurnData in TurnBlackBoard.requestedTurns) StartTurn(TurnData);
        }

        private void StartTurn(TurnBlackBoard.TurnData TurnData)
        {
            presentationPort.PlayTurn(TurnData.isMyTurn, () => ApplyTurn());
        }

        private void ApplyTurn()
        {
            Debug.Log("Turn added");
        }
    }
}