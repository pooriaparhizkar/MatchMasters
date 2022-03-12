using Medrick.Match3CoreSystem.Game;
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

            systemsController.AddSystem(new PhysicSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new TopInstanceSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new SwapSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new CheckSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new PerkHandlerSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new InGameBoosterActivationSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new InGameBoosterInstanceSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new ScoreSystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new DestroySystem(this), GameplaySystemTag.General);
            systemsController.AddSystem(new TurnSystem(this), GameplaySystemTag.General);



        }

        protected override void AddFrameBasedBlackBoardData(SystemBlackBoard frameBasedBlackBoard)
        {
            frameBasedBlackBoard.AddComponent(new SwapBlackBoard());
            frameBasedBlackBoard.AddComponent(new InGameBoosterInstanceBlackBoard());
            frameBasedBlackBoard.AddComponent(new InGameBoosterActivationBlackBoard());
            frameBasedBlackBoard.AddComponent(new CheckBlackBoard());
            frameBasedBlackBoard.AddComponent(new ScoreBlackBoard());
            frameBasedBlackBoard.AddComponent(new DestroyBlackBoard());
            frameBasedBlackBoard.AddComponent(new TopInstanceBlackBoard());
            frameBasedBlackBoard.AddComponent(new PerkHandlerBlackBoard());
            frameBasedBlackBoard.AddComponent(new PhysicBlackBoard());
            frameBasedBlackBoard.AddComponent(new TurnBlackBoard());
        }

        protected override void AddSessionBasedBlackBoardData(SystemBlackBoard sessionBasedBlackBoard)
        {
        }
    }
}