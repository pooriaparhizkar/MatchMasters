using Medrick.Match3CoreSystem.Game.Core;

namespace Medrick.Match3CoreSystem.Game
{
    public interface PresentationPort
    {
    }


    public interface GameplayMainController
    {
        LevelBoard LevelBoard { get; }
        GameplaySystemsController SystemsController { get; }

        SystemBlackBoard FrameBasedBlackBoard { get; }

        SystemBlackBoard SessionBasedBlackBoard { get; }

        void AddPresentationPort(PresentationPort port);
        T GetPresentationPort<T>() where T : PresentationPort;

        void Start();
        void Update(float dt);
    }
}