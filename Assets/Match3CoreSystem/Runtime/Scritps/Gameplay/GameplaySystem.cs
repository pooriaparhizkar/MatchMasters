namespace Medrick.Match3CoreSystem.Game
{
    public interface GameplaySystem
    {
        void Start();

        void Reset();

        void OnActivated();

        void OnDeactivated();

        void Update(float dt);

        T GetFrameData<T>() where T : BlackBoardData;


        T GetSessionData<T>() where T : BlackBoardData;

    }
}