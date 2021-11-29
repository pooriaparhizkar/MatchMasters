using Medrick.ComponentSystem.Core;

namespace Medrick.Match3CoreSystem.Game
{
    public class SystemBlackBoard : BasicSpecializedEntity<BlackBoardData>
    {
        public void Clear()
        {
            foreach (var data in AllComponents())
                data.Clear();
        }
    }
}
