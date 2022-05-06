using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game.Core;
using Script.CoreGame;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemBoosterProgressBarPresentationAdapter : MonoBehaviour, BoosterProgressBarSystemPresentationPort
    {
        public Text myBoosterProgressBarText;
        public Text herBoosterProgressBarText;
        public Slider myBoosterProgressBarSlider;
        public Slider herBoosterProgressBarSlider;

        public async void PlayBoosterProgressBar(bool isMyBoosterProgressBar, Action onCompleted)
        {
            if (isMyBoosterProgressBar)
            {
                Debug.Log("**********************umad tu play booster progress bar1111111111111");
                int newBoosterProgressBar = Int32.Parse(myBoosterProgressBarText.text.Split('/')[0]) + 1;
                if (newBoosterProgressBar<=5)
                {
                    Debug.Log("********************umad tu play booster progress bar2222222222222222");
                    if (newBoosterProgressBar==5)
                    {
                        Debug.Log("****************umad tu play booster progress bar5555555555555555");
                        // myBoosterProgressBarText.text = "Active";
                    }
                    else
                    {
                        Debug.Log("**************umad tu play booster progress bar444444444444");
                        myBoosterProgressBarText.text = newBoosterProgressBar.ToString() + "/5";
                    }
                    myBoosterProgressBarSlider.value = (float) newBoosterProgressBar / 5;
                    turnHandler.setMyBoosterProgressBar(newBoosterProgressBar);
                }

            }
            else
            {
                Debug.Log("umad tu play booster progress bar1111111111111");
                int newBoosterProgressBar = Int32.Parse(herBoosterProgressBarText.text.Split('/')[0]) + 1;
                if (newBoosterProgressBar<=5)
                {
                    Debug.Log("umad tu play booster progress bar2222222222222222");
                    if (newBoosterProgressBar==5)
                    {
                        // herBoosterProgressBarText.text = "Active";
                        Debug.Log("umad tu play booster progress bar5555555555555555");
                    }
                    else
                    {
                        Debug.Log("umad tu play booster progress bar444444444444");
                        herBoosterProgressBarText.text = newBoosterProgressBar.ToString() + "/5";
                    }
                    herBoosterProgressBarSlider.value = (float) newBoosterProgressBar / 5;
                    turnHandler.setHisBoosterProgressBar(newBoosterProgressBar);
                }

            }

            await Task.Delay(50);

            onCompleted.Invoke();
        }
    }
}