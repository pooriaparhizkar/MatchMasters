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
            public readonly bool isMyTurn;

            public TurnData(bool isMyTurn) : this()
            {
                this.isMyTurn = isMyTurn;
            }
        }
    }
}