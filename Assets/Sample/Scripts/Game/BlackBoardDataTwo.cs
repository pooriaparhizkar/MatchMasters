using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;

namespace Sample
{
    public class BlackBoardDataTwo : BlackBoardData
    {
        public readonly List<int> appliedSwaps = new List<int>();

        public void Clear()
        {
            appliedSwaps.Clear();
        }
    }
}