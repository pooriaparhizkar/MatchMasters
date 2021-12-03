﻿using Medrick.Match3CoreSystem.Game;
using Medrick.Match3CoreSystem.Game.Core;

namespace Sample
{
    public class SampleGameplayMainController : BasicGameplayMainController
    {
        public SampleGameplayMainController(LevelBoard levelBoard, TileStackFactory tileFactory) : base(levelBoard,
            tileFactory)
        {
        }

        protected override void AddSystems(GameplaySystemsController systemsController)
        {
            // NOTE: The order of the additions defines their order of execution

            // systemsController.AddSystem(new SystemOne(this), GameplaySystemTag.General);
            // systemsController.AddSystem(new SystemTwo(this), GameplaySystemTag.General);
        }

        protected override void AddFrameBasedBlackBoardData(SystemBlackBoard frameBasedBlackBoard)
        {
            // frameBasedBlackBoard.AddComponent(new BlackBoardDataOne());
            // frameBasedBlackBoard.AddComponent(new BlackBoardDataTwo());
        }

        protected override void AddSessionBasedBlackBoardData(SystemBlackBoard sessionBasedBlackBoard)
        {
        }
    }
}