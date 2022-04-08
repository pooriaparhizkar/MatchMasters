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
    public interface RoundSystemPresentationPort : PresentationPort
    {
        void PlayRoundSystem(int whichRound, Action onCompleted);
    }
    public interface FinishGameSystemPresentationPort : PresentationPort
    {
        void PlayFinishGameSystem(int myScore,int hisScore, Action onCompleted);
    }

    public class TurnSystemKeyType : KeyType
    {
    }

    public class TurnSystem : BasicGameplaySystem
    {
        private TurnSystemPresentationPort presentationPort;
        private RoundSystemPresentationPort presentationPort2;
        private FinishGameSystemPresentationPort presentationPort3;
        private TurnBlackBoard TurnBlackBoard;
        private int localRound;

        public TurnSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            TurnBlackBoard = GetFrameData<TurnBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<TurnSystemPresentationPort>(); // Turn Handler
            presentationPort2 = gameplayController.GetPresentationPort<RoundSystemPresentationPort>(); // Round Handler
            presentationPort3= gameplayController.GetPresentationPort<FinishGameSystemPresentationPort>(); // Result page Handler
            localRound = 1;
            presentationPort2.PlayRoundSystem(localRound,()=> Debug.Log("Round added"));
        }

        private void applyUpdateTurn()
        {

        }

        public override void Update(float dt)
        {
            foreach (var TurnData in TurnBlackBoard.requestedTurns)
            {
                Debug.Log(turnHandler.getTurn());
                if (!TurnData.isForceNextRound)
                {
                    if (!TurnData.isExtraMove)
                        turnHandler.setRemainMove(turnHandler.getRemainMove() - 1);
                    else
                    {
                        Debug.Log("Extraaaaaaaaaa Move");
                        Debug.Log(turnHandler.getRemainMove());
                        if (turnHandler.getRemainMove() == 2 )
                            turnHandler.setRemainMove(3);
                        if (turnHandler.getRemainMove() == 4 )
                            turnHandler.setRemainMove(1);
                        Debug.Log(turnHandler.getRemainMove());
                    }
                }
                else
                {
                    if (turnHandler.getRemainMove() ==3 || turnHandler.getRemainMove() ==4)
                        turnHandler.setRemainMove(2);
                    else if
                        (turnHandler.getRemainMove() ==2 || turnHandler.getRemainMove() ==1)
                        turnHandler.setRemainMove(0);
                    }
                Debug.Log(turnHandler.getTurn());
                // StartTurn(turnHandler.isMyTurn(), turnHandler.getRemainMove());
                if  (turnHandler.getRemainMove() > 2)
                    turnHandler.setTurn(1);
                if (turnHandler.getRemainMove() == 2)
                    turnHandler.setTurn(2);
                if(turnHandler.getRemainMove()<2 && turnHandler.getRemainMove()>0)
                    turnHandler.setTurn(2);
                if (turnHandler.getRemainMove() == 0)
                {
                    turnHandler.setTurn(1);
                    turnHandler.resetRemainMove();
                    localRound++;
                    if (localRound<=4)
                        presentationPort2.PlayRoundSystem(localRound,()=> Debug.Log("Round added"));
                    else
                    {
                        presentationPort3.PlayFinishGameSystem(turnHandler.getMyScore(),turnHandler.getHisScore(),(() =>  Debug.Log("Game Finished")));
                    }

                        // StartTurn(turnHandler.isMyTurn(), turnHandler.getRemainMove());
                }
                Debug.Log(turnHandler.getTurn());
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