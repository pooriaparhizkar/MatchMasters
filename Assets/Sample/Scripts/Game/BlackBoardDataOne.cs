using Medrick.Match3CoreSystem.Game;
using System.Collections.Generic;
using UnityEngine;

namespace Sample
{
    public class BlackBoardDataOne : BlackBoardData
    {
        public struct SwapData 
        { 
            public readonly Vector2Int pos1; 
            public readonly Vector2Int pos2; 

            public SwapData(Vector2Int pos1, Vector2Int pos2) : this() 
            { 
                this.pos1 = pos1; 
                this.pos2 = pos2; 
            } 
        };

        public readonly List<SwapData> requestedSwaps = new List<SwapData>();

        public void Clear()
        {
            requestedSwaps.Clear();
        }
    }
}