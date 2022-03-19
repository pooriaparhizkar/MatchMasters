using System;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface BoosterProgressBarSystemPresentationPort : PresentationPort
    {
        void PlayBoosterProgressBar(bool isMyBoosterProgressBar, Action onCompleted);
    }

    public class BoosterProgressBarSystemKeyType : KeyType
    {
    }

    public class BoosterProgressBarSystem : BasicGameplaySystem
    {
        private BoosterProgressBarBlackBoard BoosterProgressBarBlackBoard;
        private BoosterProgressBarSystemPresentationPort presentationPort;
        private bool isLock;

        public BoosterProgressBarSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            BoosterProgressBarBlackBoard = GetFrameData<BoosterProgressBarBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<BoosterProgressBarSystemPresentationPort>();
            isLock = false;
        }

        public override void Update(float dt)
        {
            foreach (var BoosterProgressBarData in BoosterProgressBarBlackBoard.requestedBoosterProgressBars)
            {
                StartBoosterProgressBar(BoosterProgressBarData);
            }
        }

        private async void StartBoosterProgressBar(BoosterProgressBarBlackBoard.BoosterProgressBarData BoosterProgressBarData)
        {
            if (!isLock)
            {
                isLock = true;
                presentationPort.PlayBoosterProgressBar(BoosterProgressBarData.isMyBoosterProgressBar, () => ApplyBoosterProgressBar());
            }
            else
            {
                await Task.Delay(50);
                StartBoosterProgressBar(BoosterProgressBarData);
            }
        }

        private void ApplyBoosterProgressBar()
        {
            isLock = false;
        }
    }
}