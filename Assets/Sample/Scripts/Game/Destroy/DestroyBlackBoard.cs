using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class DestroyBlackBoard : MonoBehaviour,BlackBoardData
    {
        public readonly List<DestroyData> requestedDestroys = new List<DestroyData>();

        public void Clear()
        {
            requestedDestroys.Clear();
        }

        public struct DestroyData
        {
            public readonly Vector2Int pos1;

            public DestroyData(Vector2Int pos1) : this()
            {
                this.pos1 = pos1;
            }
        }
    }
}