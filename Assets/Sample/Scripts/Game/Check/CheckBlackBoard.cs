using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class CheckBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<CheckData> requestedChecks = new List<CheckData>();

        public void Clear()
        {
            requestedChecks.Clear();
        }

        public struct CheckData
        {
            public readonly Vector2Int pos1;
            public readonly Vector2Int pos2;

            public CheckData(Vector2Int pos1, Vector2Int pos2) : this()
            {
                this.pos1 = pos1;
                this.pos2 = pos2;
            }
        }
    }
}