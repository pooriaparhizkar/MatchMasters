using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class TopInstanceBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<TopInstanceData> requestedTopInstances = new List<TopInstanceData>();

        public void Clear()
        {
            requestedTopInstances.Clear();
        }

        public struct TopInstanceData
        {
            public readonly Vector2Int pos1;
            public readonly Vector2Int pos2;

            public TopInstanceData(Vector2Int pos1, Vector2Int pos2) : this()
            {
                this.pos1 = pos1;
                this.pos2 = pos2;
            }
        }
    }
}