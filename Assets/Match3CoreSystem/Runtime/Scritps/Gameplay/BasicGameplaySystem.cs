using System;

namespace Medrick.Match3CoreSystem.Game
{
    // NOTE: This is currently use for documentation only.
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class AfterAttribute : Attribute
    {
        public AfterAttribute(Type type)
        {
        }
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class BeforeAttribute : Attribute
    {
        public BeforeAttribute(Type type)
        {
        }
    }

    public abstract class BasicGameplaySystem : GameplaySystem
    {
        protected BasicGameplayMainController gameplayController;

        protected BasicGameplaySystem(BasicGameplayMainController gameplayController)
        {
            this.gameplayController = gameplayController;
        }

        public virtual void Start()
        {
        }

        public virtual void OnActivated()
        {
        }

        public virtual void OnDeactivated()
        {
        }

        public virtual void Reset()
        {
        }


        public abstract void Update(float dt);


        public T GetFrameData<T>() where T : BlackBoardData
        {
            return gameplayController.FrameBasedBlackBoard.GetComponent<T>();
        }

        public T GetSessionData<T>() where T : BlackBoardData
        {
            return gameplayController.SessionBasedBlackBoard.GetComponent<T>();
        }
    }
}