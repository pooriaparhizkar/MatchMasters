using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class TurnBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<TurnData> requestedTurns = new List<TurnData>();

        public void Clear()
        {
            requestedTurns.Clear();
        }

        public struct TurnData
        {
            public readonly bool isExtraMove;
            public readonly bool isForceNextRound;


            public TurnData(bool isExtraMove,bool isForceNextRound = false) : this()
            {
                this.isExtraMove = isExtraMove;
                this.isForceNextRound = isForceNextRound;
            }
        }
    }
}