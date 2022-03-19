using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class BoosterProgressBarBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<BoosterProgressBarData> requestedBoosterProgressBars = new List<BoosterProgressBarData>();

        public void Clear()
        {
            requestedBoosterProgressBars.Clear();
        }

        public struct BoosterProgressBarData
        {
            public readonly bool isMyBoosterProgressBar;

            public BoosterProgressBarData(bool isMyBoosterProgressBar) : this()
            {
                this.isMyBoosterProgressBar = isMyBoosterProgressBar;
            }
        }
    }
}