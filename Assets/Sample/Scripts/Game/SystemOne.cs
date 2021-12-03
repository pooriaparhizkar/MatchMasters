using Medrick.Match3CoreSystem.Game;
using UnityEngine;

namespace Sample
{
    // This system preiodically requests a random swap.
    public class SystemOne : BasicGameplaySystem
    {
        private float currentWaitTime;
        private readonly float maxWaitTime = 3;

        public SystemOne(BasicGameplayMainController gameplayController) : base(gameplayController)
        {
        }

        public override void Update(float dt)
        {
            currentWaitTime += dt;

            if (currentWaitTime >= maxWaitTime)
            {
                GenerateRandomSwaps();
                currentWaitTime = 0;
            }
        }

        private void GenerateRandomSwaps()
        {
            var size = gameplayController.LevelBoard.CellStackBoard().Size();
            var firstPos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));
            var secondPos = new Vector2Int(Random.Range(0, size.x), Random.Range(0, size.y));

            GetFrameData<BlackBoardDataOne>().requestedSwaps.Add(new BlackBoardDataOne.SwapData(firstPos, secondPos));
        }
    }
}