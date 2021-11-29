using Medrick.Match3CoreSystem.Game;
using Medrick.Mocks.Match3CoreSystem.Game;
using NUnit.Framework;


namespace Medrick.Tests.Match3CoreSystem.Game
{
    public class GameplaySystemsContollerTest
    {

        GameplaySystemsController executer;

        GameplaySystemMock mockSystem1;
        GameplaySystemMock mockSystem2;

        [SetUp]
        public void Setup()
        {
            executer = new GameplaySystemsController();

            mockSystem1 = new GameplaySystemMock();
            mockSystem2 = new GameplaySystemMock();

        }

        [Test]
        public void HasTheAddedSystems()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);

            Assert.That(executer.GetSystem<GameplaySystemMock>(), Is.SameAs(mockSystem1));
        }

        [Test]
        public void StartsAddedSystemsWhenContainerIsStarted()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);

            executer.Start();

            Assert.That(mockSystem1.isStarted);
            Assert.That(mockSystem1.isStarted);
        }

        [Test]
        public void DoesNotActivateRequestedSystemWhenRequestIsMade()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);

            executer.RequestActivation(mockSystem1);

            Assert.That(mockSystem1.isActivated, Is.False);
        }

        [Test]
        public void ActivatesRequestedSystemAfterStart()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);
            executer.RequestActivation(mockSystem1);

            executer.Start();

            Assert.That(mockSystem1.isActivated, Is.True);
        }

        [Test]
        public void DeactivatesSystemDuringStartIfDeactivationIsRequestedOnStart()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);
            executer.RequestActivation(mockSystem1);
            mockSystem1.onStartAction = () => executer.RequestDeactivation(mockSystem1);

            executer.Start();

            Assert.That(mockSystem1.isActivated, Is.False);
        }

        [Test]
        public void DeactivatesSystemDuringStartIfDeactivationIsRequestedOnActivation()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);
            executer.RequestActivation(mockSystem1);
            mockSystem1.onActivationAction = () => executer.RequestDeactivation(mockSystem1);

            executer.Start();

            Assert.That(mockSystem1.isActivated, Is.False);
        }

        [Test]
        public void DoesNotUpdateNotActivatedSystems()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);

            executer.Update(1);

            Assert.That(mockSystem1.isUpdated, Is.False);
        }

        [Test]
        public void UpdatesActivatedSystems()
        {
            executer.AddSystem(mockSystem1, GameplaySystemTag.General);
            executer.RequestActivation(mockSystem1);
            executer.Start();

            executer.Update(1);

            Assert.That(mockSystem1.isUpdated, Is.True);
        }

    }
}