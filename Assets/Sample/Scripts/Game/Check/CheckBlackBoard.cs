using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;
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
            public readonly CellStack cell1;
            public readonly CellStack cell2;

            public CheckData(CellStack cell1, CellStack cell2) : this()
            {
                this.cell1 = cell1;
                this.cell2 = cell2;
            }
        }
    }
}