using System;
using System.Threading.Tasks;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;

namespace Sample
{
    public interface DevkitSystemPresentationPort : PresentationPort
    {
        void PlayDevkit(string content, Action onCompleted);
    }

    public class DevkitSystemKeyType : KeyType
    {
    }

    public class DevkitSystem : BasicGameplaySystem
    {
        private DevkitBlackBoard DevkitBlackBoard;
        private DevkitSystemPresentationPort presentationPort;

        public DevkitSystem(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Start()
        {
            base.Start();
            DevkitBlackBoard = GetFrameData<DevkitBlackBoard>();
            presentationPort = gameplayController.GetPresentationPort<DevkitSystemPresentationPort>();
        }

        public override void Update(float dt)
        {
            foreach (var DevkitData in DevkitBlackBoard.requestedDevkits)
            {
                StartDevkit(DevkitData);
            }
        }

        private async void StartDevkit(DevkitBlackBoard.DevkitData DevkitData)
        {
            presentationPort.PlayDevkit(DevkitData.content, () => ApplyDevkit());
        }

        private void ApplyDevkit()
        {
           Debug.Log("DevKit Apply");
        }
    }
}