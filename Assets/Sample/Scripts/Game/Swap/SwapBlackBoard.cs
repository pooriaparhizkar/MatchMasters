using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class SwapBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<SwapData> requestedSwaps = new List<SwapData>();

        public void Clear()
        {
            requestedSwaps.Clear();
        }

        public struct SwapData
        {
            public readonly Vector2Int pos1;
            public readonly Vector2Int pos2;
            public readonly bool isDrag;

            public SwapData(Vector2Int pos1, Vector2Int pos2,bool isDrag=false) : this()
            {
                this.pos1 = pos1;
                this.pos2 = pos2;
                this.isDrag = isDrag;
            }
        }
    }
}