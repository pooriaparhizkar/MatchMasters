using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class DevkitBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<DevkitData> requestedDevkits = new List<DevkitData>();

        public void Clear()
        {
            requestedDevkits.Clear();
        }

        public struct DevkitData
        {
            public readonly string content;

            public DevkitData(string content) : this()
            {
                this.content = content;
            }
        }
    }
}