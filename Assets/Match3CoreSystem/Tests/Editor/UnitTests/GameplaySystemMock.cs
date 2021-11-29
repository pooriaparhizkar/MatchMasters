using Medrick.Match3CoreSystem.Game;
using System;


namespace Medrick.Mocks.Match3CoreSystem.Game
{
    public class GameplaySystemMock : GameplaySystem
    {
        public bool isUpdated;
        public bool isStarted;
        public bool isStopped;
        public bool isReset;
        public bool isActivated;

        public Action onActivationAction = delegate { };
        public Action onStartAction = delegate { };

        public T GetFrameData<T>() where T : BlackBoardData
        {
            throw new NotImplementedException();
        }

        public T GetSessionData<T>() where T : BlackBoardData
        {
            throw new NotImplementedException();
        }

        public void OnActivated()
        {
            isActivated = true;
            onActivationAction();
        }

        public void OnDeactivated()
        {
            isActivated = false;
        }

        public void Reset()
        {
            isReset = true;
        }

        public void Start()
        {
            isStarted = true;
            onStartAction();
        }

        public void Update(float dt)
        {
            isUpdated = true;
        }
    }
}