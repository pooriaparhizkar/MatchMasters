using System;
using System.Threading.Tasks;
using DG.Tweening;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Sample
{
    public class SystemScorePresentationAdapter : MonoBehaviour, ScoreSystemPresentationPort
    {
        public Text myScore;
        public Text herScore;

        public async void PlayScore(bool isMyScore, Action onCompleted)
        {
            if (isMyScore)
                myScore.text = (Int32.Parse(myScore.text) + 1).ToString();
            else
                herScore.text = (Int32.Parse(herScore.text) + 1).ToString();

            await Task.Delay(50);

            onCompleted.Invoke();
        }
    }
}