using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class InGameBoosterActivationBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<InGameBoosterActivationData> requestedInGameBoosterActivations =
            new List<InGameBoosterActivationData>();

        public void Clear()
        {
            requestedInGameBoosterActivations.Clear();
        }

        public enum InGameBoosterType
        {
            upDownarrow,
            leftRightArrow,
            bomb,
            lightning,
            ArrowArrow,
            ArrowBomb,
            ArrowLightning,
            BombBomb,
            BombLightning,
            LightningLightning
        }

        public struct InGameBoosterActivationData
        {
            public readonly Vector2 position;
            public readonly InGameBoosterType type;
            public readonly gemColors gemColors;


            public InGameBoosterActivationData(Vector2 position, InGameBoosterType type,gemColors gemColors ) : this()
            {
                this.position = position;
                this.type = type;
                this.gemColors = gemColors;

            }
        }
    }
}