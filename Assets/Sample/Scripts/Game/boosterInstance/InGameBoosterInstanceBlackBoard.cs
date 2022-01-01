using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class InGameBoosterInstanceBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<InGameBoosterInstanceData> requestedInGameBoosterInstances = new List<InGameBoosterInstanceData>();

        public void Clear()
        {
            requestedInGameBoosterInstances.Clear();
        }
public enum InGameBoosterType
{
    upDownarrow,
    leftRightArrow,
    bomb,
    lightning
}
        public struct InGameBoosterInstanceData
        {
            public readonly Vector2 position;
            public readonly InGameBoosterType type;
            public readonly gemColors color;

            public InGameBoosterInstanceData(Vector2 position, InGameBoosterType type, gemColors color) : this()
            {
                this.position = position;
                this.type = type;
                this.color = color;
            }
        }
    }
}