using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class ScoreBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<ScoreData> requestedScores = new List<ScoreData>();

        public void Clear()
        {
            requestedScores.Clear();
        }

        public struct ScoreData
        {
            public readonly bool isMyScore;

            public ScoreData(bool isMyScore) : this()
            {
                this.isMyScore = isMyScore;
            }
        }
    }
}