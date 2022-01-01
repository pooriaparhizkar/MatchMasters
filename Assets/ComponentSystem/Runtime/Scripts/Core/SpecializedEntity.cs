using System.Collections.Generic;

namespace Medrick.ComponentSystem.Core
{
    public interface SpecializedEntity<T> where T : Component
    {
        void AddComponent(T component);
        U GetComponent<U>() where U : T;

        List<T> AllComponents();
    }
}