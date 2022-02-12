using System;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface ScoreSystemPresentationPort : PresentationPort
    {
        void PlayScore(bool isMyScore, Action onCompleted);
    }

    public class ScoreSystemKeyType : KeyType
    {
    }

    public class ScoreSystem : BasicGameplaySystem
    {
        private ScoreBlackBoard ScoreBlackBoard;
        private ScoreSystemPresentationPort presentationPort;
        private bool isLock;

        public ScoreSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            ScoreBlackBoard = GetFrameData<ScoreBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<ScoreSystemPresentationPort>();
            isLock = false;
        }

        public override void Update(float dt)
        {
            foreach (var ScoreData in ScoreBlackBoard.requestedScores)
            {
                StartScore(ScoreData);
            }
        }

        private async void StartScore(ScoreBlackBoard.ScoreData ScoreData)
        {
            if (!isLock)
            {
                isLock = true;
                presentationPort.PlayScore(ScoreData.isMyScore, () => ApplyScore());
            }
            else
            {
                await Task.Delay(100);
                StartScore(ScoreData);
            }
        }

        private void ApplyScore()
        {
            Debug.Log("Score added");
            isLock = false;
        }
    }
}