using System.Collections.Generic;
using Medrick.Match3CoreSystem.Game.Core;
using UnityEngine;
using Position = UnityEngine.Vector2;


namespace Medrick.Match3CoreSystem.Game
{
    public abstract class BasicGameplayMainController : MonoBehaviour,GameplayMainController
    {
        private readonly List<PresentationPort> presentationHandlers = new List<PresentationPort>();




        public BasicGameplayMainController(LevelBoard levelBoard, TileStackFactory tileFactory)
        {
            LevelBoard = levelBoard;
            SystemsController = new GameplaySystemsController();
            CreationController = new CreationController(tileFactory, levelBoard.CellStackBoard());

            AddFrameBasedBlackBoardData(FrameBasedBlackBoard);
            AddSessionBasedBlackBoardData(SessionBasedBlackBoard);

            AddSystems(SystemsController);
            SystemsController.StoreSystemsOrderings();

            SystemsController.ActivateSystemOfTag(GameplaySystemTag.General);
            SystemsController.ActivateSystemOfTag(GameplaySystemTag.StartOnly);
        }

        public CreationController CreationController { get; }
        public LevelBoard LevelBoard { get; }

        public GameplaySystemsController SystemsController { get; }

        public SystemBlackBoard FrameBasedBlackBoard { get; } = new SystemBlackBoard();
        public SystemBlackBoard SessionBasedBlackBoard { get; } = new SystemBlackBoard();

        public void Start()
        {
            SystemsController.Start();
        }

        public void Update(float dt)
        {
            SystemsController.Update(dt);
            FrameBasedBlackBoard.Clear();
        }

        public void AddPresentationPort(PresentationPort handler)
        {
            presentationHandlers.Add(handler);
        }

        public T GetPresentationPort<T>() where T : PresentationPort
        {
            foreach (var hanlder in presentationHandlers)
                if (hanlder is T)
                    return (T) hanlder;
            return default;
        }

        protected abstract void AddSystems(GameplaySystemsController systemsController);

        protected abstract void AddSessionBasedBlackBoardData(SystemBlackBoard sessionBasedBlackBoard);

        protected abstract void AddFrameBasedBlackBoardData(SystemBlackBoard frameBasedBlackBoard);

        public void ResetGame()
        {
            SessionBasedBlackBoard.Clear();
            SystemsController.ActivateSystemOfTag(GameplaySystemTag.General);
            SystemsController.ResetSystems();
        }
    }
}