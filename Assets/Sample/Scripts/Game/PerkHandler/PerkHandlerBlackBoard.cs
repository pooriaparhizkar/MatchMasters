using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    public class PerkHandlerBlackBoard : MonoBehaviour, BlackBoardData
    {
        public readonly List<PerkHandlerData> requestedPerkHandlers = new List<PerkHandlerData>();

        public void Clear()
        {
            requestedPerkHandlers.Clear();
        }
        public enum PerkHandlerType
        {
           hammer,
           shuffle
        }
        public struct PerkHandlerData
        {
            public readonly Vector2Int position;
            public readonly PerkHandlerType type;
            public readonly int isStart; //0:close , 1:open , 2:nothing
            public readonly bool isMyPerk;

            public PerkHandlerData(PerkHandlerType type,Vector2Int position,bool isMyPerk,int isStart=2) : this()
            {
                this.type = type;
                this.position = position;
                this.isStart = isStart;
                this.isMyPerk = isMyPerk;
            }
        }
    }
}