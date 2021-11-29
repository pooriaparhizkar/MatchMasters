using System.Collections.Generic;
using System.Linq;

namespace Medrick.Match3CoreSystem.Game
{
    // TODO: Male gameplaysystem tags more generic.
    public enum GameplaySystemTag { General, EndOnly, StartOnly }

    public class GameplaySystemsController
    {

        Dictionary<GameplaySystemTag, HashSet<GameplaySystem>> systemTags;
        Dictionary<GameplaySystem, int> systemOrderings = new Dictionary<GameplaySystem, int>();

        List<GameplaySystem> gameplaySystems = new List<GameplaySystem>();

        List<GameplaySystem> activeSystems = new List<GameplaySystem>();


        List<GameplaySystem> gameplaySystemsToDeactivate = new List<GameplaySystem>();
        List<GameplaySystem> gameplaySystemsToActivate = new List<GameplaySystem>();

        public GameplaySystemsController()
        {
            systemTags = new Dictionary<GameplaySystemTag, HashSet<GameplaySystem>>();

            systemTags[GameplaySystemTag.EndOnly] = new HashSet<GameplaySystem>();
            systemTags[GameplaySystemTag.StartOnly] = new HashSet<GameplaySystem>();
            systemTags[GameplaySystemTag.General] = new HashSet<GameplaySystem>();
        }

        public void AddSystem(GameplaySystem system, GameplaySystemTag tag)
        {
            gameplaySystems.Add(system);
            systemTags[tag].Add(system);
        }

        public void StoreSystemsOrderings()
        {
            for (int i = 0; i < gameplaySystems.Count; ++i)
                systemOrderings[gameplaySystems[i]] = i;
        }

        public void ActivateSystemOfTag(GameplaySystemTag tag)
        {
            gameplaySystemsToActivate.AddRange(systemTags[tag]);
        }

        public void Start()
        {
            foreach (var system in gameplaySystems)
                system.Start();

            ProcessActivationRequests();
            ProcessDeactivatationRequests();
        }

        public void Update(float dt)
        {
            ProcessActivationRequests();

            foreach (var system in activeSystems)
                system.Update(dt);

            ProcessDeactivatationRequests();
        }

        public void ResetSystems()
        {
            ProcessDeactivatationRequests();
            ProcessActivationRequests();

            foreach (var system in gameplaySystems)
                system.Reset();

            ProcessDeactivatationRequests();
            ProcessActivationRequests();
        }

        private void ProcessDeactivatationRequests()
        {
            foreach (var system in gameplaySystemsToDeactivate)
            {
                activeSystems.Remove(system);
                system.OnDeactivated();
            }

            gameplaySystemsToDeactivate.Clear();
        }

        private void ProcessActivationRequests()
        {
            if (gameplaySystemsToActivate.Count == 0)
                return;

            foreach (var system in gameplaySystemsToActivate)
            {
                if (activeSystems.Contains(system) == false)
                {
                    activeSystems.Add(system);
                    system.OnActivated();
                }
            }

            activeSystems.Sort((a, b) => systemOrderings[a].CompareTo(systemOrderings[b]));

            gameplaySystemsToActivate.Clear();
        }

        public void RemoveSystem<T>() where T : BasicGameplaySystem
        {
            RequestDeactivation<T>();
            gameplaySystems.RemoveAll(g => g is T);
            foreach (var entry in systemTags)
                entry.Value.RemoveWhere(s => s is T);
        }

        public T GetSystem<T>() where T : GameplaySystem
        {
            return (T)gameplaySystems.FirstOrDefault(s => s is T);
        }

        public void RequestActivation<T>() where T : GameplaySystem
        {
            RequestActivation(GetSystem<T>());
        }

        public void RequestActivation(GameplaySystem gameplaySystem)
        {
            gameplaySystemsToActivate.Add(gameplaySystem);
        }

        public void RequestDeactivation<T>() where T : GameplaySystem
        {
            RequestDeactivation(GetSystem<T>());
        }

        public void RequestDeactivation(GameplaySystem gameplaySystem)
        {
            gameplaySystemsToDeactivate.Add(gameplaySystem);
        }

        public bool IsDeactive<T>() where T : GameplaySystem
        {
            return activeSystems.Contains(GetSystem<T>() as BasicGameplaySystem) == false;
        }

        public void DeactiveAllSystems()
        {
            gameplaySystemsToDeactivate.AddRange(activeSystems);
        }

        public IEnumerable<GameplaySystem> ActiveSystems()
        {
            return activeSystems;
        }

    }
}