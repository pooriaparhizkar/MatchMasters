using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class PhysicBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<PhysicData> requestedPhysics = new List<PhysicData>();

        public void Clear()
        {
            requestedPhysics.Clear();
        }

        public struct PhysicData
        {
            public readonly Vector2Int pos1;
            public readonly Vector2Int pos2;

            public PhysicData(Vector2Int pos1, Vector2Int pos2) : this()
            {
                this.pos1 = pos1;
                this.pos2 = pos2;
            }
        }
    }
}