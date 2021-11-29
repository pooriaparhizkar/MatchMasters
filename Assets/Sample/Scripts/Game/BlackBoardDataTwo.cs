using Medrick.Match3CoreSystem.Game;
using System.Collections.Generic;

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