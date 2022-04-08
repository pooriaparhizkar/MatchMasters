using System;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Sample;

namespace Script.CoreGame
{
    public class timerHandler : spawnGems
    {
        public Slider timerSlider;

        private async void timerLoop()
        {
            timerSlider.value -= 0.0025f;
            await Task.Delay(50);
            if (timerSlider.value==0) //timerDone
            {
                gameplayController.FrameBasedBlackBoard.GetComponent<TurnBlackBoard>().requestedTurns
                    .Add(new TurnBlackBoard.TurnData(false, true));
                timerSlider.value = 1;
            }
           timerLoop();

        }

        private void Start()
        {
            timerLoop();
        }
    }
}